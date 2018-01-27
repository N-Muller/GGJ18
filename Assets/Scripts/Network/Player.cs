using UnityEngine.Networking;
using UnityEngine;
using System.Collections.Generic;
using System;

class Player : NetworkBehaviour
{
	enum Type { None, Peasant, Fourbe }

	[Serializable]
	public class Card
	{
		int id;

		public Card(int id)
		{
			this.id = id;
		}
	}

	[SyncVar]
	Type type = Type.None;
	[SyncVar]
	List<Card> hand;






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

		// health -= amount;
		RpcDamage(amount);
	}
}