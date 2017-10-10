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
		if (Input.GetButtonDown (FireButton)) {
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