using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	public float gravity = 9.8f;
	public TankNavS tank;
	public float randRefreshTimerSeconds = 1;
	public float randRange = 5;
	public string fireButton = "Fire";
	public int MoneyMultiplier = 10;
	private float randTimer = 0;
	private int waveCount = 0;
	private Vector3 randVector = new Vector3(0,0,0);
	private bool waveStarted = false;
	private bool waitingCanvas = false;
	public int Money = 0;
	private List<TankNavS> npctanks;
	[Tooltip ("WaveCanvasControl")] public WaveCanvasControl waveCanvasControl;
	[Tooltip ("SpawnpointScript")] public SpawnpointScript Spawnpoint1;
	[Tooltip ("SpawnpointScript")] public SpawnpointScript Spawnpoint2;
	[Tooltip ("SpawnpointScript")] public SpawnpointScript Spawnpoint3;
	[Tooltip ("SpawnpointScript")] public SpawnpointScript Spawnpoint4;
	private List<SpawnpointScript> spawnPoints;
	[Tooltip ("TankNavS")] public TankNavS playerTank;
	[Tooltip ("ThirdPersonCam")] public ThirdPersonCam thirdPersonCam;
	// Use this for initialization
	void Start () {
		npctanks = new List<TankNavS>  ();
		spawnPoints = new List<SpawnpointScript> ();
//		Sawnpoint1.SpawnTank (10, npctanks);
//		Sawnpoint2.SpawnTank (10, npctanks);
		spawnPoints.Add(Spawnpoint1);
		spawnPoints.Add(Spawnpoint2);
		spawnPoints.Add(Spawnpoint3);
		spawnPoints.Add(Spawnpoint4);
		waveSpawn ();
	}
	void OnGUI() {
		
	}
	private bool checkWaveComplete(){
		foreach (SpawnpointScript s in spawnPoints) {
			if (s.getRemainingSpawnNum() > 0)
				return false;
		}
		return npctanks.Count == 0 ? true : false;
	}
	public void startWave(){
		thirdPersonCam.showCrosshair = true;
		thirdPersonCam.cameraMove = true;
		Cursor.lockState = CursorLockMode.Locked;
		waveCanvasControl.gameObject.SetActive (false);
		waitingCanvas = false;
		waveStarted = true;
		foreach (SpawnpointScript s in spawnPoints) {
			s.startSpawn = true;
		}
	}
	public void tankDestroy(TankNavS tank){
		if (tank.name.Equals ("NavTank")) {
			//gameover event
			return;
		}
		npctanks.Remove (tank);
		Money += waveCount * MoneyMultiplier;
	}
	private void refreshRand(){
		randTimer -= Time.deltaTime;
		if (randTimer <= 0) {
			randVector = new Vector3 (Random.Range (-randRange, randRange), Random.Range (-randRange, randRange), Random.Range (-randRange, randRange));
			randTimer = randRefreshTimerSeconds;
		}
	}
	private void showWaveCanvas(){
		waitingCanvas = true;
		thirdPersonCam.showCrosshair = false;
		thirdPersonCam.cameraMove = false;
		Cursor.lockState = CursorLockMode.None;
		waveCanvasControl.gameObject.SetActive (true);
		waveCanvasControl.EnermyArmorText.text = Spawnpoint1.spawnArmor.ToString();
		waveCanvasControl.EnermyFireCooldownText.text = Spawnpoint1.spawnFireCooldown.ToString();
		waveCanvasControl.EnermyFirepowerText.text = Spawnpoint1.spawnFirepower.ToString();
		waveCanvasControl.EnermyHealthText.text = Spawnpoint1.spawnHealth.ToString();
		waveCanvasControl.WaveText.text = waveCount.ToString ();
		waveCanvasControl.EnermyNumText.text = countSpawn ().ToString();
	}
	public int countSpawn(){
		int counter = 0;
		foreach(SpawnpointScript s in spawnPoints){
			counter += s.getRemainingSpawnNum ();
		}
		return counter;
	}

	private void waveSpawn(){
		waveCount++;
		foreach(SpawnpointScript s in spawnPoints){
			s.startSpawn = false;
		}
		switch (waveCount) {
		case 1:
			setSpawnStates (25, 20, 40, 5);
			Spawnpoint2.SpawnTank (1, npctanks);
			showWaveCanvas ();
			break;
		case 2:
			setSpawnStates (30, 30, 60, 5);
			Spawnpoint1.SpawnTank (1,npctanks);
			Spawnpoint2.SpawnTank (1,npctanks);
			showWaveCanvas ();
			break;
		case 3:
			setSpawnStates (40, 40, 80, 4);
			Spawnpoint1.SpawnTank (1,npctanks);
			Spawnpoint2.SpawnTank (1,npctanks);
			Spawnpoint3.SpawnTank (1,npctanks);
			showWaveCanvas ();
			break;
		case 4:
			setSpawnStates (60, 40, 100, 4);
			Spawnpoint1.SpawnTank (1,npctanks);
			Spawnpoint2.SpawnTank (1,npctanks);
			Spawnpoint3.SpawnTank (1,npctanks);
			Spawnpoint4.SpawnTank (1,npctanks);
			showWaveCanvas ();
			break;
		case 5:
			setSpawnStates (80, 50, 120, 3);
			Spawnpoint1.SpawnTank (2,npctanks);
			Spawnpoint2.SpawnTank (2,npctanks);
			Spawnpoint3.SpawnTank (1,npctanks);
			Spawnpoint4.SpawnTank (1,npctanks);
			showWaveCanvas ();
			break;
		case 6:
			setSpawnStates (100, 50, 150, 3);
			Spawnpoint1.SpawnTank (2,npctanks);
			Spawnpoint2.SpawnTank (2,npctanks);
			Spawnpoint3.SpawnTank (2,npctanks);
			Spawnpoint4.SpawnTank (2,npctanks);
			showWaveCanvas ();
			break;
		default:
			setSpawnStates (waveCount*2*10, waveCount*10, waveCount*3*10, 2);
			Spawnpoint1.SpawnTank (waveCount/2,npctanks);
			Spawnpoint2.SpawnTank (waveCount/2,npctanks);
			Spawnpoint3.SpawnTank (waveCount/2,npctanks);
			Spawnpoint4.SpawnTank (waveCount/2,npctanks);
			showWaveCanvas ();
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
		if (waitingCanvas)
			return;
		if (waveStarted == true && checkWaveComplete () && waitingCanvas == false) {
			waveSpawn ();
		}
		refreshRand ();
		Physics.gravity = new Vector3(0, -gravity, 0);
		if (Input.GetKey(KeyCode.Escape) || waitingCanvas)
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
