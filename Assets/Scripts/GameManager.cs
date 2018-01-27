using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	List<Player> players;

	// Use this for initialization
	void Start () {
		
	}


	void StartGame ()
	{
		players = new List<Player> (transform.root.GetComponentsInChildren<Player> ());



	}


	// Update is called once per frame
	void Update () {
		
	}
}
