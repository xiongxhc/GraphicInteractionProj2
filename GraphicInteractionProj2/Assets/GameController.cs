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
	private Vector3 randVector = new Vector3(0,0,0);

	private List<TankNavS> npctanks;
	[Tooltip ("SawnpointScript")] public SawnpointScript Sawnpoint1;
	[Tooltip ("SawnpointScript")] public SawnpointScript Sawnpoint2;
	[Tooltip ("TankNavS")] public TankNavS playerTank;
	// Use this for initialization
	void Start () {
		npctanks = new List<TankNavS>  ();
		Sawnpoint1.SpawnTank (2, npctanks);
		Sawnpoint2.SpawnTank (10, npctanks);
	}
	private void refreshRand(){
		randTimer -= Time.deltaTime;
		if (randTimer <= 0) {
			randVector = new Vector3 (Random.Range (-randRange, randRange), Random.Range (-randRange, randRange), Random.Range (-randRange, randRange));
			randTimer = randRefreshTimerSeconds;
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
			//npctank.Fire ();
		}
	}

}
