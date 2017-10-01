using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Top_mouse_follow : MonoBehaviour {

	float PositionY;
	float PositionZ;
	float TargetPostionY;
	public GameObject tank;

	private float rotX;
	private float rotY;
	private float rotZ;
	private float y_offset = 270;
	public float movementSpeed = 5.0f;
	public float mouseSensetivity = 1.0f;
	public float rotateSensetivity = 1.0f;
	public float gunElevation = 20;
	public float gunDepression = -15;
	// Use this for initialization
	void Start () {

		update_location ();
	}
	void update_location(){
		Vector3 rot = transform.eulerAngles;
		rotX = rot.x;
		rotY = rot.y;
		rotZ = rot.z;
	
	}
	// Update is called once per frame
	void Update () {
		Mouse_Update ();
		
	}
	void Mouse_Update(){

		float horizontal = Input.GetAxis("Mouse X") * movementSpeed;
		rotY += horizontal * 3.0f;
		transform.rotation = Quaternion.Euler (rotX, rotY, rotZ);
		float vertical = Input.GetAxis("Mouse Y") * movementSpeed;
		//Debug.Log (vertical);
		//vertical = Mathf.Clamp (vertical, gunDepression, gunElevation);
		Vector3 delta_y = Vector3.down * vertical * 3;

		transform.Rotate (delta_y);

		// clamp angles
		var clamp_x = Mathf.Clamp (transform.rotation.eulerAngles.x, gunDepression + y_offset, gunElevation + y_offset);
		transform.eulerAngles = new Vector3(clamp_x,transform.eulerAngles.y,transform.eulerAngles.z);

		update_location ();




	}
}
