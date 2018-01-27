using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour {

	public static GameManager Instance;

	[SyncVar]
	public int turn;


	// Use this for initialization
	void Start () {
		Instance = this;
	}


	void StartGame ()
	{
		Debug.Assert (isServer, "StartGame called on client!!!");

		turn = 0;




	}


	// Update is called once per frame
	void Update () {
		
	}
}
