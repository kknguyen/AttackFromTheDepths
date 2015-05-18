using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDScript : MonoBehaviour
{
	PlayerController thePlayerController;
	Slider healthBar;
	Text healthValue;
	Image ability1;
	Image ability2;
	RectTransform powerUpPanel;

	public RectTransform moveSpeedPowerPrefab;
	public RectTransform attackDamagePowerPrefab;


	RectTransform moveSpeedPassive;
	RectTransform attackDamagePassive;

	private bool showMoveSpeed = false;
	private bool showAttackDamage = false;

	// Use this for initialization
	void Start ()
	{
		GameObject thePlayer = GameObject.FindGameObjectWithTag("Player");
		thePlayerController = thePlayer.GetComponent<PlayerController>();
		healthBar = GameObject.FindGameObjectWithTag("healthBar").GetComponent<Slider>();
		healthValue = GetComponentInChildren<Text>();
		ability1 = GameObject.FindGameObjectWithTag("ability1").GetComponent<Image>();
		ability2 = GameObject.FindGameObjectWithTag("ability2").GetComponent<Image>();
		powerUpPanel = GameObject.FindGameObjectWithTag("powerUpPanel").GetComponent<RectTransform>();


	}

	
	// Update is called once per frame
	void Update () {
		if (thePlayerController.moveSpeedCount == 0)
			showMoveSpeed = false;
		if (thePlayerController.attackDamageCount == 0)
			showAttackDamage = false;


		if (thePlayerController.attackDamageCount >= 1 && !showAttackDamage)
		{
			attackDamagePassive = Instantiate(attackDamagePowerPrefab, transform.position, Quaternion.identity) as RectTransform;
			attackDamagePassive.transform.SetParent (powerUpPanel.transform, true);
			showAttackDamage = true;
		}
		if (thePlayerController.moveSpeedCount >= 1 && !showMoveSpeed)
		{
			moveSpeedPassive = Instantiate(moveSpeedPowerPrefab, transform.position, Quaternion.identity) as RectTransform;
			moveSpeedPassive.transform.SetParent (powerUpPanel.transform, true);
			showMoveSpeed = true;
		}

		healthBar.value = thePlayerController.currentHealth;

		healthValue.text = thePlayerController.currentHealth + "/" + thePlayerController.startingHealth;

	}


}
