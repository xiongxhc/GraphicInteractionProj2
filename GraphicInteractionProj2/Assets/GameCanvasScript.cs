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
		HealthSlider.maxValue = tankControl.getHealthScript ().MaxHealth;
		HealthSlider.value = tankControl.getHealthScript ().getCurrentHealth();
		HealthText.text = tankControl.getHealthScript ().getCurrentHealth().ToString() + "/" + tankControl.getHealthScript ().MaxHealth.ToString();
		ReloadSlider.value = 100 - (shootControl.getshootCooldownCounter() / shootControl.shootCooldown) * 100;
	}
}
