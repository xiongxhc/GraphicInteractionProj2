using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainMenuHelpScript : MonoBehaviour {
	public Canvas mainCanvas;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ToggleHelpMenu(){
		mainCanvas.gameObject.SetActive (!mainCanvas.gameObject.activeSelf);
		gameObject.SetActive (!gameObject.activeSelf);
	}
}
