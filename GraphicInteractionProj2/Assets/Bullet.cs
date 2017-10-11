using System.Collections;
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

	private void OnTriggerEnter(Collider other)
	{	

				Collider[] colliders = Physics.OverlapSphere(transform.position,ExplosionRadius,TankMask);
				print ("Collsion Test : ");
				print (colliders.Length);

				for (int i = 0; i < colliders.Length; i++) {
					Rigidbody TargetRigidbody = colliders [i].GetComponent<Rigidbody> ();
					if (!TargetRigidbody) {
					print ("1");
						continue;
					}
					//TargetRigidbody.AddExplosionForce (ExplosionForce, transform.position, ExplosionRadius);
					Health targetHealth = TargetRigidbody.GetComponent<Health> ();
					print (targetHealth);

					if (!targetHealth) {
						continue;
					}
					//float damage = CalculateDamage (TargetRigidbody.position);
					targetHealth.TakeDamage (MaxDamage);
					print (targetHealth);
		
				}

//				if (colliders.Length > 0) {
//					return;
//				}


//		ExplosionParticles.transform.position = gameObject.transform.position;
//		ExplosionParticles.Play ();
//		ExplosionAudio.Play ();
//		Destroy (ExplosionParticles.gameObject, 5.0f);
		Destroy (gameObject);

	}


//	private float CalculateDamage(Vector3 targetPosition)
//	{
//		// Calculate the amount of damage a target should take based on it's position.
//		Vector3 explosionToTarget = targetPosition - transform.position;
//		float explosionDistance = explosionToTarget.magnitude;
//		float relativeDistance = (ExplosionRadius - explosionDistance) / ExplosionRadius;
//		float damage = relativeDistance * MaxDamage;
//		damage = Mathf.Max (0f, damage);
//		return damage;
//	}
}

