using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	public float gravity = 9.8f;
	public TankNavS tank;
	public float randRefreshTimerSeconds = 1;
	public float randRange = 5;
	public string fireButton = "Fire";
	private float randTimer = 0;
	private int waveCount = 0;
	private Vector3 randVector = new Vector3(0,0,0);
	private bool waveStarted = false;
	private List<TankNavS> npctanks;
	[Tooltip ("SpawnpointScript")] public SpawnpointScript Spawnpoint1;
	[Tooltip ("SpawnpointScript")] public SpawnpointScript Spawnpoint2;
	[Tooltip ("SpawnpointScript")] public SpawnpointScript Spawnpoint3;
	[Tooltip ("SpawnpointScript")] public SpawnpointScript Spawnpoint4;
	private List<SpawnpointScript> spawnPoints;
	[Tooltip ("TankNavS")] public TankNavS playerTank;
	// Use this for initialization
	void Start () {
		npctanks = new List<TankNavS>  ();
//		Sawnpoint1.SpawnTank (10, npctanks);
		//Sawnpoint2.SpawnTank (10, npctanks);
		spawnPoints.Add(Spawnpoint1);
		spawnPoints.Add(Spawnpoint2);
		spawnPoints.Add(Spawnpoint3);
		spawnPoints.Add(Spawnpoint4);
	}
	private bool checkWaveComplete(){
		foreach (SpawnpointScript s in spawnPoints) {
			if (s.getRestSpawnNum() > 0)
				return false;
		}
		return npctanks.Count == 0 ? true : false;
	}
	public void tankDestroy(TankNavS tank){
		npctanks.Remove (tank);
	}
	private void refreshRand(){
		randTimer -= Time.deltaTime;
		if (randTimer <= 0) {
			randVector = new Vector3 (Random.Range (-randRange, randRange), Random.Range (-randRange, randRange), Random.Range (-randRange, randRange));
			randTimer = randRefreshTimerSeconds;
		}
	}
	private void waveSpawn(){
		waveCount++;
		switch (waveCount) {
		case 1:
			setSpawnStates (25, 20, 40, 5);
			Spawnpoint1.SpawnTank (1,npctanks);
			break;
		case 2:
			setSpawnStates (30, 30, 60, 5);
			Spawnpoint1.SpawnTank (1,npctanks);
			Spawnpoint2.SpawnTank (1,npctanks);
			break;
		case 3:
			setSpawnStates (40, 40, 80, 4);
			Spawnpoint1.SpawnTank (1,npctanks);
			Spawnpoint2.SpawnTank (1,npctanks);
			Spawnpoint3.SpawnTank (1,npctanks);
			break;
		case 4:
			setSpawnStates (60, 40, 100, 4);
			Spawnpoint1.SpawnTank (1,npctanks);
			Spawnpoint2.SpawnTank (1,npctanks);
			Spawnpoint3.SpawnTank (1,npctanks);
			Spawnpoint4.SpawnTank (1,npctanks);
			break;
		case 5:
			setSpawnStates (80, 50, 120, 3);
			Spawnpoint1.SpawnTank (2,npctanks);
			Spawnpoint2.SpawnTank (2,npctanks);
			Spawnpoint3.SpawnTank (1,npctanks);
			Spawnpoint4.SpawnTank (1,npctanks);
			break;
		case 6:
			setSpawnStates (100, 50, 150, 3);
			Spawnpoint1.SpawnTank (2,npctanks);
			Spawnpoint2.SpawnTank (2,npctanks);
			Spawnpoint3.SpawnTank (2,npctanks);
			Spawnpoint4.SpawnTank (2,npctanks);
			break;
		default:
			setSpawnStates (waveCount*2*10, waveCount*10, waveCount*3*10, 2);
			Spawnpoint1.SpawnTank (waveCount/2,npctanks);
			Spawnpoint2.SpawnTank (waveCount/2,npctanks);
			Spawnpoint3.SpawnTank (waveCount/2,npctanks);
			Spawnpoint4.SpawnTank (waveCount/2,npctanks);
			break;
		}
	}

	public void setSpawnStates(float firepower, float armor, float health, float cooldown){
		foreach (SpawnpointScript s in spawnPoints) {
			s.spawnFirepower = firepower;
			s.spawnArmor = armor;
			s.spawnHealth = health;
			s.spawnFireCooldown = cooldown;
		}
	}
	// Update is called once per frame
	void Update () {
		refreshRand ();
		Physics.gravity = new Vector3(0, -gravity, 0);
		if (Input.GetKey(KeyCode.Escape))
			Cursor.lockState = CursorLockMode.None;
		else
			Cursor.lockState = CursorLockMode.Locked;
		int torqueDirection = 0;
		int forceDirection = 0;
		if (Input.GetKey (KeyCode.A)) 
			torqueDirection = -1;
		if (Input.GetKey (KeyCode.D))
			torqueDirection = 1;
		if (Input.GetKey (KeyCode.W))
			forceDirection = 1;
		if (Input.GetKey (KeyCode.S))
			forceDirection = -1;
		if (Input.GetButtonDown (fireButton)) {
			playerTank.getTankControl().Fire ();
		}
		playerTank.accelerate (forceDirection);
		playerTank.turn (torqueDirection, forceDirection);
		foreach(TankNavS npctank in npctanks){
			npctank.getTankControl().AimTarget (playerTank.getTankControl().getWorldPosition() + randVector);
			npctank.getNav().SetDestination(playerTank.getTankControl().getWorldPosition());
			npctank.getTankControl().Fire ();
		}
	}

}
