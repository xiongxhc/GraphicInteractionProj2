using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
	public float MaxHealth = 100f;          
	[Tooltip ("TankControl")] public TankControl tankControl;
	[Tooltip ("TankNavS")] public TankNavS tankNavS;
	[Tooltip ("GameController")] public GameController gameController;
	private float CurrentHealth;  
	private bool Dead;            
	public Slider Slider;                        
	public Image FillImage;                      
	public Color FullHealthColor = Color.green;  

	private void Awake()
	{
	}
	public float getArmor(){
		return tankControl.getArmor();
	}

	private void OnEnable()
	{
		CurrentHealth = MaxHealth;
		Dead = false;
		SetHealthUI();
	}
	public void setMaxHealth(float h){
		MaxHealth = h;
		CurrentHealth = MaxHealth;
		SetHealthUI ();
	}
	public void setCurrentHealth(float h){
		CurrentHealth = h > MaxHealth ? MaxHealth : h;
		SetHealthUI ();
	}
	public void restoreHeath(float h){
		CurrentHealth += h;
		CurrentHealth = CurrentHealth > MaxHealth ? MaxHealth : CurrentHealth;
		SetHealthUI ();
	}
	public void restoreHeathToMax(){
		CurrentHealth = MaxHealth;
		SetHealthUI ();
	}

	private void SetHealthUI()
	{
		// Adjust the value and colour of the slider.
		Slider.maxValue = MaxHealth;
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
		gameController.tankDestroy (tankNavS);
		//ExplosionParticles.transform.position = transform.position;
		//ExplosionParticles.gameObject.SetActive (true);
		//ExplosionParticles.Play ();
		Destroy(gameObject);
	}
}