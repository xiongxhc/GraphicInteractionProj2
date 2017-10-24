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
	}
	public void BuyHealth(){
	}
	public void BuyCooldown(){
	}
	public void BuyArmor(){
	}


}
