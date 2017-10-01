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
	

	public void GunCamera_ON(){
		main_camera.enabled = false;
		this_camera.enabled = true;
	}
	public void GunCamera_OFF(){
		main_camera.enabled = true;
		this_camera.enabled = false;
	}
}
