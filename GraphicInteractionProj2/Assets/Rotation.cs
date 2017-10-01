using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour {

	public Rigidbody rb;
	public GameObject cube;


	Vector2 LastMousePosition;
	float PositionY;
	float PositionZ;
	float TargetPostionY;

	private float rotX;
	private float rotY;
	private float rotZ;
	public float movementSpeed = 5.0f;
	public float mouseSensetivity = 1.0f;
	public float rotateSensetivity = 1.0f;

	private float camX;
	private float camY;
	private float camZ;
	private float posX;
	private float posY;
	private float posZ;

	public bool space_down = false;
	public bool space_up = false;
	[Tooltip ("GUN_camera")] public Gun_Camera guncamerascript;


	// Use this for initialization
	void Start () {
		Vector3 rot = cube.transform.rotation.eulerAngles;
		rotX = rot.x;
		rotY = rot.y;
		rotZ = rot.z;
		Vector3 position = transform.position;
		posX = position.x;
		posY = position.y;
		posZ = position.z;
		Vector3 camera = cube.transform.rotation.eulerAngles;
		camX = camera.x;
		camY = camera.y;
		camZ = camera.z;


		LastMousePosition = Input.mousePosition;

		//transform.position = new Vector3 (transform.position.x,height,transform.position.z);

	}

	// Update is called once per frame
	void Update () {
		Mouse_Update ();
		//Keyboard_control();
		//Mouse_control ();
		//View_update();
		//	Mouse_control2 ();
		Space_detection();
		Space_control();
	}

	void Mouse_Update(){
		
		float horizontal = (Input.mousePosition.x - LastMousePosition.x) * 0.1f;
		//print (Input.mousePosition.x);
		//print (LastMousePosition.x);

		//TargetPostionY -= horizontal * 3.0f;
		rotZ += horizontal * 3.0f;

		float vertical = (Input.mousePosition.y - LastMousePosition.y) * 0.1f;
		camY += vertical * 2.0f;

		//print (LastMousePosition);

		LastMousePosition = Input.mousePosition;

		//TargetPostionY = Mathf.MoveTowardsAngle (rotY, TargetPostionY, 270.0f * Time.deltaTime);
		print(rotY);
		transform.rotation = Quaternion.Euler (rotX, rotY, rotZ);
		//cube.transform.rotation = Quaternion.Euler (camX, camY, camZ);
		print (cube.transform.rotation.eulerAngles);

		//transform.Rotate (0, rotY, 0);

	}
	void Keyboard_control(){
		Vector3 dir = new Vector3 ();

		if (Input.GetKey (KeyCode.W)) dir.x += 1.0f;
		if (Input.GetKey (KeyCode.S)) dir.x -= 1.0f;
		if (Input.GetKey (KeyCode.A)) dir.y -= 1.0f;
		if (Input.GetKey (KeyCode.D)) dir.y += 1.0f;

		//dir.Normalize ();
		//transform.rotation = camera.transform.rotation;
		transform.Translate (dir * movementSpeed * Time.deltaTime);
	}

	void Mouse_control(){
		rotZ -= Input.GetAxis ("Mouse X") * mouseSensetivity;
		rotY += Input.GetAxis ("Mouse Y") * -mouseSensetivity;
		transform.rotation = Quaternion.Euler (rotX, rotY, rotZ);
		//transform.rotation = Quaternion.Euler (camY, camX, camZ);
		print(camY);

	}



	void Mouse_control2(){
		if (Input.GetMouseButton (2)) {
			transform.Translate (Vector3.left * Input.GetAxis ("Mouse X"));
			transform.Translate (Vector3.right * Input.GetAxis ("Mouse Y") * -1);
		}
	}

	void Space_detection(){
		if(Input.GetKeyDown (KeyCode.Space)){
			space_down = true;
			space_up = false;

		}
		if (space_down == true) {
			if (Input.GetKeyUp (KeyCode.Space)) {
				space_down = true;
				space_up = true;
			}
		}

	}

	void Space_control(){
		if (space_down == true) {
			//fangda
			if (space_up == false) {
				guncamerascript.GunCamera_ON ();
				//transform.position = new Vector3 (camX + 20, camY, camZ);
			} else {
				guncamerascript.GunCamera_OFF ();
				//transform.position = new Vector3 (camX, camY, camZ);
				space_down = false;
			}

		} 
	}
}