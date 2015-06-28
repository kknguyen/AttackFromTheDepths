using UnityEngine;
using System.Collections;

public class Meteors : MonoBehaviour 
{
	private float hitTime;
	private float destroyTime;

	void Awake()
	{
		hitTime = 0.5f;
		destroyTime = 4f;
	}

	void Start()
	{	
		Destroy (this.gameObject, destroyTime);
	}
	
	void Update()
	{
		hitTime -= Time.deltaTime;
	}
	
	void OnTriggerStay2D(Collider2D collis)
	{
		if (collis.gameObject.tag == "wall")
		{		
		}
		else if (collis.gameObject.tag == "theKing")
		{
			KingControl enemyHealth = collis.GetComponent<KingControl>();

			if (enemyHealth.currentHealth > 0 && hitTime <= 0)
			{
				enemyHealth.TakeDamage(10);
				hitTime = 0.5f;
			}
		}
	}
}
