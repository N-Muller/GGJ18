using UnityEngine.Networking;
using UnityEngine;
using System.Collections.Generic;
using System;

class Player : NetworkBehaviour
{
	[Serializable]
	public enum Role { Undefined, Fourbe, Gentil }

	[Serializable]
	public class SerializedPlayerData
	{
		public Role role;
		public List<int> hand;

		public SerializedPlayerData( Player p)
		{
			role = p.role;
			hand = p.hand;
		}
	}


	public static Player LocalPlayer;

	public static List<Player> Players = new List<Player> ();

	public Role role = Role.Undefined;
	public List<int> hand;

	public static bool PlayersInitialized = false;

	[SyncVar]
	public bool ready = false;

	[SyncVar]
	public bool hasPlayed = false;

	void Start()
	{
		if (isLocalPlayer)
			LocalPlayer = this;

		Players.Add (this);

		name = "Player_" + Players.Count;
	}


	[Command]
	void CmdReady()
	{
		ready = true;

		Debug.Assert (isServer == true, "This function is supposed to be called by the server");

		if (!Players.Exists (p => !p.ready ) && Players.Count == Constants.PlayerCount && !PlayersInitialized) {
			GameManager.Instance.StartGame ();
		}
	}

	public void Ready()
	{
		if (!isLocalPlayer)
			return;

		CmdReady ();
	}

	public static void InitPlayers()
	{
		int fourbe = UnityEngine.Random.Range (0, Players.Count);
		List<int> deck = new List<int> ();

		for (int i = 0; i < Constants.DeckSize; i++) 
		{
			deck.Add (i);
		}


		for (int i = 0; i < Players.Count; i++) {
			Player p = Players [i];

			p.role = i == fourbe ? Role.Fourbe : Role.Gentil;

			for (int j = 0; j < Constants.HandSize; j++) {
				int cardId = UnityEngine.Random.Range (0, deck.Count);
				p.hand.Add (deck [cardId]);
				deck.RemoveAt (cardId);
			}

			SerializedPlayerData data = new SerializedPlayerData (p);

			p.RpcSyncClient (JsonUtility.ToJson (data));
		}

		PlayersInitialized = true;
	}
		
	[ClientRpc]
	void RpcSyncClient(string serializedPlayerData)
	{
		SerializedPlayerData data = JsonUtility.FromJson<SerializedPlayerData> (serializedPlayerData);

		role = data.role;
		hand = data.hand;

		if (isLocalPlayer) {
			DropContainer container = DropContainer.ByName ["Main"];

			for (int i = 0; i < hand.Count; i++)
			{
				Card c = CardFactory.Instance.Cards [hand [i]];
				c.gameObject.SetActive (true);
				c.transform.parent = container.dms [i].transform;
				c.transform.position = container.dms [i].transform.position;

			}
		}
	}

	public void CardDroppedOn(int cardId, DropMe dm )
	{
		Card c = CardFactory.Instance.Cards [cardId];



		switch (dm.name) {
		case "Table":
			if (c.data.isOnTable)
				return;


			break;
		case "Main":
			if (! c.data.isOnTable)
				return;


			break;
		case "PlayerDrops":

			break;
		}
	}

	public void Reveal (Zone z)
	{

	}
}