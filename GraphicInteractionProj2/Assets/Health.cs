using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
	public float StartingHealth = 50f;          

	private float CurrentHealth;  
	private bool Dead;            
	public Slider Slider;                        
	public Image FillImage;                      
	public Color FullHealthColor = Color.green;  

	private void Awake()
	{
	}
	public float getArmor(){
		return GetComponentInParent<TankControl> ().getArmor();
	}

	private void OnEnable()
	{
		CurrentHealth = StartingHealth;
		Dead = false;
		SetHealthUI();


	}

	private void SetHealthUI()
	{
		// Adjust the value and colour of the slider.
		Slider.value = CurrentHealth;
		FillImage.color = FullHealthColor;
	}

	public void TakeDamage(float amount)
	{
		CurrentHealth -= amount;
		SetHealthUI();
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