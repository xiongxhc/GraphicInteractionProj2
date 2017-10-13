using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScrollControl : MonoBehaviour {
	public float stepSize = 60f;
	[Tooltip ("ThirdPersonCam")] public ThirdPersonCam thirdPersonCam;
	private float pivitAngle;
	// Use this for initialization
	void Start () {
		// calculate initial pivit angle
		var diffToCenter = transform.position - thirdPersonCam.getTransform().position;
		pivitAngle = -Mathf.Atan (diffToCenter.y/diffToCenter.x)*Mathf.Rad2Deg;

	}
	
	// Update is called once per frame
	void Update () {
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
	public Vector3 getPivitPoint(){
		var y = thirdPersonCam.getWorldPosition ().y + (transform.position - thirdPersonCam.getWorldPosition ()).magnitude * Mathf.Sin (pivitAngle);
		return new Vector3 (thirdPersonCam.getWorldPosition ().x,y,thirdPersonCam.getWorldPosition ().z);
	}
	public Vector3 getAimTarget(){
		return getPivitPoint () * 2 - transform.position;
	}
	public float getDiffYTOCenter(){
		// try calculate elevation diff
		// difference: y axis is O, x axis is a. tan(theta) = O/A, theta = atan(O/A)
		var diff_vector = thirdPersonCam.getWorldPosition() - transform.position;
		var diff_x = Mathf.Sqrt(Mathf.Pow(diff_vector.x,2f)+Mathf.Pow(diff_vector.z,2f));
		float y_angle = Mathf.Rad2Deg * Mathf.Atan(diff_vector.y/diff_x);
		return y_angle;
	}
	void LateUpdate(){
		if (Vector3.Distance (transform.position, thirdPersonCam.getWorldPosition ()) > (stepSize * Input.GetAxis ("Mouse ScrollWheel"))) {
			// update camera locaiton
			transform.position = Vector3.MoveTowards (transform.position, thirdPersonCam.getWorldPosition (), stepSize * Input.GetAxis ("Mouse ScrollWheel"));
		}
			
	}
}
