using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAiming : MonoBehaviour {
	// Since the location aiming should be aimed from the gun, thus turrect location location should be here
	// Use this for initialization

	public float turrentRotationSpeed = 80f;
	public float gunElevSpeed = 1f;
	private Vector3 turret_euler_offset;
	private Vector3 camera_scroll_euler_offset;
	private Vector3 third_person_cam_euler_offset;
	public float gunElevation = 20;
	public float gunDepression = -15;
	public float aimOffset = 50;
	[Tooltip ("TankBody")] public TankBody tankbody;
	[Tooltip ("CameraScrollControl")] public CameraScrollControl cameraScrollControl;
	[Tooltip ("TurretControl")] public TurretControl turretControl;
	[Tooltip ("ThirdPersonCam")] public ThirdPersonCam thirdPersonCam;
	[Tooltip ("AimCube")] public AimCube aimCube;
	void Start () {
		camera_scroll_euler_offset = cameraScrollControl.getTransform ().eulerAngles;
		turret_euler_offset = turretControl.getTransform ().eulerAngles;

	}
	void OnGUI(){
//		//var turret_euler_diff = turretControl.getTransform ().eulerAngles - turret_euler_offset;
//		GUI.Label (new Rect (10, 40, 1000, 200), "Turret euler: " + turretControl.getTransform().localEulerAngles);
//		var cam_euler_diff = cameraScrollControl.getTransform ().eulerAngles - camera_scroll_euler_offset;
//		//GUI.Label (new Rect (10, 80, 1000, 200), "Current Camera Local Euler:" + cam_euler_diff);

		GUI.Label (new Rect (10, 40, 1000, 200), turretControl.getTransform ().localRotation.eulerAngles.ToString());

	}
	void LateUpdate () {
		//turretControl.getTransform ().Rotate (Vector3.forward * turrentRotationSpeed);
		AngleEulerCalc();
	}
	// calcula angular difference between camera and turret orientation
	void AngleEulerCalc(){
		// First, map the target aiming location
		Vector3 diff_turret_cam = turretControl.getTransform().position - cameraScrollControl.getTransform().position;
		Vector3 aim_target = turretControl.getTransform ().position + diff_turret_cam;

		//aim_target*=2;
		//aimCube.getTransform ().position = aim_target;

		Vector3 targetV = aim_target - turretControl.getTransform ().position;
		targetV.Normalize ();
		Vector3 GunTarget = targetV;
		//GunTarget.z = 0;
		//GunTarget.x = 0;
		//GunTarget.y = 0;
		targetV.y = 0;
		var TurretRotation = Quaternion.LookRotation (targetV);
		Vector3 orientation_fix = new Vector3 (turret_euler_offset.x, turret_euler_offset.x, turret_euler_offset.z);
		TurretRotation *= Quaternion.Euler (orientation_fix);
		turretControl.getTransform ().rotation = Quaternion.RotateTowards(turretControl.getTransform().rotation,TurretRotation,turrentRotationSpeed*Time.deltaTime);

		Vector3 gun_orientation_fix = new Vector3 (camera_scroll_euler_offset.x-aimOffset,camera_scroll_euler_offset.x,camera_scroll_euler_offset.z);
		var GunRotation = Quaternion.LookRotation (GunTarget);
		GunRotation *= Quaternion.Euler (gun_orientation_fix);
		transform.rotation = Quaternion.RotateTowards (transform.rotation, GunRotation, 5);
		transform.localRotation = Quaternion.Euler(0f,	transform.localRotation.eulerAngles.y,transform.localRotation.eulerAngles.z);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
