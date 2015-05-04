using UnityEngine;
using System.Collections;

public class FireBallClones : MonoBehaviour
{
	private float destroyTime;
	private int abilitySpeed = 15;

	void Awake()
	{
		destroyTime = 2;
	}

	void Start()
	{
		Destroy(this.gameObject, destroyTime);
		GameObject thePlayer = GameObject.Find("Player1");
		this.GetComponent<Rigidbody2D>().velocity = thePlayer.transform.up * abilitySpeed;
	}

	void Update()
	{

	}

	void OnTriggerEnter2D(Collider2D collis)
	{
		if (collis.gameObject.tag == "wall")
		{
			Destroy (this.gameObject);
		}
		else if (collis.gameObject.tag == "enemy")
		{
			EnemyController enemyHealth = collis.GetComponent<EnemyController>();
			if (enemyHealth.currentHealth > 0)
				Destroy (this.gameObject);
			enemyHealth.TakeDamage (1);
			Debug.Log (enemyHealth.currentHealth);
		}
	}
}
