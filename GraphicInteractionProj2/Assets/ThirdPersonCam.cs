using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour {

	public float stepSize = 12f;
	public float mouseSensetivity = 5f;
	public Texture2D crosshairOuter;
	public Texture2D crosshairInner;
	[Tooltip ("GunCamera")] public GunCamera guncamerascript;
	[Tooltip ("TankControl")] public TankControl tankControl;

	[Tooltip ("CameraScrollControl")] public CameraScrollControl cameraScrollControl;
	[Tooltip ("AimCube")] public AimCube aimCube;
	// Use this for initialization
	void Start () {
	}
	void OnGUI() {
//		float xMin = (Screen.width / 2) - (crosshairOuter.width / 2);
//		float yMin = (Screen.height / 2) - (crosshairOuter.height / 2);
//		GUI.DrawTexture(new Rect(xMin, yMin, crosshairOuter.width, crosshairOuter.height), crosshairOuter);
//		GUI.Label (new Rect (10, 40, 1000, 200), "Local camera rotaion:" + transform.localEulerAngles);
//		GUI.Label (new Rect (10, 80, 1000, 200), "World camera rotaion:" + transform.rotation.eulerAngles);
	}
	// Update is called once per frame
	void Update () {
		transform.position = tankControl.getWorldPosition ();
		if (Input.GetKeyUp (KeyCode.Space)) {
			guncamerascript.GunCamera_toggle ();
		}
	}
	public Vector3 getWorldPosition(){
		return transform.position;
	}
	void LateUpdate(){
		View_update ();
	}
	public Transform getTransform (){
		return transform;
	}
	void View_update(){
		//transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x, transform.localEulerAngles.y,transform.localEulerAngles.z + Input.GetAxis ("Mouse X") * mouseSensetivity);
		//
		transform.Rotate(Vector3.up * Input.GetAxis ("Mouse X") * mouseSensetivity, Space.World);
		transform.Rotate(Vector3.up * Input.GetAxis ("Mouse Y") * mouseSensetivity * -1);
		tankControl.AimTarget (cameraScrollControl.getAimTarget());
		aimCube.getTransform ().position = cameraScrollControl.getAimTarget ();
		//Debug.Log (cameraScrollControl.getAimTarget ());
	}

}
