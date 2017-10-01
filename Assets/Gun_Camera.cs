using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Camera : MonoBehaviour {

	public Camera main_camera;
	public Camera this_camera;

	// Use this for initialization
	void Start () {
		this_camera = GetComponent<Camera> ();
		this_camera.enabled = false;
	}
	
	public void GunCamera_toggle(){
		main_camera.enabled = !main_camera.enabled;
		this_camera.enabled = !this_camera.enabled;
	}

}
