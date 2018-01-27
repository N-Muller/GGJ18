using UnityEngine.Networking;
using UnityEngine;
using System.Collections.Generic;
using System;

class Player : NetworkBehaviour
{
	[Serializable]
	public enum Role { Undefined, Fourbe, Gentil }

	public static Player LocalPlayer;

	public static List<Player> Players;


	public Role role = Role.Undefined;

	public List<int> hand;

	public bool ready = false;

	void Start()
	{
		if (isLocalPlayer)
			LocalPlayer = this;

		Players = new List<Player> ();
	}


	[Command]
	void CmdReady()
	{
		ready = true;

		if (!Players.Exists (p => p.ready = false)) {
			InitPlayers ();
		}
	}

	[ClientRpc]
	void RpcInit( /* Role r, List<int> cards*/ )
	{
		/*
		role = r;
		hand = cards;
		*/
	}


	static void InitPlayers()
	{
		int fourbe = UnityEngine.Random.Range (0, Players.Count);
		List<int> deck = new List<int> ();

		for (int i = 0; i < 20; i++) {
			deck.Add (i);
		}


		for (int i = 0; i < Players.Count; i++) {
			Players [i].role = i == fourbe ? Role.Fourbe : Role.Gentil;
		}

	}


	[SyncVar]
	int health;

	[ClientRpc]
	void RpcDamage(int amount)
	{
		Debug.Log("Took damage:" + amount);
	}


	[Command]
	void CmdDamage(int amount)
	{
		print ("eeee");
		TakeDamage (amount);
	}


	public void DoCmdDamage(int amount)
	{
		if (!isLocalPlayer)
			return;
		
		CmdDamage (amount);

	}

	public void TakeDamage(int amount)
	{
		if (!isServer)
			return;

		health -= amount;
		RpcDamage(amount);
	}
}