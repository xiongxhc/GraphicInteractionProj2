using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankControl : MonoBehaviour {
	public float powerKN = 1;
	public float rotateSpeed = 5;
	private float power;
	private float torque;

	// Use this for initialization
	public Rigidbody rb;
	void Start () {
		//transform.rotation = transform.rotation *= Quaternion.Euler (0, -90, 0);
		//transform.Rotate (-90, 0, 0);
		rb = GetComponent<Rigidbody>();
		power = powerKN * 1000;
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
		if (Input.GetKey(KeyCode.W)) rb.AddForce(transform.right * power);
		if (Input.GetKey(KeyCode.S)) rb.AddForce(transform.right * -power);
		if (Input.GetKey (KeyCode.A)) {
			transform.Rotate (0.0f,rotateSpeed*-1,0.0f,Space.World);
		}
			
		if (Input.GetKey (KeyCode.D))
			transform.Rotate (0.0f,rotateSpeed,0.0f,Space.World);;
	}
	void Keyboard_control(){

	}
	public Vector3 getWorldPosition(){
		return transform.position;
	}
	public Transform getTransform(){
		return transform;
	}
}
