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

	[SyncVar]
	public int id;

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

		id = Players.Count;

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

	public bool CardDroppedOn(int cardId, DropMe dm )
	{
		Debug.Assert (isLocalPlayer, ":(");

		print ("CardDroppedOn(int " + cardId + "," + dm);

		Card c = CardFactory.Instance.Cards [cardId];

		switch (dm.container.name) {
		case "Table":
			CmdPoser (cardId, dm.container.dmsNameToId [dm.name]);
			break;
		case "Main":
			// if (c.data.isOnTable)
			CmdVerifier (cardId, dm.container.dmsNameToId [dm.name]);
			break;
		case "PlayerDrops":
			CmdEnvoyer (cardId, DropIdToPlayerId (dm.container.dmsNameToId [dm.name]));
			break;
		}

		return false;
	}

	[Command]
	void CmdPoser( int cardId, int dropId)
	{
		// TODO => éviter que qqun puisse poser une carte la ou il y en a déjà une. 

		hasPlayed = true;
		CardData dat = CardFactory.Instance.Cards [cardId].data;

		dat.isOnTable = true;
		dat.slotOnTable = dropId;

		hand.Remove (cardId);

		RpcBroadcast (JsonUtility.ToJson(dat));

		RpcHasPlayed ();
	}

	[Command]
	void CmdEnvoyer ( int cardId, int playerId)
	{
		if (Players [playerId].hand.Count >= Constants.MaxHandSize) {
			return;
		}


		hasPlayed = true;
		CardData dat = CardFactory.Instance.Cards [cardId].data;

		Player destinataire = Players [playerId];

		destinataire.hand.Add (dat.id);

		destinataire.RpcSyncClient (JsonUtility.ToJson (new SerializedPlayerData (destinataire)));
		// TODO : the player that receive the card will have to verify it .

		CardFactory.Instance.Cards [cardId].gameObject.SetActive (false);
		CardFactory.Instance.Cards [cardId].transform.parent = CardFactory.Instance.transform;


		RpcHasPlayed ();

		return;
	}

	[Command]
	void CmdVerifier( int cardId, int dropId)
	{
		hasPlayed = true;

		CardData dat = CardFactory.Instance.Cards [cardId].data;
	



		bool carteTruquee = 
			Constants.IdToImageName (dat.idnext) != dat.imageSuivant ||
			Constants.IdToImageName (dat.idprec) != dat.imagePrecedent;


		//TODO : interface pour dire au joueur si la carte est truquée .
		print (carteTruquee);


		hand.Add (cardId);
		dat.isOnTable = false;
		dat.slotOnTable = -1;

		foreach (Player p in Players) {
			RpcBroadcast (JsonUtility.ToJson (dat));
		}

		RpcSyncClient (JsonUtility.ToJson (new SerializedPlayerData (this)));

		RpcHasPlayed ();
	}

	[Command]
	void CmdEchanger (int playerId)
	{
		
	}

	[ClientRpc]
	void RpcHasPlayed()
	{
		if (!isLocalPlayer)
			return;

		//TODO : bloquer les actions du joueur jusqu'au début du prochain tour. 
	}

	[ClientRpc]
	void RpcBroadcast(string serializedDat)
	{
		CardFactory.Instance.UpdateCard (JsonUtility.FromJson<CardData> (serializedDat));
	}


	public void Reveal (Zone z)
	{

	}

	public int DropIdToPlayerId(int dropId)
	{
		return dropId + (dropId >= id ? 1 : 0);
	}
}