using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankControl : MonoBehaviour {
	public float powerKN = 1;
	public float torqueKNM = 5;
	private float power;
	private float torque;
	// Use this for initialization
	public Rigidbody rb;
	void Start () {
		//transform.rotation = transform.rotation *= Quaternion.Euler (0, -90, 0);
		//transform.Rotate (-90, 0, 0);
		rb = GetComponent<Rigidbody>();
		power = powerKN * 1000;
		torque = torqueKNM * 1000;
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
	void OnGUI() {
		GUI.Label (new Rect (10, 40, 200, 200), rb.velocity.ToString());
	}
	void FixedUpdate() {
//		if (Input.GetButtonDown("Jump"))
//			rigidbody.velocity = new Vector3(0, 10, 0);
		if (Input.GetKey(KeyCode.W)) rb.AddForce(transform.right * power);
		// check if tank stationery. If so, toque multiply by two - since two track will be both rotating.
		var torque_multi = 1f;
		if (rb.velocity.magnitude <= 0.1f)
			torque_multi = 2f;
		if (Input.GetKey (KeyCode.A)) {
			rb.AddTorque (transform.forward * torque * -1 * torque_multi);
		}
			
		if (Input.GetKey (KeyCode.D))
			rb.AddTorque (transform.forward * torque * 1 * torque_multi);
	}
	void Keyboard_control(){

	}
}
