using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawnpointScript : MonoBehaviour {
	private int maxSpawnInstance = 7;
	public float minSpawnCoolDown = 1; // spawn a tank every 5 seconds if possible
	private float timeTracker = 0;
	private int spawnNum = 0;
	private Quaternion spawnQuaternion = Quaternion.Euler(-90,-90,0);
	private List<Collider> colliderRecord;
	private List<TankNavS> mainTrackList;
	public TankNavS tank ;
	// Use this for initialization
	void Start () {
		colliderRecord = new List<Collider> ();
	}
	void OnTriggerEnter(Collider other){
		if (!colliderRecord.Contains (other))
			colliderRecord.Add (other);
	}
	void OnTriggerExit(Collider other){
		colliderRecord.Remove (other);
	}
	// Update is called once per frame
	void Update () {
		timeTracker -= timeTracker > 0 ? Time.deltaTime : 0;
		if (timeTracker <= 0  && spawnNum > 0 && mainTrackList.Count < maxSpawnInstance && colliderRecord.Count == 0) {
			spawnNum -= 1;
			TankNavS newTank = Instantiate (tank, transform.position, Quaternion.identity);
			newTank.getNav ().enabled = true;
			newTank.getTankControl ().transform.localRotation = spawnQuaternion;
			newTank.setAIControl ();
			mainTrackList.Add(newTank);
			timeTracker = minSpawnCoolDown;
		}
	}


	public Transform getTransforn(){
		return transform;
	}

	public void SpawnTank(int num,List<TankNavS> tankList){
		mainTrackList = tankList;
		spawnNum = num;
	}
}
