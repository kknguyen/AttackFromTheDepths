using UnityEngine;
using System.Collections;


public class EnemyAI : MonoBehaviour {

	private Transform player;               	// Reference to the player's position.
	private PlayerController playerHealth;      // Reference to the player's health.
	private EnemyController enemyHealth;        // Reference to this enemy's health.

	// Enemy pathfinding
	private Vector2 zeroVelocity;
	private int maxRaycast;						// Maximum Raycast distance
	private int patrolRaycast;					// Raycast length for enemy patrolling
	private bool patrolling;
	private float waitTime;

	private Vector3 playerPosition;
	private Vector2 playerDirection;
	private float xDifChase;
	private float yDifChase;

	private float speed;
	private int wall;
	private int playerLayer;
	private float distance;
	private bool stun;
	private float stunTime;
	private Vector3 enemyStart;
	private int maximumRange;		// Max units the enemy can move from its starting point.
	private float minX;
	private float maxX;
	private float minY;
	private float maxY;
	private Vector2 patrolDirection;	// The direction that the enemy will patrol towards.
	private float wallDistance;
	private float playerDistance;

	void Awake ()
	{
		speed = 2;
		stun = false;
		wall = 1 << 8;
		playerLayer = 1 << 9;
		zeroVelocity = new Vector2 (0, 0);
		maxRaycast = 10;
		patrolling = false;
		waitTime = 3;
		maximumRange = 20;
		patrolRaycast = 2;
	}

	void Start ()
	{
		// Set up the references.
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		playerHealth = GameObject.FindGameObjectWithTag ("Player").GetComponent <PlayerController>();
		enemyHealth = GetComponent <EnemyController> ();
		newPatrolStartPoint ();
		newPatrolDirection ();

	}



	void Update ()
	{

		distance = Vector2.Distance (playerPosition, transform.position);
		playerPosition = GameObject.FindGameObjectWithTag ("Player").transform.position;
		xDifChase = playerPosition.x - transform.position.x;
		yDifChase = playerPosition.y - transform.position.y;
		playerDirection = new Vector2 (xDifChase, yDifChase);

		// If the enemy and the player have health left...
		if (enemyHealth.currentHealth > 0 /* && playerHealth.currentHealth > 0*/) {
			if (patrolling) {
				patrol ();
			} else {
				Debug.Log (seePlayer ());
				chasePlayer ();
			}
		}
		// Otherwise...
		else 
		{
			this.GetComponent<Rigidbody2D> ().velocity = zeroVelocity;
		}

	}

	void patrol()
	{
		if (seePlayer() && !seeWall ())
		{
			patrolling = false;
		}
		else
		{
			while(patrolSeeWall(patrolDirection))
			{
				if(seePlayer () && !seeWall ())
					break;
				newPatrolDirection ();
				print (patrolDirection);
			}

			this.GetComponent<Rigidbody2D> ().velocity = patrolDirection.normalized * speed;
			print (this.GetComponent<Rigidbody2D> ().velocity);
			print ("We are patrolling.");
		}
	}

	void newPatrolDirection()
	{
		float xDifPatrol = Random.Range (minX,maxX) - transform.position.x;
		float yDifPatrol = Random.Range (minY,maxY) - transform.position.y;
		patrolDirection = new Vector2 (xDifPatrol, yDifPatrol);
	}

	void newPatrolStartPoint()
	{
		enemyStart = this.transform.position;
		minX = enemyStart.x - maximumRange;
		maxX = enemyStart.x + maximumRange;
		minY = enemyStart.y - maximumRange;
		maxY = enemyStart.y + maximumRange;
	}

	void chasePlayer()
	{
		Debug.Log ("Entered chaseplayer()");
		if (distance < 3 && (!seeWall() && seePlayer () || seeWall () && seePlayer() && playerDistance < wallDistance))
		{
			this.GetComponent<Rigidbody2D> ().velocity = zeroVelocity;
			playerHealth.TakeDamage(2);
			print (playerHealth.currentHealth);
		}
		else if (!seeWall() && seePlayer () || seeWall () && seePlayer() && playerDistance < wallDistance)
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
		wallDistance = Physics2D.Raycast (transform.position, playerDirection, maxRaycast, wall).distance;
		return Physics2D.Raycast (transform.position, playerDirection, maxRaycast, wall);
	}
	bool seePlayer()
	{
		playerDistance = Physics2D.Raycast (transform.position, playerDirection, maxRaycast, playerLayer).distance;
		return Physics2D.Raycast (transform.position, playerDirection, maxRaycast, playerLayer);

	}

	bool patrolSeeWall(Vector2 enemyDirection)
	{
		return Physics2D.Raycast (transform.position, enemyDirection, patrolRaycast, wall);
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
			newPatrolStartPoint();	// Do we want to create a new roaming boundary every time we enter patrol??
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
