using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
	public float StartingHealth = 100f;          
	//private ParticleSystem ExplosionParticles;   
	private float CurrentHealth;  
	private bool Dead;            

	private void Awake()
	{
		//ExplosionParticles = Instantiate(ExplosionPrefab).GetComponent<ParticleSystem>();

		//ExplosionParticles.gameObject.SetActive(false);
	}


	private void OnEnable()
	{
		CurrentHealth = StartingHealth;
		Dead = false;

	}


	public void TakeDamage(float amount)
	{
		CurrentHealth -= amount;
		if (CurrentHealth <= 0f && !Dead) {
			OnDeath ();
		}
	}

	private void OnDeath()
	{
		Dead = true;
		//ExplosionParticles.transform.position = transform.position;
		//ExplosionParticles.gameObject.SetActive (true);
		//ExplosionParticles.Play ();
		gameObject.SetActive (false);
	}
}