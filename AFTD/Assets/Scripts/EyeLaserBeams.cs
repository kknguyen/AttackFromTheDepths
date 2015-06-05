using UnityEngine;
using System.Collections;

public class EyeLaserBeams : MonoBehaviour {
	
	private float hitTime;
	private float destroyTime;
	void Awake()
	{
		hitTime = 0.45f;
		destroyTime = 1.417f;		
	}
	
	void Start()
	{	
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
		if (collis.gameObject.tag == "Player")
		{
			PlayerController player = collis.GetComponent<PlayerController>();
			Vector3 targetPosition = collis.transform.position - this.transform.position;
			targetPosition.z = 0;
			if (player.currentHealth > 0 && hitTime <= 0)
			{
				player.TakeDamage(10);
				hitTime = 0.45f;
			}
		}

	}
}
