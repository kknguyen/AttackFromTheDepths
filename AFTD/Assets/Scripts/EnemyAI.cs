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
	private float xDifChase;
	private float yDifChase;
	private float xDifPatrol;
	private float yDifPatrol;

	private float speed;
	private int wall;
	private int playerLayer;
	private float distance;
	private bool stun;
	private float stunTime;
	private Vector3 enemyStart;

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
		enemyStart = this.transform.position;
	}



	void Update ()
	{

		distance = Vector2.Distance (playerPosition, transform.position);
		playerPosition = GameObject.FindGameObjectWithTag ("Player").transform.position;
		xDifChase = playerPosition.x - transform.position.x;
		yDifChase = playerPosition.y - transform.position.y;
		playerDirection = new Vector2 (xDifChase, yDifChase);

		// If the enemy and the player have health left...
		if(enemyHealth.currentHealth > 0 /* && playerHealth.currentHealth > 0*/)
		{
			if (patrolling)
			{
				patrol ();
			}
			else
			{
				Debug.Log (seePlayer ());
				chasePlayer ();
			}
		}
		// Otherwise...
	}

	void patrol()
	{
		if (seePlayer() && !seeWall ())
		{
			patrolling = false;
		}
		else
		{
			xDifPatrol = enemyStart.x - transform.position.x;
			yDifPatrol = enemyStart.y - transform.position.y;
			Vector2 enemyDirection = new Vector2 (xDifPatrol, yDifPatrol);
			if (xDifPatrol < 0.2 && yDifPatrol < 0.2)
				this.GetComponent<Rigidbody2D> ().velocity = zeroVelocity;
			else
				this.GetComponent<Rigidbody2D> ().velocity = enemyDirection.normalized * speed;
		}
	}

	void chasePlayer()
	{
		Debug.Log ("Entered chaseplayer()");
		if (distance < 3 && !seeWall() && seePlayer ())
		{
			this.GetComponent<Rigidbody2D> ().velocity = zeroVelocity;
		}
		else if (!seeWall() && seePlayer ())
		{
				this.GetComponent<Rigidbody2D> ().velocity = playerDirection.normalized * speed;
				waitTime = 3;
				print ("im chasing");
		}
		else
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
		print ("i saw player");
		return Physics2D.Raycast (transform.position, playerDirection, maxRaycastPlayer, playerLayer);

	}

	void enemyWait()
	{
		if (waitTime > 0)
		{
			waitTime -= Time.deltaTime;
			print ("im waiting");
		}
		else
		{
			print ("enter patrol");
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
