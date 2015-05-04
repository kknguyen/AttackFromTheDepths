using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
	private Transform player;               	// Reference to the player's position.
	private PlayerController playerHealth;      // Reference to the player's health.
	private EnemyController enemyHealth;        // Reference to this enemy's health.

	// Enemy pathfinding
	private bool patrolling;					// Is the enemy patrolling?
	private float speed;						// Speed of the enemy
	private float waitTime;						// How long does the enemy wait before patrolling
	private float distance;						// Distance from the enemy to the player
	private float wallDistance;					// Distance of the hit Raycast to wall
	private float playerDistance;				// Distance of the hit Raycast to player

	// Raycast values
	private int wallLayer;						// Layer mask for the wall objects
	private int playerLayer;					// Layer mask for player
	private int maxRaycast;						// Maximum Raycast distance for chasing
	private float patrolRaycast;					// Maximum Raycast distance patrolling (wall collision)

	// Direction vectors for enemy movement
	private Vector2 zeroVelocity;				// Velocity vector to stop enemy movement
	private Vector2 patrolDirection;			// The direction that the enemy will patrol towards.
	private Vector2 playerDirection;			// Player direction relative to the enemy
	private Vector3 playerPosition;				// Player position in environment

	// Boundary set for enemy roam/patrolling
	private Vector3 enemyStart;					// Location of enemy position when patrol state starts
	private int maximumRange;					// Max units the enemy can move from its starting point.
	private float minX;
	private float maxX;
	private float minY;
	private float maxY;

	// Initialize variables
	void Awake()
	{
		speed = 2;
		wallLayer = 1 << 8;
		playerLayer = 1 << 9;
		zeroVelocity = new Vector2 (0, 0);
		maxRaycast = 10;
		patrolling = false;
		waitTime = 3;
		maximumRange = 20;
		patrolRaycast = 1.5f;
	}

	// Set up object references
	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
		playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent <PlayerController>();
		enemyHealth = GetComponent <EnemyController>();
		NewPatrolStartPoint();
		NewPatrolDirection();
	}

	void Update()
	{
		playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
		distance = Vector2.Distance (playerPosition, transform.position);
		float xDifChase = playerPosition.x - transform.position.x;
		float yDifChase = playerPosition.y - transform.position.y;
		playerDirection = new Vector2(xDifChase, yDifChase);

		// If the enemy and the player have health left...
		if (enemyHealth.currentHealth > 0 /* && playerHealth.currentHealth > 0*/)
		{
			if (patrolling)
				Patrol();
			else
				ChasePlayer();
		}
		// Enemy is dead
		else
		{
			StopMoving();
		}
	}

	void ChasePlayer ()
	{
		if (distance < 3 && ChaseCondition())
		{
			StopMoving();
			playerHealth.TakeDamage(2);
		}
		else if (ChaseCondition())
		{
				this.GetComponent<Rigidbody2D>().velocity = playerDirection.normalized * speed;
				waitTime = 3;
		}
		else
		{
			StopMoving();
			EnemyWait();
		}
	}

	void EnemyWait()
	{
		if (waitTime > 0)
		{
			waitTime -= Time.deltaTime;
		}
		else
		{
			patrolling = true;
			NewPatrolStartPoint();	// Do we want to create a new roaming boundary every time we enter patrol??
			waitTime = 3;
		}
	}

	void Patrol()
	{
		if (SeePlayer() && !SeeWall())
		{
			patrolling = false;
		}
		else
		{
			while(PatrolSeeWall(patrolDirection))
			{
				if (SeePlayer() && !SeeWall())
					break;
				NewPatrolDirection();
			}
			this.GetComponent<Rigidbody2D>().velocity = patrolDirection.normalized * speed;
			Debug.Log(this.GetComponent<Rigidbody2D>().velocity);
		}
	}

	void NewPatrolDirection()
	{
		float xDifPatrol = Random.Range (minX,maxX) - transform.position.x;
		float yDifPatrol = Random.Range (minY,maxY) - transform.position.y;
		patrolDirection = new Vector2(xDifPatrol, yDifPatrol);
	}

	void NewPatrolStartPoint()
	{
		enemyStart = this.transform.position;
		minX = enemyStart.x - maximumRange;
		maxX = enemyStart.x + maximumRange;
		minY = enemyStart.y - maximumRange;
		maxY = enemyStart.y + maximumRange;
	}

	void StopMoving()
	{
		this.GetComponent<Rigidbody2D>().velocity = zeroVelocity;
	}

	bool ChaseCondition()
	{
		return (!SeeWall() && SeePlayer() || SeeWall() && SeePlayer() && playerDistance < wallDistance);
	}

	bool SeeWall()
	{
		wallDistance = Physics2D.Raycast(transform.position, playerDirection, maxRaycast, wallLayer).distance;
		return Physics2D.Raycast(transform.position, playerDirection, maxRaycast, wallLayer);
	}
	bool SeePlayer()
	{
		playerDistance = Physics2D.Raycast (transform.position, playerDirection, maxRaycast, playerLayer).distance;
		return Physics2D.Raycast (transform.position, playerDirection, maxRaycast, playerLayer);
	}

	bool PatrolSeeWall(Vector2 enemyDirection)
	{
		return Physics2D.Raycast (transform.position, enemyDirection, patrolRaycast, wallLayer);
	}

	void OnCollisionEnter2D(Collision2D playerHit)
	{
		if (playerHit.gameObject.tag == "Player")
		{

		}
	}
}
