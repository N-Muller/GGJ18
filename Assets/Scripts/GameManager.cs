using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour {

	public static GameManager Instance;

	[SyncVar]
	public int turn;


	List<CardData> cards;

	// Use this for initialization
	void Start () {
		Instance = this;
	}


	void StartGame ()
	{
		Debug.Assert (isServer, "StartGame called on client!!!");

		turn = 0;

		RawCardSet gen = new RawCardSet ();

		cards = gen.GenerateCards ();



		Player.InitPlayers ();



	}


	// Update is called once per frame
	void Update () {
		
	}
}
