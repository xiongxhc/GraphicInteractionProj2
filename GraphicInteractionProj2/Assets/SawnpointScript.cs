using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawnpointScript : MonoBehaviour {
	private Quaternion initalQuaternion = Quaternion.Euler (-90, 0, 0);
	public TankNavS tank ;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public Transform getTransforn(){
		return transform;
	}

	public void SpawnTank(int num,List<TankNavS> tankList){
		tankList.Add(Instantiate (tank,transform.position,initalQuaternion));
	}
}
