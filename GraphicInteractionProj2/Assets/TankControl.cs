using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankControl : MonoBehaviour {
	public float powerKN = 1;
	public float rotateSpeed = 5;
	//public float torqueKN = 5;
	private float power;
	private float torque;
	private bool leftTrackGrounded;
	private bool rightTrackGrounded;
	[Tooltip ("TankTrack")] public TankTrack leftTrack;
	[Tooltip ("TankTrack")] public TankTrack rightTrack;
	// Use this for initialization
	public Rigidbody rb;
	void Start () {
		//transform.rotation = transform.rotation *= Quaternion.Euler (0, -90, 0);
		//transform.Rotate (-90, 0, 0);
		rb = GetComponent<Rigidbody>();
		power = powerKN * 1000;
		//torque = torqueKN * 1000;
	}
	
	// Update is called once per frame

	void OnCollisionStay(Collision collision){
		foreach(ContactPoint contact in collision.contacts){
			if(contact.thisCollider.name == ("LeftTrack")) leftTrackGrounded = true;
			if(contact.thisCollider.name == ("RightTrack")) rightTrackGrounded = true;

		}
	}
	void OnCollisionExit(Collision collision){
	}
	bool isGrounded(){
		return leftTrackGrounded && rightTrackGrounded;
	}
	void FixedUpdate(){
		if (isGrounded ()) {
		} else {
		}
		if (!rb.IsSleeping()) {
			leftTrackGrounded = false;
			rightTrackGrounded = false;
		}

	}
	void Update() {

		Debug.Log(isGrounded());
		if (!isGrounded ())
			return;
		int forceDirection = 0;
		if (Input.GetKey (KeyCode.W))
			forceDirection = 1;
		if (Input.GetKey (KeyCode.S))
			forceDirection = -1;
		rb.AddForce(transform.right * power * forceDirection * Time.deltaTime);

		if (Input.GetKey (KeyCode.A)) {
			transform.Rotate (0.0f,rotateSpeed*-1* Time.deltaTime,0.0f,Space.World);
		}
	
		if (Input.GetKey (KeyCode.D))
			transform.Rotate (0.0f,rotateSpeed* Time.deltaTime,0.0f,Space.World);

		if (Input.GetKey(KeyCode.Escape))
			Cursor.lockState = CursorLockMode.None;
		else
			Cursor.lockState = CursorLockMode.Locked;
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
