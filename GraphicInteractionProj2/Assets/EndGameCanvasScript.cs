using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGameCanvasScript : MonoBehaviour {
	public Text EndGameWaveText;
	[Tooltip ("GameController")] public GameController gameController;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		EndGameWaveText.text = gameController.getWaveCount ().ToString();
	}
	public void backToMenu(){
		SceneManager.LoadScene("menu", LoadSceneMode.Single);
	}
}
