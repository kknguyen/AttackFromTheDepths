using UnityEngine;
using System.Collections;

public abstract class EnemyAI : MonoBehaviour
{
	protected Transform player;               	// Reference to the player's position.
	protected PlayerController playerHealth;    // Reference to the player's health.
	protected EnemyController enemyHealth;      // Reference to this enemy's health.
	protected float attackCooldown;
	protected float defaultCooldown = 1;
	

	// Enemy pathfinding
	protected bool patrolling;					// Is the enemy patrolling?
	protected float speed;						// Speed of the enemy
	protected float waitTime;					// How long does the enemy wait before patrolling
	protected float distance;					// Distance from the enemy to the player
	protected float wallDistance;				// Distance of the hit Raycast to wall
	protected float playerDistance;				// Distance of the hit Raycast to player

	// Raycast values
	protected int wallLayer;					// Layer mask for the wall objects
	protected int playerLayer;					// Layer mask for player
	protected int maxRaycast;					// Maximum Raycast distance for chasing
	protected float patrolRaycast;				// Maximum Raycast distance patrolling (wall collision)

	// Direction vectors for enemy movement
	protected Vector2 zeroVelocity;				// Velocity vector to stop enemy movement
	protected Vector2 patrolDirection;			// The direction that the enemy will patrol towards.
	protected Vector2 playerDirection;			// Player direction relative to the enemy
	protected Vector3 playerPosition;			// Player position in environment

	// Boundary set for enemy roam/patrolling
	protected Vector3 enemyStart;				// Location of enemy position when patrol state starts
	protected int maximumRange;					// Max units the enemy can move from its starting point.
	protected float minX;
	protected float maxX;
	protected float minY;
	protected float maxY;

	// Initialize variables
	protected virtual void Awake()
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
		attackCooldown = 0;
	}

	// Set up object references
	protected virtual void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
		playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent <PlayerController>();
		enemyHealth = GetComponent <EnemyController>();
		NewPatrolStartPoint();
		NewPatrolDirection();
	}

	protected virtual void Update()
	{
		attackCooldown -= Time.deltaTime;
		playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
		distance = Vector2.Distance(playerPosition, transform.position);
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

	protected virtual void ChasePlayer()
	{
		if (distance < 3 && ChaseCondition())
		{
			StopMoving();
			if (attackCooldown <= 0)
			{
				playerHealth.TakeDamage(enemyHealth.EnemyAttack());
				attackCooldown = defaultCooldown;
				print(playerHealth.currentHealth);
			}

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

	protected virtual void EnemyWait()
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

	protected virtual void Patrol()
	{
		if (SeePlayer() && !SeeWall())
		{
			patrolling = false;
		}
		else
		{
			while (PatrolSeeWall(patrolDirection))
			{
				if (SeePlayer() && !SeeWall())
					break;
				NewPatrolDirection();
			}
			this.GetComponent<Rigidbody2D>().velocity = patrolDirection.normalized * speed;
			Debug.Log(this.GetComponent<Rigidbody2D>().velocity);
		}
	}

	protected virtual void NewPatrolDirection()
	{
		float xDifPatrol = Random.Range(minX, maxX) - transform.position.x;
		float yDifPatrol = Random.Range(minY, maxY) - transform.position.y;
		patrolDirection = new Vector2(xDifPatrol, yDifPatrol);
	}

	protected virtual void NewPatrolStartPoint()
	{
		enemyStart = this.transform.position;
		minX = enemyStart.x - maximumRange;
		maxX = enemyStart.x + maximumRange;
		minY = enemyStart.y - maximumRange;
		maxY = enemyStart.y + maximumRange;
	}

	protected virtual void StopMoving()
	{
		this.GetComponent<Rigidbody2D>().velocity = zeroVelocity;
	}

	protected virtual bool ChaseCondition()
	{
		return (!SeeWall() && SeePlayer() || SeeWall() && SeePlayer() && playerDistance < wallDistance);
	}

	protected virtual bool SeeWall()
	{
		wallDistance = Physics2D.Raycast(transform.position, playerDirection, maxRaycast, wallLayer).distance;
		return Physics2D.Raycast(transform.position, playerDirection, maxRaycast, wallLayer);
	}
	protected virtual bool SeePlayer()
	{
		playerDistance = Physics2D.Raycast (transform.position, playerDirection, maxRaycast, playerLayer).distance;
		return Physics2D.Raycast(transform.position, playerDirection, maxRaycast, playerLayer);
	}

	protected virtual bool PatrolSeeWall(Vector2 enemyDirection)
	{
		return Physics2D.Raycast(transform.position, enemyDirection, patrolRaycast, wallLayer);
	}

	protected virtual void OnCollisionEnter2D(Collision2D playerHit)
	{
		if (playerHit.gameObject.tag == "Player")
		{

		}
	}
}
