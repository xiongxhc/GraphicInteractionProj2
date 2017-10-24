using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnpointScript : MonoBehaviour {
	private int maxSpawnInstance = 20;
	public float minSpawnCoolDown = 1; // spawn a tank every 5 seconds if possible
	private float timeTracker = 0;
	private int spawnNum = 0;
	private Quaternion spawnQuaternion = Quaternion.Euler(-90,-90,0);
//	private List<string> colliderRecord;
	private bool isColliding = false;
	private List<TankNavS> mainTrackList;
	public TankNavS tank ;
	public float spawnFirepower = 20f;
	public float spawnArmor = 5f;
	public float spawnHealth = 10f;
	public float spawnFireCooldown = 5f;
	public bool startSpawn = false;
	// Use this for initialization
	void Start () {
		//colliderRecord = new List<string> ();
	}
	void OnTriggerEnter(Collider other){
		
	}
	void OnTriggerStay(Collider other){
		isColliding = true;
	}
	void OnTriggerExit(Collider other){

	}
	public int getRemainingSpawnNum(){
		return spawnNum;
	}
	void FixedUpdate(){
		isColliding = false;
	}
	// Update is called once per frame
	void Update () {
		timeTracker -= timeTracker > 0 ? Time.deltaTime : 0;
		if (startSpawn && timeTracker <= 0  && spawnNum > 0 && mainTrackList.Count < maxSpawnInstance && !isColliding) {
			spawnNum -= 1;
			TankNavS newTank = Instantiate (tank, transform.position, Quaternion.identity);
			newTank.getTankControl ().setArmor (spawnArmor);
			newTank.getTankControl ().setDamage (spawnFirepower);
			newTank.getTankControl ().getHealthScript().setMaxHealth (spawnHealth);
			newTank.getTankControl ().setCooldown (spawnFireCooldown);
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
