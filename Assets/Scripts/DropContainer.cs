using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropContainer : MonoBehaviour {
	public List<int> cardInSlot; // cardInSlot[slotId] => -1 for no card || cardId;

	public static Dictionary<string, DropContainer> ByName = new Dictionary<string, DropContainer> ();

	public Dictionary<string, int> dmsNameToId = new Dictionary<string, int> ();
	public DropMe[] dms;


	void Start()
	{
		ByName.Add (name, this);

		cardInSlot = new List<int> ();

		for (int i = 0; i < Constants.DCNameToSize (name); i++)
			cardInSlot.Add (-1);

		dms = GetComponentsInChildren<DropMe> ();

		Debug.Assert (dms.Length >= cardInSlot.Count, ":(");

		for (int i = 0; i < dms.Length; i++) {
			dmsNameToId.Add (dms [i].name, i);
			dms [i].container = this;
		}
	}
}
