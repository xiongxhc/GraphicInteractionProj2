using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuControl : MonoBehaviour {

	public void playGame () {
		SceneManager.LoadScene(2);
	}

	public void option () {
		SceneManager.LoadScene(1);
	}

	public void quitGame () {
		Application.Quit ();
	}

	public void backToMenu () {
		SceneManager.LoadScene(0);
	}

}
