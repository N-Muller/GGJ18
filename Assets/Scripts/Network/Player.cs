using UnityEngine.Networking;
using UnityEngine;

class Player : NetworkBehaviour
{

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