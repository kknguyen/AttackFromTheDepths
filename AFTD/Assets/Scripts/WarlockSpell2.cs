using UnityEngine;
using System.Collections;

public class WarlockSpell2 : MonoBehaviour
{

	private float destroyTime = 2;

	void Start ()
	{
		Destroy(this.gameObject, destroyTime);
	}
	
	
	void Update ()
	{
		
	}
	
	void OnTriggerEnter2D(Collider2D collis)
	{
		if (collis.gameObject.tag == "wall")
		{
			Destroy (this.gameObject);
		}
		else if (collis.gameObject.tag == "Player")
		{
			PlayerController playerHealth = collis.GetComponent<PlayerController>();
			if (playerHealth.currentHealth > 0)
			{
				Destroy (this.gameObject);
				playerHealth.TakeDamage (20);
				print(playerHealth.currentHealth);
			}
		}
	}
}
