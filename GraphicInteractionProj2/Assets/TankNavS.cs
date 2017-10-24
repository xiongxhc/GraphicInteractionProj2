using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankNavS : MonoBehaviour {
	[Tooltip ("TankControl")] public TankControl tankControl;
	[Tooltip ("centerOfMass")] public centerOfMass massCenter;

	public UnityEngine.AI.NavMeshAgent nav;
	private bool AIControl = false;
	public Rigidbody rb;
	private float power;
	private float torque;
	public float powerKN = 220;
	public float maxRotateSpeed = 1.5f;
	public float torqueKN = 1100;
	public float airDrag = 0.05f;
	public float groundGrag = 0.15f;
	public float stationeryTorqueMutiplier = 10f;
	private bool leftTrackGrounded = false;
	private bool rightTrackGrounded = false;
	public float powerDirectionMultipler = 0.8f;
	public float engineMaxPitch = 0.8f;
	public float engineMinPitch = 0.4f;
	public float enginePitchRandRange = 0.3f;
	public float maxVelocity = 40f;
	public float NavStartDelay = 0.5f; // depaly navgation active by 0.5 seconds.
	private float soundOffset;
//	private Quaternion spawnQuaternion = Quaternion.Euler(-90,0,0);
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		power = powerKN * 1000;
		torque = torqueKN * 1000;
		soundOffset = Random.Range (-enginePitchRandRange, enginePitchRandRange);
		rb.centerOfMass = massCenter.transform.localPosition;
	}
	public void setAIControl(){
		AIControl = true;
	}
	void OnCollisionStay(Collision collision){
		foreach(ContactPoint contact in collision.contacts){
			if(contact.thisCollider.name == ("LeftTrack")) leftTrackGrounded = true;
			if(contact.thisCollider.name == ("RightTrack")) rightTrackGrounded = true;
		}
	}
	public TankControl getTankControl(){
		return tankControl;
	}
	public UnityEngine.AI.NavMeshAgent getNav(){
		return nav;
	}
	bool isGrounded(){
		return leftTrackGrounded || rightTrackGrounded;
	}
	void FixedUpdate(){
		if (AIControl) {
//			tankControl.transform.localRotation = spawnQuaternion;
			return;
		}
		if (!rb.IsSleeping()) {
			leftTrackGrounded = false;
			rightTrackGrounded = false;
		}
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

		foreach (Transform child in transform) {
			var localVel = transform.InverseTransformDirection(rb.velocity);
			var forwordSpeed = localVel.x;
			var rotateSpeed = rb.angularVelocity.y;
			if(child.name.Equals("PlayerTank")){
				foreach (Transform childinchild in child) {
					if(childinchild.name.Contains("Wheel")){
						// get diameter
						var length = childinchild.GetComponent<MeshRenderer> ().bounds.extents.x;
						var c = 2 * Mathf.PI * length;
						var rotationDirection = childinchild.name.Contains ("Left") ? 1 : -1;
						// calculate Rotation per second
						var wheelSpeed = forwordSpeed + -rotateSpeed * rotationDirection * 10;
						//var rps = wheelSpeed / c;
						// apply rotation
						childinchild.Rotate (Vector3.forward,wheelSpeed * Time.deltaTime*10);
					}
				}
			}

		}
		rb.maxAngularVelocity = maxRotateSpeed;

		var localVelocity = transform.InverseTransformDirection (rb.velocity);
		// kill drift
		localVelocity.z = 0;
		// eliminate overspeed
		localVelocity.x = localVelocity.x > maxVelocity ? maxVelocity : localVelocity.x;
		rb.velocity = transform.TransformDirection (localVelocity);
	}

	// Update is called once per frame
	void Update () {
		NavStartDelay -= Time.deltaTime;
		NavStartDelay = NavStartDelay <= 0 ? 0 : NavStartDelay;
		if (AIControl && NavStartDelay <= 0 && !getNav().enabled) {
			getNav ().enabled = true;
		}
	}
	public void accelerate(int forceDirection){
		if(forceDirection!=0)
			GetComponents<AudioSource> ()[0].pitch = Mathf.Lerp(GetComponent<AudioSource> ().pitch, engineMaxPitch + soundOffset,0.1f);
		else 
			GetComponents<AudioSource> ()[0].pitch = Mathf.Lerp(GetComponent<AudioSource> ().pitch, engineMinPitch + soundOffset,0.1f);
		if (!isGrounded() || rb.velocity.magnitude > maxVelocity)
			return;
		var DirectionMultipler = forceDirection == -1 ? powerDirectionMultipler : 1;
		rb.AddForce(transform.right * power * forceDirection * Time.deltaTime * DirectionMultipler);
	}
	public void turn(int torqueDirection,int forceDirection){
		if (!isGrounded())
			return;
		forceDirection = forceDirection == 0? 1:forceDirection;
		rb.AddTorque (transform.up * torque * torqueDirection * forceDirection * Time.deltaTime);
	}
}
