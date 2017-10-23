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
	public float shellDamage = 20;
	public float engineMaxPitch = 1.2f;
	public float engineMinPitch = 0.4f;
	public float enginePitchRandRange = 0.3f;
	public float powerDirectionMultipler = 0.6f;
	private float soundOffset;
	public ParticleSystem gunFireParticle;
	private float power;
	private float torque;
	private bool leftTrackGrounded;
	private bool rightTrackGrounded;
	private float inclineOffset;
	[Tooltip ("GunAiming")] public GunAiming gunAiming;
	[Tooltip ("TankTrack")] public TankTrack leftTrack;
	[Tooltip ("TankTrack")] public TankTrack rightTrack;
	[Tooltip ("TankBody")] public TankBody tankBody;
	[Tooltip ("TurretControl")] public TurretControl turretControl;
	[Tooltip ("shoot")] public shoot shootScript;
	[Tooltip ("ShootingPointController")] public ShootingPointController shootingPointController;

	// Use this for initialization
	public Rigidbody rb;
	void Start () {
		//transform.rotation = transform.rotation *= Quaternion.Euler (0, -90, 0);
		//transform.Rotate (-90, 0, 0);
		rb = GetComponent<Rigidbody>();
		power = powerKN * 1000;
		torque = torqueKN * 1000;
		rb.centerOfMass = new Vector3 (0,0,-5);
		inclineOffset = transform.localRotation.eulerAngles.x;
		shootScript.setParticle (gunFireParticle);
		soundOffset = Random.Range (-enginePitchRandRange, enginePitchRandRange);
	}
	public void setDamage(float Damage){
		shellDamage = Damage;
	}
	public void AimTarget(Vector3 target){
		gunAiming.setAimingTarget (target);
	}
	public void Fire(){
		shootScript.Fire ();
		if (gunFireParticle == null)
			return;
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
		foreach (Transform child in transform) {
			var localVel = transform.InverseTransformDirection(rb.velocity);
			var forwordSpeed = localVel.x;
			var rotateSpeed = rb.angularVelocity.y;
			if(child.name.Contains("Wheel")){
				// get diameter
				var length = child.GetComponent<MeshRenderer> ().bounds.extents.x;
				var c = 2 * Mathf.PI * length;
				var rotationDirection = child.name.Contains ("Left") ? 1 : -1;
				// calculate Rotation per second
				var wheelSpeed = forwordSpeed + -rotateSpeed * rotationDirection * 10;
				var rps = wheelSpeed / c;
				// apply rotation
				child.Rotate (Vector3.forward,wheelSpeed * Time.deltaTime*10);
			}
		}
		
		// 
		rb.maxAngularVelocity = maxRotateSpeed;


	}
	public void accelerate(int forceDirection){
		if(forceDirection!=0)
			GetComponents<AudioSource> ()[0].pitch = Mathf.Lerp(GetComponent<AudioSource> ().pitch, engineMaxPitch + soundOffset,0.1f);
		else 
			GetComponents<AudioSource> ()[0].pitch = Mathf.Lerp(GetComponent<AudioSource> ().pitch, engineMinPitch + soundOffset,0.1f);
		if (!isGrounded ())
			return;

		var DirectionMultipler = forceDirection == -1 ? powerDirectionMultipler : 1;
//		var currentPower = power * forceDirection * Time.deltaTime * DirectionMultipler;
//		var forwardPower = Mathf.Cos(getIncline()*Mathf.Deg2Rad) * currentPower; 
		rb.AddForce(transform.right * power * forceDirection * Time.deltaTime * DirectionMultipler);
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
	public float getIncline(){
		return transform.localEulerAngles.x - inclineOffset;
	}
	void Update(){

	}
//	void updateHealth(){
//
//	}
}
