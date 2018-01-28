using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour {

	public Sprite a;
	public bool b;

	[ContextMenu("test")]
	void test()
	{
		this.GetComponent<Image> ().sprite = b ? a : ResourcesLoader.Load<Sprite> ("Sprites/armes");
	}
}
