using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shoot : MonoBehaviour
{
	public Bullet Shell;            
	public float MinLaunchForce = 15f; 
	public float MaxLaunchForce = 30f; 
	public float MaxChargeTime = 0.75f;
	public Transform FireTransform;    
	[Tooltip ("TurretControl")] public TurretControl turretControl;
	[Tooltip ("ShootingPointController")] public ShootingPointController shootingPointController;
	public ParticleSystem gunFireParticle;
	private float CurrentLaunchForce;  
	private float ChargeSpeed;         
	private float shootCooldownCounter = 0f;
	public AudioSource ShootingAudio;  
	public AudioClip FireClip; 
	public AudioSource MovingAudio;  
	public AudioClip MovingClip;
	public Slider Slider;
	public Color reloadColor = Color.red;
	public Image FillImage;
	[Tooltip ("TankNavS")] public TankNavS tank;
	public float shootCooldown = 1.0f;
	public void setShootSooldown(float cooldown){
		shootCooldown = cooldown;
	}
	private void OnEnable()
	{
		CurrentLaunchForce = MinLaunchForce;
	}

	private void Start()
	{
	}


	private void Update()
	{
		shootCooldownCounter -= Time.deltaTime;
		updateRealoadBar ();
		if (shootCooldownCounter <= 0.0f) {
			shootCooldownCounter = 0;
			//print ("Fire");
//			MovingAudio.clip = MovingClip;
//			MovingAudio.Play ();
		}
	}
	private void updateRealoadBar(){
		Slider.value = 100 - (shootCooldownCounter / shootCooldown) * 100;
		FillImage.color = reloadColor;
	}

	public void Fire()
	{
		if (shootCooldownCounter > 0)
			return;
		Bullet shellInstance = Instantiate (Shell, FireTransform.position, FireTransform.rotation);
		shellInstance.setVelocity(CurrentLaunchForce * FireTransform.forward + tank.GetComponent<Rigidbody>().velocity);
		shellInstance.setDamage (tank.getTankControl().shellDamage);
		CurrentLaunchForce = MinLaunchForce;
		shootCooldownCounter = shootCooldown;
		updateRealoadBar ();
		GameObject particleObject = Instantiate (gunFireParticle, shootingPointController.getTransform ().position, Quaternion.identity).gameObject;
		Destroy (particleObject, 1f);
		particleObject.GetComponent<Rigidbody>().velocity = GetComponentInParent<Rigidbody>().velocity;
		//particleObject.GetComponent<Rigidbody> ().AddForce (MinLaunchForce * FireTransform.forward * 10);
		GetComponentsInParent<AudioSource> () [1].Play();
	}
}