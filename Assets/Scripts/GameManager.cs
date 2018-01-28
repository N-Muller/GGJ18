using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameManager : NetworkBehaviour {

	public static GameManager Instance;
	public NetworkManagerHUD nmhud;

	[SyncVar]
	public int turn;

	public List<CardData> cards;
	public List<int> table;
	public List<RawCardSet.cardIntModel> solution;

	bool gameStarted = false;

	// Use this for initialization
	void Start () {
		Instance = this;


	}

	public void StartGame ()
	{
		gameStarted = true;

		Debug.Assert (Player.LocalPlayer.isServer, "StartGame called on client!!!");

		turn = 5;

		RawCardSet gen = new RawCardSet ();

		cards = gen.GenerateCards ();
		solution = gen.solution;

		cards.Sort ((a, b) => a.id - b.id);

		RpcUpdateCards (JsonUtility.ToJson (new Wrapper<List<CardData>> (cards)));

		Player.InitPlayers ();
		RpcUpdateCards (JsonUtility.ToJson (new Wrapper<List<CardData>> (cards)));


	}

	[ClientRpc]
	public void RpcUpdateCards(string serializedCards)
	{
		MenuAttente.SetActive (false);

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

	public GameObject MenuAttente;
	public void Ready()
	{
		Player.LocalPlayer.Ready ();
		nmhud.showGUI = false;

	}

	public GameObject menuFin;
	public Text textFin;
	public void Update()
	{
		if (isServer && gameStarted) {
			List<Card> l = CardFactory.Instance.Cards.FindAll (c => c.data.isOnTable);

			if (l.Count < Constants.TableSize)
				return;

			l.Sort ((a, b) => a.data.slotOnTable - b.data.slotOnTable);

			for (int i = 1; i < l.Count; i++) {
				if (l [i - 1].data.id != l [i].data.idprec)
					return;
			}

			RpcEndGame (false);
		}


	}

	[ClientRpc]
	void RpcEndGame(bool fourbeWins)
	{
		menuFin.SetActive (true);
		if (Player.LocalPlayer.role == Player.Role.Fourbe) {
			textFin.text= fourbeWins ? "You win!" : "You lost :(";
		}
		else
			textFin.text = !fourbeWins ? "You win!" : "You lost :(";
	}
}
