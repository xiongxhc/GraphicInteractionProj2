﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour {

	public float movementSpeed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Keyboard_control ();
	}
	void Keyboard_control(){
		Vector3 dir = new Vector3 ();

		if (Input.GetKey (KeyCode.W)) dir.x += 1.0f;
		if (Input.GetKey (KeyCode.S)) dir.x -= 1.0f;
		if (Input.GetKey (KeyCode.A)) dir.y -= 1.0f;
		if (Input.GetKey (KeyCode.D)) dir.y += 1.0f;

		//dir.Normalize ();
		//transform.rotation = camera.transform.rotation;
		transform.Translate (dir * movementSpeed * Time.deltaTime);
	}
}