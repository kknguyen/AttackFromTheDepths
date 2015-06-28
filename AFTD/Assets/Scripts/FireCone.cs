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
		if (collis.gameObject.tag == "enemy")
		{
			EnemyController enemyHealth = collis.GetComponent<EnemyController>();
			enemyHealth.wasHit = true;
			Vector3 targetPosition = collis.transform.position - this.transform.position;
			targetPosition.z = 0;
			if (enemyHealth.currentHealth > 0)
			{
				collis.transform.GetChild(0).gameObject.SetActive (true);
				collis.transform.Translate(targetPosition.normalized*2);
				enemyHealth.TakeDamage (10);
			}
		}
		if (collis.gameObject.tag == "theKing")
		{
			KingControl enemyHealth = collis.GetComponent<KingControl>();
			Vector3 targetPosition = collis.transform.position - this.transform.position;
			targetPosition.z = 0;
			if (enemyHealth.currentHealth > 0)
			{
				enemyHealth.TakeDamage(10);
			}
		}
	}
}
