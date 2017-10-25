using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TankControl : MonoBehaviour {


	public float shellDamage = 20;
	public float tankArmor = 20; // tank armor. Will negate damage. if tank armor is greater then shell damage, shell will bounce.
	public ParticleSystem gunFireParticle;
	private float inclineOffset;
	[Tooltip ("GunAiming")] public GunAiming gunAiming;
	[Tooltip ("TankTrack")] public TankTrack leftTrack;
	[Tooltip ("TankTrack")] public TankTrack rightTrack;
	[Tooltip ("TankBody")] public TankBody tankBody;
	[Tooltip ("TurretControl")] public TurretControl turretControl;
	[Tooltip ("shoot")] public shoot shootScript;
	[Tooltip ("Health")] public Health health;
	[Tooltip ("ShootingPointController")] public ShootingPointController shootingPointController;
	public Canvas HeathBar;
	public Canvas ReloadBar;
	void Start () {
		//transform.rotation = transform.rotation *= Quaternion.Euler (0, -90, 0);
		//transform.Rotate (-90, 0, 0);
		inclineOffset = transform.localRotation.eulerAngles.x;
	}
	public void setCooldown(float c){
		shootScript.shootCooldown = c; 
	}
	public float getCooldown(){
		return shootScript.shootCooldown; 
	}
	public Health getHealthScript(){
		return health;
	}
	public void setDamage(float Damage){
		shellDamage = Damage;
	}
	public void AimTarget(Vector3 target){
		gunAiming.setAimingTarget (target);
	}
	public void setArmor(float a){
		tankArmor = a;
	}
	public float getArmor(){
		return tankArmor;
	}
	public TurretControl getTurretControl(){
		return turretControl;
	}
	public void setDamager(float d){
		shellDamage = d;
	}
	public void disableOnTankUIBar(){
		HeathBar.enabled = false;
		ReloadBar.enabled = false;
	}
	public void enableOnTankUIBar(){
		HeathBar.enabled = true;
		ReloadBar.enabled = true;
	}
	public void Fire(){
		shootScript.Fire ();
		if (gunFireParticle == null)
			return;
	}

	// Update is called once per frame


	void OnCollisionExit(Collision collision){
	}

	void FixedUpdate(){
		
	}


	public Vector3 getWorldPosition(){
		return transform.position;
	}
	public Transform getTransform(){
		return transform;
	}
	public float getIncline(){
		return transform.localEulerAngles.x - inclineOffset;
	}
	void Update(){

	}
//	void updateHealth(){
//
//	}
}
