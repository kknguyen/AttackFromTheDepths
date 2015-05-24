using UnityEngine;
using System.Collections;

public class FireCone : MonoBehaviour
{
	
	void Start()
	{
	
	}

	void Update()
	{
	}

	void OnTriggerEnter2D(Collider2D collis)
	{
		if (collis.gameObject.tag == "wall")
		{

		}
		else if (collis.gameObject.tag == "enemy")
		{
			EnemyController enemyHealth = collis.GetComponent<EnemyController>();
			enemyHealth.wasHit = true;
			Vector3 targetPosition = collis.transform.position - this.transform.position;
			targetPosition.z = 0;
			if (enemyHealth.currentHealth > 0)
			{
				GameObject.FindGameObjectWithTag("healthBarEnemy").SetActive(true);
				print ("hit with cone");
				collis.transform.Translate(targetPosition.normalized*4);
				enemyHealth.TakeDamage (1);
			}
		}
	}
}
