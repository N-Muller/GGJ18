using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour {

	public static GameManager Instance;

	[SyncVar]
	public int turn;

	public List<CardData> cards;
	public List<int> table;


	// Use this for initialization
	void Start () {
		Instance = this;


	}

	public void StartGame ()
	{
		Debug.Assert (Player.LocalPlayer.isServer, "StartGame called on client!!!");

		turn = 5;

		RawCardSet gen = new RawCardSet ();

		cards = gen.GenerateCards ();

		cards.Sort ((a, b) => a.id - b.id);

		RpcUpdateCards (JsonUtility.ToJson (new Wrapper<List<CardData>> (cards)));

		Player.InitPlayers ();
	}

	[ClientRpc]
	public void RpcUpdateCards(string serializedCards)
	{
		print ("Receiving : " + serializedCards);


		cards = JsonUtility.FromJson<Wrapper<List<CardData>>> (serializedCards);

		print ("Cards is now : " + cards.ToString ());

		CardFactory.Instance.UpdateCards (cards);
	}

	[ClientRpc]
	public void RpcUpdateTable(string serializedTable)
	{
		DropContainer table = DropContainer.ByName ["Table"];

		table.cardInSlot = JsonUtility.FromJson<Wrapper<List<int>>> (serializedTable);

		for (int slotId = 0; slotId < table.cardInSlot.Count; slotId++) {
			int cardId = table.cardInSlot [slotId];
			if (cardId != -1) {
				Card c = CardFactory.Instance.Cards [cardId];
				c.gameObject.SetActive (true);
				c.transform.parent = table.dms [slotId].transform;
				c.transform.position = table.dms [slotId].transform.position;
			}
		}
	}

}
