using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	public float gravity = 9.8f;
	public TankControl tank;
	public float randRefreshTimerSeconds = 1;
	public float randRange = 5;
	public string fireButton = "Fire";
	private float randTimer = 0;
	private Vector3 randVector = new Vector3(0,0,0);

	private List<TankControl> npctanks;
	[Tooltip ("TankControl")] public TankControl playerTankControl;
	// Use this for initialization
	void Start () {
		npctanks = new List<TankControl>  ();
		//npctanks.Add(Instantiate (tank,new Vector3(20,20,0),Quaternion.Euler(-90,0,0)));
		//tank.gameObject.AddComponent<AudioListener> ();
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
			playerTankControl.Fire ();
		}
		playerTankControl.accelerate (forceDirection);
		playerTankControl.turn (torqueDirection, forceDirection);

		foreach(TankControl npctank in npctanks){
			npctank.AimTarget (playerTankControl.getWorldPosition() + randVector);
			npctank.Fire ();
		}
	}

}
