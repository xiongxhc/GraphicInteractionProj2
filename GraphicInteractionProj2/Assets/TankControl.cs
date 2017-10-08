using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankControl : MonoBehaviour {
	public float powerKN = 1;
	public float maxRotateSpeed = 5;
	public float torqueKN = 50;
	public float airDrag = 0.1f;
	public float groundGrag = 2.3f;
	public float stationeryTorqueMutiplier = 1.5f;
	private float power;
	private float torque;
	private bool leftTrackGrounded;
	private bool rightTrackGrounded;
	[Tooltip ("TankTrack")] public TankTrack leftTrack;
	[Tooltip ("TankTrack")] public TankTrack rightTrack;
	[Tooltip ("TankBody")] public TankBody tankBody;
	// Use this for initialization
	public Rigidbody rb;
	void Start () {
		//transform.rotation = transform.rotation *= Quaternion.Euler (0, -90, 0);
		//transform.Rotate (-90, 0, 0);
		rb = GetComponent<Rigidbody>();
		power = powerKN * 1000;
		torque = torqueKN * 1000;
		rb.centerOfMass = new Vector3 (0,0,-5);
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
			rb.drag = airDrag + groundGrag;
		} else {
			rb.drag = airDrag;
		}
		if (rb.velocity.magnitude < 0.1f) {
			torque = torqueKN * 1000 * stationeryTorqueMutiplier;
		} else {
			torque = torqueKN * 1000;
		}
		if (!rb.IsSleeping()) {
			leftTrackGrounded = false;
			rightTrackGrounded = false;
		}
		// 
		rb.maxAngularVelocity = maxRotateSpeed;
	}
	void Update() {
		//Debug.Log (torque);
		if (!isGrounded ())
			return;
		int forceDirection = 0;
		if (Input.GetKey (KeyCode.W))
			forceDirection = 1;
		if (Input.GetKey (KeyCode.S))
			forceDirection = -1;
		rb.AddForce(transform.right * power * forceDirection * Time.deltaTime);
		int torqueDirection = 0;
		if (Input.GetKey (KeyCode.A)) 
			torqueDirection = -1;
		if (Input.GetKey (KeyCode.D))
			torqueDirection = 1;
		//var position = transform.forward + distance * torqueDirection;
		//rb.AddForceAtPosition (Vector3.up * 500000, position);
//		transform.Rotate (0.0f,0.0f,rotateSpeed* Time.deltaTime * torqueDirection,Space.Self);
		rb.AddTorque (transform.forward * torque * torqueDirection * forceDirection * Time.deltaTime);
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
