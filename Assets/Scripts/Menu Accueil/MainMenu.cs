using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void ReadyGame()
    {
        Debug.Log("Prêt à jouer !");
    }

	public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
