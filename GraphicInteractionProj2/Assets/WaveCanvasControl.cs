using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveCanvasControl : MonoBehaviour {
	public UnityEngine.UI.Text MoneyText;
	public UnityEngine.UI.Text PlayerFirepowerText;
	public UnityEngine.UI.Text PlayerArmorText;
	public UnityEngine.UI.Text PlayerHealthText;
	public UnityEngine.UI.Text PlayerFireCooldownText;

	public UnityEngine.UI.Text PlayerFirepowerCostText;
	public UnityEngine.UI.Text PlayerArmorCostText;
	public UnityEngine.UI.Text PlayerHealthCostText;
	public UnityEngine.UI.Text PlayerFireCooldownCostText;

	public UnityEngine.UI.Text WaveText;
	public UnityEngine.UI.Text EnermyFirepowerText;
	public UnityEngine.UI.Text EnermyArmorText;
	public UnityEngine.UI.Text EnermyHealthText;
	public UnityEngine.UI.Text EnermyFireCooldownText;
	public UnityEngine.UI.Text EnermyNumText;
	[Tooltip ("GameController")] public GameController gameController;
	[Tooltip ("TankNavS")] public TankNavS playerTankNavS;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!gameObject.activeSelf)
			return;
		MoneyText.text = gameController.Money.ToString();
		PlayerFirepowerText.text = playerTankNavS.getTankControl ().shellDamage.ToString();
		PlayerArmorText.text = playerTankNavS.getTankControl ().tankArmor.ToString();
		PlayerHealthText.text = playerTankNavS.getTankControl ().getHealthScript().MaxHealth.ToString();
		PlayerFireCooldownText.text = playerTankNavS.getTankControl ().getCooldown().ToString();
	}

	public void NextWaveEvent(){
		gameController.startWave ();
	}
	public void BuyFirepower(){
		if (gameController.Money >= int.Parse (PlayerFirepowerCostText.text)) {
			gameController.Money -= int.Parse (PlayerFirepowerCostText.text);
			playerTankNavS.getTankControl ().shellDamage += 10;
		}
	}
	public void BuyHealth(){
		if (gameController.Money >= int.Parse (PlayerHealthCostText.text)) {
			gameController.Money -= int.Parse (PlayerHealthCostText.text);
			playerTankNavS.getTankControl ().getHealthScript ().setMaxHealth (playerTankNavS.getTankControl ().getHealthScript().MaxHealth + 10);
		}
	}
	public void BuyCooldown(){
		if (gameController.Money >= int.Parse (PlayerFireCooldownCostText.text)) {
			gameController.Money -= int.Parse (PlayerFireCooldownCostText.text);
			playerTankNavS.getTankControl ().setCooldown (playerTankNavS.getTankControl ().getCooldown () * 0.9f);
		}
	}
	public void BuyArmor(){
		if (gameController.Money >= int.Parse (PlayerArmorCostText.text)) {
			gameController.Money -= int.Parse (PlayerArmorCostText.text);
			playerTankNavS.getTankControl ().tankArmor += 10;
		}
	}


}
