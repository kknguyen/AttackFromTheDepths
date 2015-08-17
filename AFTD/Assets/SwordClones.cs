using UnityEngine;
using System.Collections;

public class SwordClones : MonoBehaviour
{
	private float destroyTime;
	private float abilitySpeed;
	private int arrowDamage;
	
	void Awake()
	{
		abilitySpeed = 15f;
		destroyTime = 10;
		arrowDamage = 15;
	}
	
	void Start()
	{
		Destroy(this.gameObject, destroyTime);
		GameObject thePlayer = GameObject.FindGameObjectWithTag("Player");
		
		this.GetComponent<Rigidbody2D>().velocity = (thePlayer.transform.position - transform.position).normalized * abilitySpeed;
		transform.rotation = Quaternion.LookRotation(Vector3.forward, thePlayer.transform.position - transform.position);
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
		else if (collis.gameObject.tag == "Player")
		{
			PlayerController playerHealth = collis.GetComponent<PlayerController>();
			if (playerHealth.currentHealth > 0)
			{
				Destroy (this.gameObject);
				playerHealth.TakeDamage (15);
				print(playerHealth.currentHealth);
			}
		}
	}
}
