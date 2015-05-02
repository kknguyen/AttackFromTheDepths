using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

	private Transform player;               	// Reference to the player's position.
	//PlayerHealth playerHealth;      			// Reference to the player's health.
	private EnemyController enemyHealth;        // Reference to this enemy's health.

	// Enemy pathfinding
	private Vector2 zeroVelocity;
	private int maxRaycastWall;						// Maximum Raycast distance
	private int maxRaycastPlayer;
	private bool patrolling;
	private float waitTime;

	private Vector3 playerPosition;
	private Vector2 playerDirection;
	private float xDif;
	private float yDif;
	private float speed;
	private int wall;
	private int playerLayer;
	private float distance;
	private bool stun;
	private float stunTime;

	void Awake ()
	{
		speed = 2;
		stun = false;
		wall = 1 << 8;
		playerLayer = 1 << 9;
		zeroVelocity = new Vector2 (0, 0);
		maxRaycastWall = 3;
		maxRaycastPlayer = 10;
		patrolling = false;
		waitTime = 3;
	}

	void Start ()
	{
		// Set up the references.
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		//playerHealth = player.GetComponent <PlayerHealth> ();
		enemyHealth = GetComponent <EnemyController> ();

	}



	void Update ()
	{
		// If the enemy and the player have health left...
		if(enemyHealth.currentHealth > 0 /* && playerHealth.currentHealth > 0*/)
		{
			if (patrolling)
			{
				patrol ();
			}
			else
			{
				chasePlayer ();
			}
		}
		// Otherwise...
		else
		{
			// ... disable the nav mesh agent.
			//nav.enabled = false;
		}

	}

	void patrol()
	{
		distance = Vector2.Distance (playerPosition, transform.position);
		if (distance < 10)
		{
			patrolling = false;
			Debug.Log ("chase now");
		}

	}

	void chasePlayer()
	{
		distance = Vector2.Distance (playerPosition, transform.position);
		playerPosition = GameObject.FindGameObjectWithTag ("Player").transform.position;

//		if (stunTime > 0)
//		{
//			stunTime -= Time.deltaTime;
//			Debug.Log ("reduce stun time");
//		}
//		else
//		{
//			stun = false;
//			Debug.Log (stun);
//		}
		if (distance < 3)
		{
			this.GetComponent<Rigidbody2D> ().velocity = zeroVelocity;
		}
		else if (distance >= 3 && distance < 10)
		{
			xDif = playerPosition.x - transform.position.x;
			yDif = playerPosition.y - transform.position.y;
			playerDirection = new Vector2 (xDif, yDif);

			if (!seeWall())
			{
				this.GetComponent<Rigidbody2D> ().velocity = playerDirection.normalized * speed;
				waitTime = 3;
				Debug.Log ("im chasing");
			}
			else
			{
				this.GetComponent<Rigidbody2D> ().velocity = zeroVelocity;
				enemyWait ();
			}
		}
		else if ( distance >= 10)
		{
			this.GetComponent<Rigidbody2D> ().velocity = zeroVelocity;
			enemyWait ();
		}
	}

	bool seeWall()
	{
		return Physics2D.Raycast (transform.position, playerDirection, maxRaycastWall, wall);
	}
	bool seePlayer()
	{
		return Physics2D.Raycast (transform.position, playerDirection, maxRaycastPlayer, playerLayer);
	}

	void enemyWait()
	{
		if (waitTime > 0)
		{
			waitTime -= Time.deltaTime;
			Debug.Log ("im waiting");
		}
		else
		{
			Debug.Log ("enter patrol");
			patrolling = true;
			waitTime = 3;
		}
	}


	void OnCollisionEnter2D (Collision2D playerHit)
	{
		if (playerHit.gameObject.tag == "Player")
		{
//			stun = true;
//			stunTime = 1;
//			Debug.Log (stun);
		}
	}

}
