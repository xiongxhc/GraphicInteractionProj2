﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public LayerMask TankMask;
	public ParticleSystem ExplosionParticles;       
	public float MaxDamage = 50f;                  
	public float ExplosionForce = 2.0f;            
	public float MaxLifeTime = 2f;                  
	public float ExplosionRadius = 3f;              


	private void Start()
	{
		Destroy(gameObject, MaxLifeTime);
	}
	public void setDamage(float damage){
		MaxDamage = damage;
	}
	public void setVelocity(Vector3 velocity){
		GetComponent<Rigidbody> ().velocity = velocity;
	}
	void OnCollisionEnter(Collision other)
	{	
		//Debug.Log ("Entering!");
		GetComponent<AudioSource> ().Play ();
		//Debug.Log (GetComponent<AudioSource> ().isPlaying);
		Health targetHealth = other.gameObject.GetComponentInParent<Health>();
		if(targetHealth)targetHealth.TakeDamage (MaxDamage);
		Destroy(Instantiate (ExplosionParticles, transform.position, Quaternion.identity).gameObject,3f);
		Destroy (gameObject);

	}
		
}

