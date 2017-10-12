using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audio : MonoBehaviour {

	public AudioSource ShootingAudio;  
	public AudioClip FireClip; 
	private string FireButton;         

	// Use this for initialization
	void Start () {
		FireButton = "Fire";
	}
	
	// Update is called once per frame
	void Update () {
		ShootingAudio.clip = FireClip;
		ShootingAudio.Play ();

	}
}
