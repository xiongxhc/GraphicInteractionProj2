using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScrollControl : MonoBehaviour {
	public float stepSize = 60f;
	public float aimDistanceMultipler = 100f;
	[Tooltip ("ThirdPersonCam")] public ThirdPersonCam thirdPersonCam;
	[Tooltip ("GunCamera")] public GunCamera gunCamera;
	private float pivitAngle;
	private float z_diff;
	private Camera cam;
	private Rigidbody rb;
	// Use this for initialization
	void Start () {
		// calculate initial pivit angle
		var diffToCenter = transform.position - thirdPersonCam.getTransform().position;
		pivitAngle = -Mathf.Atan (diffToCenter.y/diffToCenter.x)*Mathf.Rad2Deg;
		z_diff = transform.localPosition.z;
		cam = GetComponent<Camera> ();
		rb = GetComponent<Rigidbody> ();
	}
	void Fixedupdate(){
	}
	// Update is called once per frame
	void OnTriggerEnter(Collision collisionInfo)
	{

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
		var pivit = getPivitPoint();
		var deltaUnit = (pivit - transform.position).normalized;
		return getPivitPoint () + deltaUnit * aimDistanceMultipler;
	}
	public Vector3 getAimTargetGUIThirdPerson(){
		return cam.WorldToScreenPoint (getAimTarget ());
	}
	public Vector3 getAimTargetGUIFirstPerson(){
		return gunCamera.getCam().WorldToScreenPoint (getAimTarget ());
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
