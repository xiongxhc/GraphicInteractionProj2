using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuControl : MonoBehaviour {

	public void playGame () {
		SceneManager.LoadScene("game", LoadSceneMode.Single);
	}

	public void quitGame () {
		Application.Quit ();
	}

}
