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
	
//	void Update()
//	{
//		hitTime -= Time.deltaTime;
//	}
//	
//	void OnTriggerStay2D(Collider2D collis)
//	{
//		if (collis.gameObject.tag == "wall")
//		{		
//		}
//		else if (collis.gameObject.tag == "enemy")
//		{
//			EnemyController enemyHealth = collis.GetComponent<EnemyController>();
//
//			if (enemyHealth.currentHealth > 0 && hitTime <= 0)
//			{
//				collis.transform.GetChild(0).gameObject.SetActive (true);
//				enemyHealth.wasHit = true;
//				enemyHealth.TakeDamage (200);
//				hitTime = 0.5f;
//			}
//		}
//	}
}
