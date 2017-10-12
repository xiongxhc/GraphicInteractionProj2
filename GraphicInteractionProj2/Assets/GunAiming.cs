﻿using System.Collections;
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
	[Tooltip ("TankBody")] public TankBody tankbody;
	[Tooltip ("CameraScrollControl")] public CameraScrollControl cameraScrollControl;
	[Tooltip ("TurretControl")] public TurretControl turretControl;
	[Tooltip ("ThirdPersonCam")] public ThirdPersonCam thirdPersonCam;
	[Tooltip ("AimCube")] public AimCube aimCube;
	[Tooltip ("TankControl")] public TankControl tankControl;

	void Start () {
		gunRotYOffset = transform.localRotation.eulerAngles.y;
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

	Vector3 RotateAroundPoint(Vector3 originLocation, Vector3 rotatePiviot, Vector3 angles){
		return  Quaternion.Euler (angles) * (originLocation - rotatePiviot) + rotatePiviot; 
	}
	// calcula angular difference between camera and turret orientation
	void AngleEulerCalc(){
		//Vector3 screen_target = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, Camera.main.nearClipPlane)); 
		var pivit = cameraScrollControl.getPivitPoint ();
		Vector3 aim_target = pivit * 2 - cameraScrollControl.getTransform().position;
		//aim_target = RotateAroundPoint (screen_target, turretControl.getTransform ().position, new Vector3 (0,180,0));
		//p = c.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, c.nearClipPlane));
		aimCube.getTransform ().position = aim_target;
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
	}

	// Update is called once per frame
	void Update () {
		
	}
}