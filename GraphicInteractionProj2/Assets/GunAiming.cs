using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAiming : MonoBehaviour {
	// Since the location aiming should be aimed from the gun, thus turrect location location should be here
	// Use this for initialization

	public float turrentRotationSpeed = 80f;
	public float gunElevSpeed = 1f;
	private float gunRotYOffset;
	public float gunElevation = 20;
	public float gunDepression = -15;
	private Vector3 aim_target = new Vector3(0,0,0); 
	[Tooltip ("TankBody")] public TankBody tankbody;
	[Tooltip ("CameraScrollControl")] public CameraScrollControl cameraScrollControl;
	[Tooltip ("TurretControl")] public TurretControl turretControl;
	[Tooltip ("ThirdPersonCam")] public ThirdPersonCam thirdPersonCam;
	[Tooltip ("TankControl")] public TankControl tankControl;
	[Tooltip ("TurretColliderControl")] public TurretColliderControl turretColliderControl;

	void Start () {
		gunRotYOffset = transform.localRotation.eulerAngles.y;
	}
	void OnGUI(){
	}
	void LateUpdate () {
		//turretControl.getTransform ().Rotate (Vector3.forward * turrentRotationSpeed);
		AngleEulerCalc();
	}

	Vector3 RotateAroundPoint(Vector3 originLocation, Vector3 rotatePiviot, Vector3 angles){
		return  Quaternion.Euler (angles) * (originLocation - rotatePiviot) + rotatePiviot; 
	}
	public void setAimingTarget(Vector3 targetPosition){
		aim_target = targetPosition;
	}
	void AngleEulerCalc(){
		//aimCube.getTransform ().position = aim_target;
		// Turret Rotation
		var turret_curr_rotation = turretControl.getTransform().localRotation;
		turretControl.getTransform ().LookAt (aim_target);
		turretControl.getTransform ().Rotate (Vector3.up,-90);
		turretControl.getTransform ().Rotate (Vector3.right,-90);
		turretControl.getTransform().localRotation = Quaternion.Euler(0,0,turretControl.getTransform().localRotation.eulerAngles.z);
		turretControl.getTransform ().localRotation = Quaternion.RotateTowards (turret_curr_rotation, turretControl.getTransform ().localRotation, turrentRotationSpeed * Time.deltaTime);
		// Gun elevation rotation
		var gun_priv_rot = transform.localRotation;
		transform.LookAt(aim_target);
		//transform.Rotate (Vector3.up,-90);
		transform.Rotate (Vector3.forward,90);
		//Debug.Log (gunRotYOffset);
		transform.localRotation = Quaternion.Euler (0,Mathf.Clamp(transform.localRotation.eulerAngles.y,gunRotYOffset-gunElevation,gunRotYOffset-gunDepression),0);
		//transform.localRotation = Quaternion.Euler (0,transform.localRotation.eulerAngles.y,0);
		transform.localRotation = Quaternion.RotateTowards (gun_priv_rot,transform.localRotation,gunElevSpeed);
		turretColliderControl.getTransform ().localRotation = Quaternion.Euler(0,turretControl.getTransform ().localRotation.eulerAngles.z,0);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
