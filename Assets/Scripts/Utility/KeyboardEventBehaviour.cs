using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyboardEventBehaviour : MonoBehaviour {

	public KeyCode key;
	public UnityEvent OnToggleTrue;
	public UnityEvent OnToggleFalse;
	public UnityEvent OnDown;
	public UnityEvent OnUp;

	[Tooltip("Initial state.\nWill change on first key press.")]
	bool toggle = false;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (key)) {
			toggle = !toggle;
			OnUp.Invoke ();
			if (toggle)
				OnToggleTrue.Invoke ();
			else
				OnToggleFalse.Invoke ();
		}

		if (Input.GetKeyUp (key)) 
			OnDown.Invoke ();
	}
}
