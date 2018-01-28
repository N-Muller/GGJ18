using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TexteAttente : MonoBehaviour {

    public UnityEngine.UI.Text display;
    public UnityEngine.UI.Text input;
	
	// Update is called once per frame
	void Update () {
        display.text = "Bienvenue à toi " + input.text + " ! \n En attente des autres joueurs.";
        Debug.Log("Bienvenue à toi "+input.text+" !");
    }
}
