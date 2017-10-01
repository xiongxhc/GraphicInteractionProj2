using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankControl : MonoBehaviour {

	public float forwardSpeed = 1;
	// Use this for initialization
	public Rigidbody rigidbody;
	void Start () {
		//transform.rotation = transform.rotation *= Quaternion.Euler (0, -90, 0);
		//transform.Rotate (-90, 0, 0);
		rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
//		Keyboard_control ();
//		transform.Rotate(Vector3.up * Time.deltaTime * 20);
		if (Input.GetKey(KeyCode.Escape))
			Cursor.lockState = CursorLockMode.None;
		else
			Cursor.lockState = CursorLockMode.Locked;
	
	}
	void FixedUpdate() {
//		if (Input.GetButtonDown("Jump"))
//			rigidbody.velocity = new Vector3(0, 10, 0);
		if (Input.GetKey(KeyCode.W)) rigidbody.velocity += transform.right * forwardSpeed;
	}
	void Keyboard_control(){

	}
}
