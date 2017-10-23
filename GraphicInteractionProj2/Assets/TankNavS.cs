using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankNavS : MonoBehaviour {
	[Tooltip ("TankControl")] public TankControl tankControl;
	public UnityEngine.AI.NavMeshAgent nav;
	// Use this for initialization
	void Start () {
		
	}
	public TankControl getTankControl(){
		return tankControl;
	}
	public UnityEngine.AI.NavMeshAgent getNav(){
		return nav;
	}
	// Update is called once per frame
	void Update () {
		
	}
}
