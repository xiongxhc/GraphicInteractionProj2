using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScrollControl : MonoBehaviour {
	float stepSize = 20f;
	[Tooltip ("ThirdPersonCam")] public ThirdPersonCam thirdPersonCam;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void LateUpdate () {
	}
	public Vector3 getWorldLocation(){
		return transform.position;
	}
	public Transform getTransform(){
		return transform;
	}
	public Vector3 getDiffVectorToCenter(){
		return thirdPersonCam.getWorldPosition() - transform.position;
	}
	public float getDiffYTOCenter(){
		// try calculate elevation diff
		// difference: y axis is O, x axis is a. tan(theta) = O/A, theta = atan(O/A)
		var diff_vector = thirdPersonCam.getWorldPosition() - transform.position;
		var diff_x = Mathf.Sqrt(Mathf.Pow(diff_vector.x,2f)+Mathf.Pow(diff_vector.z,2f));
		float y_angle = Mathf.Rad2Deg * Mathf.Atan(diff_vector.y/diff_x);
		return y_angle;
	}
	void FixedUpdate(){
		if(Vector3.Distance(transform.position,thirdPersonCam.getWorldPosition()) > (stepSize*Input.GetAxis ("Mouse ScrollWheel")))
			transform.position = Vector3.MoveTowards (transform.position, thirdPersonCam.getWorldPosition (), stepSize * Input.GetAxis ("Mouse ScrollWheel"));
	}
}
