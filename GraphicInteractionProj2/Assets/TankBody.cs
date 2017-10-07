using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBody : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public Transform getTransform(){
		return transform;
	}
	public Vector3 getWorldEulerAngles(){
		return transform.eulerAngles;
	}
	public Vector3 getLocalEulerAngles(){
		return transform.eulerAngles;
	}
}
