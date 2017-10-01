using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_wheel : MonoBehaviour {

	public float view_value;
	private float posX;
	private float posY;
	private float posZ;
	[Tooltip ("GUN_camera")] public Gun_Camera guncamerascript;
	// Use this for initialization
	void Start () {
		Vector3 position = transform.position;
		posX = position.x;
		posY = position.y;
		posZ = position.z;
	}
	
	// Update is called once per frame
	void Update () {
		View_update ();
	}
	void View_update(){
		if (Input.GetAxis ("Mouse ScrollWheel") != 0) {
			//cube.transform.Translate (new Vector3 (0, 0, Input.GetAxis ("Mouse ScrollWheel") * Time.deltaTime * view_value));
			posX += Input.GetAxis ("Mouse ScrollWheel") * view_value;
			transform.position = new Vector3 (posX, posY, posZ);
		}
		if (Input.GetKeyUp (KeyCode.Space)) {
			guncamerascript.GunCamera_toggle ();
		}
	}

}
