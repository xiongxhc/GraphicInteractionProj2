using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretControl : MonoBehaviour {

	float PositionY;
	float PositionZ;
	float TargetPostionY;

	// Use this for initialization
	void Start () {
	}
	// Update is called once per frame
	void Update () {

	}
	public Transform getTransform(){
		return transform;
	}

	void Mouse_Update(){
//		float horizontal = Input.GetAxis("Mouse X") * movementSpeed;
//		rotY += horizontal * 3.0f;
//		transform.rotation = Quaternion.Euler (rotX, rotY, rotZ);
//		float vertical = Input.GetAxis("Mouse Y") * movementSpeed;
//		//Debug.Log (vertical);
//		//vertical = Mathf.Clamp (vertical, gunDepression, gunElevation);
//		Vector3 delta_y = Vector3.down * vertical * 3;
//
//		transform.Rotate (delta_y);
//
//		// clamp angles
//		var body_angle = tankbody.getWorldEulerAngles();
//		var y_offset = body_angle.x;
//		var diff = transform.rotation.eulerAngles.x - body_angle.x;
//		Debug.Log ("Current Local: " + transform.localRotation);
//		var clamp_x = Mathf.Clamp (transform.rotation.eulerAngles.x, gunDepression + y_offset, gunElevation + y_offset);
//		transform.eulerAngles = new Vector3(clamp_x,transform.eulerAngles.y,transform.eulerAngles.z);
//
//		update_location ();




	}
}
