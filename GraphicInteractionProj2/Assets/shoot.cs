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

	private string FireButton;         
	private float CurrentLaunchForce;  
	private float ChargeSpeed;         

	public AudioSource ShootingAudio;  
	public AudioClip FireClip; 
	public AudioSource MovingAudio;  
	public AudioClip MovingClip;  
	[Tooltip ("TankControl")] public TankControl tank;
	public float ShootingTimePeriod = 1.0f;

	private void OnEnable()
	{
		CurrentLaunchForce = MinLaunchForce;
	}


	private void Start()
	{
		ShootingTimePeriod = 0;
		FireButton = "Fire";
	}


	private void Update()
	{
		ShootingTimePeriod -= Time.deltaTime;
		if (ShootingTimePeriod <= 0.0f) {
			ShootingTimePeriod = 0;
			//print ("Fire");
//			MovingAudio.clip = MovingClip;
//			MovingAudio.Play ();
		}
	}


	public void Fire()
	{
		if (ShootingTimePeriod > 0)
			return;
		Bullet shellInstance = Instantiate (Shell, FireTransform.position, FireTransform.rotation);
		shellInstance.setVelocity(CurrentLaunchForce * FireTransform.forward + tank.GetComponent<Rigidbody>().velocity);
		shellInstance.setDamage (tank.shellDamage);
		CurrentLaunchForce = MinLaunchForce;
		ShootingTimePeriod = 1.0f;
	}
}