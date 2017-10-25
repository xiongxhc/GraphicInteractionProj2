using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameCanvasScript : MonoBehaviour {
	public Slider HealthSlider;                        
	public Image HealthFillImage;                      
	public Color FullHealthColor = Color.green;
	public Slider ReloadSlider;                        
	public Image ReloadFillImage;                      
	public Color ReloadColor = Color.red;
	public Text HealthText;
	public Text EnermyRemainingText;

	[Tooltip ("TankControl")] public TankControl tankControl;
	[Tooltip ("shoot")] public shoot shootControl;
	[Tooltip ("GameController")] public GameController gameController;
	// Use this for initialization
	void Start () {
		HealthFillImage.color = FullHealthColor;
		ReloadFillImage.color = ReloadColor;
		ReloadSlider.maxValue = 100;
	}
	
	// Update is called once per frame
	void Update () {
		EnermyRemainingText.text = gameController.countRemainingEnermy ().ToString();
		if(HealthSlider.enabled)HealthSlider.maxValue = tankControl.getHealthScript ().MaxHealth;
		if(HealthSlider.enabled)HealthSlider.value = tankControl.getHealthScript ().getCurrentHealth();
		if(HealthSlider.enabled)HealthText.text = tankControl.getHealthScript ().getCurrentHealth().ToString() + "/" + tankControl.getHealthScript ().MaxHealth.ToString();
		if(ReloadSlider.enabled)ReloadSlider.value = 100 - (shootControl.getshootCooldownCounter() / shootControl.shootCooldown) * 100;
	}
	public void enableBars(){
		HealthSlider.gameObject.SetActive(true);
		ReloadSlider.gameObject.SetActive(true);
	}
	public void disableBars(){
		HealthSlider.gameObject.SetActive(false);
		ReloadSlider.gameObject.SetActive(false);
	}
	public void setBarEnable(bool state){
		HealthSlider.gameObject.SetActive(state);
		ReloadSlider.gameObject.SetActive(state);
	}
}
