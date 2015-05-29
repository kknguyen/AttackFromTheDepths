using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyHP : MonoBehaviour
{
	EnemyController theEnemyController;
	Slider healthBar;

	void Start ()
	{
		theEnemyController = GetComponentInParent<KnightController>();
		if (theEnemyController == null) 
		{
			theEnemyController = GetComponentInParent<WarlockController>();
		}
		if(theEnemyController == null)
		{
			theEnemyController = GetComponentInParent<ArcherController>();
		}

		healthBar = GetComponentInChildren<Slider>();
		healthBar.maxValue = theEnemyController.startingHealth;
		this.gameObject.SetActive(false);

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (healthBar.value != theEnemyController.currentHealth)
		{
			healthBar.value = theEnemyController.currentHealth;
			Invoke("HideHp", 3f);
		}
		if (theEnemyController.isDead)
		{
			Destroy (this.gameObject);
		}
	}

	void HideHp()
	{
		this.gameObject.SetActive(false);
	}
}
