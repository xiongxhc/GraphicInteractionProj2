using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Top_mouse_follow : MonoBehaviour {

	Vector2 LastMousePosition;
	float PositionY;
	float PositionZ;
	float TargetPostionY;
	public GameObject tank;

	private float rotX;
	private float rotY;
	private float rotZ;
	public float movementSpeed = 5.0f;
	public float mouseSensetivity = 1.0f;
	public float rotateSensetivity = 1.0f;

	// Use this for initialization
	void Start () {
		Vector3 rot = transform.rotation.eulerAngles;
		rotX = rot.x;
		rotY = rot.y;
		rotZ = rot.z;

		print ("-----------------------------------------------------------");
		print (rotZ);
		print ("-----------------------------------------------------------");
		LastMousePosition = Input.mousePosition;
	}
	
	// Update is called once per frame
	void Update () {
		Mouse_Update ();
		
	}
	void Mouse_Update(){

		float horizontal = (Input.mousePosition.x - LastMousePosition.x) * 0.1f;
		//print (Input.mousePosition.x);
		//print (LastMousePosition.x);

		rotZ += horizontal * 3.0f;
		//rotZ = TargetPostionY;
		//float vertical = (Input.mousePosition.y - LastMousePosition.y) * 0.1f;
		//rotZ += vertical * 2.0f;

		//print (LastMousePosition);

		LastMousePosition = Input.mousePosition;

		//TargetPostionY = Mathf.MoveTowardsAngle (rotY, TargetPostionY, 270.0f * Time.deltaTime);

		transform.rotation = Quaternion.Euler (rotX, rotY, rotZ);
		//transform.Rotate (0, rotY, 0);

	}
}
