﻿using System.Collections;
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
	[Tooltip ("GunAiming")] public GunAiming gunAiming;
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
	public void AimTarget(Vector3 target){
		gunAiming.setAimingTarget (target);
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
		return leftTrackGrounded || rightTrackGrounded;
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
	public void accelerate(int forceDirection){
		if (!isGrounded ())
			return;

		rb.AddForce(transform.right * power * forceDirection * Time.deltaTime);
	}
	public void turn(int torqueDirection,int forceDirection){
		if (!isGrounded ())
			return;
		forceDirection = forceDirection == 0? 1:forceDirection;
		rb.AddTorque (transform.forward * torque * torqueDirection * forceDirection * Time.deltaTime);
	}

	public Vector3 getWorldPosition(){
		return transform.position;
	}
	public Transform getTransform(){
		return transform;
	}
//	void updateHealth(){
//
//	}
}
