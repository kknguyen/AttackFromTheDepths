using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDScript : MonoBehaviour
{
	PlayerController thePlayerController;
	PlayerAbilities theAbilities;
	Slider healthBar;
	Text healthValue;

	KingControl theKing;
	Slider bossHealthBar;
	Text bossHealthValue;
	
	Image ability1;
	Text cdText1;
	Slider cdSlider1;
	
	Image ability2;
	Text cdText2;
	Slider cdSlider2;

	Image ability3;
	Text cdText3;
	Slider cdSlider3;
	
	
	RectTransform powerUpPanel;
	
	public RectTransform moveSpeedPowerPrefab;
	public RectTransform attackDamagePowerPrefab;
	public RectTransform bossKeyPrefab;
	
	
	RectTransform moveSpeedPassive;
	RectTransform attackDamagePassive;
	RectTransform bossKeyPassive;
	
	private bool showMoveSpeed = false;
	private bool showAttackDamage = false;
	private bool showBossKey = false;
	
	// Use this for initialization
	void Start ()
	{
		GameObject thePlayer = GameObject.FindGameObjectWithTag("Player");
		thePlayerController = thePlayer.GetComponent<PlayerController>();
		theAbilities = thePlayer.GetComponentInChildren<PlayerAbilities>();
		
		healthBar = GameObject.FindGameObjectWithTag("healthBar").GetComponent<Slider>();
		healthValue = healthBar.GetComponentInChildren<Text>();

		theKing = GameObject.FindGameObjectWithTag("theKing").GetComponent<KingControl>();

		bossHealthBar = GameObject.FindGameObjectWithTag("bossHealthBar").GetComponent<Slider>();
		bossHealthValue = bossHealthBar.GetComponentInChildren<Text>();

		// Hides the boss health bar until the player initiates the boss fight.
		this.bossHealthBar.GetComponent<CanvasGroup>().alpha = 0;

		ability1 = GameObject.FindGameObjectWithTag("ability1").GetComponent<Image>();
		cdSlider1 = ability1.GetComponentInChildren<Slider>();
		cdText1 = cdSlider1.GetComponentInChildren<Text>();
		
		ability2 = GameObject.FindGameObjectWithTag("ability2").GetComponent<Image>();
		cdSlider2 = ability2.GetComponentInChildren<Slider>();
		cdText2 = cdSlider2.GetComponentInChildren<Text>();

		ability3 = GameObject.FindGameObjectWithTag("ability3").GetComponent<Image>();
		cdSlider3 = ability3.GetComponentInChildren<Slider>();
		cdText3 = cdSlider3.GetComponentInChildren<Text>();

		powerUpPanel = GameObject.FindGameObjectWithTag("powerUpPanel").GetComponent<RectTransform>();
		
		
	}
	
	
	// Update is called once per frame
	void Update () {
		
		if (theAbilities.attack1Time <= 0)
		{
			cdText1.text = "";
			cdSlider1.value = 0;
		}
		else
		{
			cdText1.text = theAbilities.attack1Time.ToString("F1");
			cdSlider1.value = 100 * (theAbilities.attack1Time/theAbilities.attack1CD);
		}
		
		if (theAbilities.attack2Time <= 0)
		{
			cdText2.text = "";
			cdSlider2.value = 0;
		}
		else
		{
			cdText2.text = theAbilities.attack2Time.ToString("F1");
			cdSlider2.value = 100 * (theAbilities.attack2Time/theAbilities.attack2CD);
		}

		if (theAbilities.attack3Time <= 0)
		{
			cdText3.text = "";
			cdSlider3.value = 0;
		}
		else
		{
			cdText3.text = theAbilities.attack3Time.ToString("F1");
			cdSlider3.value = 100 * (theAbilities.attack3Time/theAbilities.attack3CD);
        }
        
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
		if (thePlayerController.acquiredBossKey && !showBossKey)
		{
			bossKeyPassive = Instantiate(bossKeyPrefab, transform.position, Quaternion.identity) as RectTransform;
			bossKeyPassive.transform.SetParent (powerUpPanel.transform, true);
			showBossKey = true;
		}
		
		healthBar.value = thePlayerController.currentHealth;		
		healthValue.text = thePlayerController.currentHealth + "/" + thePlayerController.startingHealth;

		bossHealthBar.value = theKing.currentHealth;		
		bossHealthValue.text = "The King's HP: " + theKing.currentHealth + "/" + theKing.startingHealth;
		
	}
	
	
}