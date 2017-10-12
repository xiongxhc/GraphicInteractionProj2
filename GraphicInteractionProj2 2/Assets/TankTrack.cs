using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankTrack : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	void OnCollisionEnter(Collision collision){
	}
	void OnCollisionStay(Collision collision){
	}
	void OnCollisionExit(Collision collision){
	}
	public Transform getTransform(){
		return transform;
	}
	// Update is called once per frame
	void Update () {
		
	}
}
