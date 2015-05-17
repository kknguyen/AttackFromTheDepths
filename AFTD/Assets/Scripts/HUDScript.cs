using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDScript : MonoBehaviour
{
	PlayerController thePlayerController;
	Slider healthBar;
	Image ability1;
	Image ability2;
	RectTransform powerUpPanel;

	public RectTransform moveSpeedPowerPrefab;



	// Use this for initialization
	void Start ()
	{
		GameObject thePlayer = GameObject.FindGameObjectWithTag("Player");
		thePlayerController = thePlayer.GetComponent<PlayerController>();
		healthBar = GameObject.FindGameObjectWithTag("healthBar").GetComponent<Slider>();
		ability1 = GameObject.FindGameObjectWithTag("ability1").GetComponent<Image>();
		ability2 = GameObject.FindGameObjectWithTag("ability2").GetComponent<Image>();
		powerUpPanel = GameObject.FindGameObjectWithTag("powerUpPanel").GetComponent<RectTransform>();

	}

	
	// Update is called once per frame
	void Update () {
		if (thePlayerController.attackDamagePower)
		{

		}
		if (thePlayerController.moveSpeedPower)
		{
			RectTransform test = Instantiate(moveSpeedPowerPrefab, powerUpPanel.transform.position, Quaternion.identity) as RectTransform;
			test.transform.SetParent (this.transform, true);
			print ("movespeed");
		}

	}
}
