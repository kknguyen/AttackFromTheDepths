using UnityEngine;
using System.Collections;

public class FireBallClones : MonoBehaviour {

	public float destroyTime;
	int abilitySpeed = 15;

	// Use this for initialization
	void Start () 
	{
		Destroy (this.gameObject, destroyTime);
		GameObject thePlayer = GameObject.Find ("Player1");
		this.GetComponent<Rigidbody2D>().velocity = thePlayer.transform.up * abilitySpeed;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnTriggerEnter2D(Collider2D collis)
	{
		if (collis.gameObject.tag == "wall") 
		{
			Debug.Log ("hit wall");
			Destroy (this.gameObject);
		}
		else if (collis.gameObject.tag == "enemy") 
		{
			EnemyController enemyHealth = collis.GetComponent<EnemyController>();
			Debug.Log ("hit enemy");
			if (enemyHealth.currentHealth > 0)
				Destroy (this.gameObject);

			enemyHealth.TakeDamage (1);
			Debug.Log (enemyHealth.currentHealth);

		}
	}
}
