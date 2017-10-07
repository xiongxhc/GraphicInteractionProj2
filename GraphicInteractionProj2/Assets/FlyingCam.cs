using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingCam : MonoBehaviour {

	public float speed = 100.0f;
	private Vector3 lastMouse = new Vector3 (255, 255, 255);

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		// using mouse

		lastMouse = Input.mousePosition - lastMouse;
		lastMouse = new Vector3 (transform.eulerAngles.x - lastMouse.y, transform.eulerAngles.y + lastMouse.x, 0 );
		transform.eulerAngles = lastMouse;

		lastMouse = Input.mousePosition;

		// using keyboard

		Vector3 dir = new Vector3 ();

		if (Input.GetKey (KeyCode.W)) dir.z += 1.0f;
		if (Input.GetKey (KeyCode.S)) dir.z -= 1.0f;
		if (Input.GetKey (KeyCode.A)) dir.x -= 1.0f;
		if (Input.GetKey (KeyCode.D)) dir.x += 1.0f;

		dir.Normalize ();
		transform.Translate (dir * speed * Time.deltaTime);

	}

}