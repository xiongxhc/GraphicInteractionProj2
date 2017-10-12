using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shoot : MonoBehaviour
{
	public Rigidbody Shell;            
	public float MinLaunchForce = 15f; 
	public float MaxLaunchForce = 30f; 
	public float MaxChargeTime = 0.75f;
	public Transform FireTransform;    

	private string FireButton;         
	private float CurrentLaunchForce;  
	private float ChargeSpeed;         
	private bool Fired;

	public AudioSource ShootingAudio;  
	public AudioClip FireClip; 
	public AudioSource MovingAudio;  
	public AudioClip MovingClip;  


	private void OnEnable()
	{
		CurrentLaunchForce = MinLaunchForce;
	}


	private void Start()
	{
		FireButton = "Fire";
	}


	private void Update()
	{
		print ("Fire");
		MovingAudio.clip = MovingClip;
		MovingAudio.Play ();
		if (Input.GetButtonDown (FireButton)) {
			ShootingAudio.clip = FireClip;
			ShootingAudio.Play ();
			print ("Fire1");

			Fire ();
		}
	}


	private void Fire()
	{
		Fired = true;
		Rigidbody shellInstance = Instantiate (Shell, FireTransform.position, FireTransform.rotation) as Rigidbody;
		shellInstance.velocity = CurrentLaunchForce * FireTransform.forward;
		CurrentLaunchForce = MinLaunchForce;
	}
}