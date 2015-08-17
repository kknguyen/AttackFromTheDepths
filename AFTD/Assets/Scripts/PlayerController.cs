using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public int startingHealth = 1000;
	public int currentHealth;
	public GameObject rightClick;

	// x variable for movement
	public float h;
	// y variable for movement
	public float v; 

	// Used for right click animation.
	public Vector2 mousePos;

	// Used to determine which way the player is facing.
	public Vector3 turnMouse;
	public Vector3 movement;
	private Animator anim;

	//Rigidbody2D needs to have "Fixed Angle" checked
	//so that the player doesn't spin around when clicking to move.
	private Rigidbody2D playerRigidbody2D;
	private BoxCollider2D playerBoxCollider2D;
	private int floorMask;
	private float camRayLength = 100f;
	private int floorLevel;
	public bool isDead;

	private float moveSpeed = 6f;
	public int moveSpeedCount = 0;
	private float attackDamage = 1f;
	public int attackDamageCount = 0;
	public bool acquiredBossKey = false;

	void Awake()
	{
		//floorLevel = int.Parse(Application.loadedLevelName);
		floorMask = LayerMask.GetMask("Floor");
		currentHealth = startingHealth;
	}

	void Start()
	{
		anim = GetComponent<Animator>();
		playerRigidbody2D = GetComponent<Rigidbody2D>();
		playerBoxCollider2D = GetComponent<BoxCollider2D>();
		mousePos = playerRigidbody2D.position;
	}

	void FixedUpdate()
	{
		if (Input.GetMouseButton(1)) 
		{
			mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			turnMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			if (Input.GetMouseButtonDown(1)) 
			{
				Instantiate(rightClick, mousePos, Quaternion.identity);
			}
			Turning();
		}

		h = (mousePos.x - transform.position.x);
		v = (mousePos.y - transform.position.y);
		//Fixes shaking issue where when you are really close to destination, it doesn't stop moving.
		//If you are close enough to the destination, it will get h and v to 0 to stop the shaking.
		if (h < 0.1 && h > -0.1 && v < 0.1 && v > -0.1) 
		{
			h = 0;
			v = 0;
		}
		Move(h, v);
		//Turning();
		Animating (h, v);
	}

	void Move(float h, float v)
	{
		movement.Set (h, v, 0f);
		movement = movement.normalized * moveSpeed * Time.deltaTime;
		playerRigidbody2D.MovePosition(transform.position + movement);
	}

	void Turning()
	{
		//Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		transform.rotation = Quaternion.LookRotation(Vector3.forward, turnMouse - transform.position);
	}

	public void TakeDamage(int amount)		// Add in Vector2 hitPoint parameter for hit particle animations later.
	{
		// If the enemy is dead...
		if (isDead)
			// No need to take damage so exit the function.
			return;

		// Play the hurt sound effect.
		// EnemyAudio.Play ();

		// Reduce the current health by the amount of damage sustained.
		currentHealth -= amount;
		if (currentHealth < 0)
			currentHealth = 0;

		// Set the position of the particle system to where the hit was sustained.
		// HitParticles.transform.position = hitPoint;

		// And play the particles.
		// HitParticles.Play();

		// If the current health is less than or equal to zero...
		if (currentHealth <= 0)
		{
			// The enemy is dead.
			OnDeath();
		}
	}

	void OnDeath()
	{
		Time.timeScale = 0f;
		isDead = true;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "upstairs") {
			floorLevel++;
			string floorString = floorLevel.ToString();
			Application.LoadLevel(floorString);
		}
	}

	public void ProcessPowerUp(string powerUp)
	{
		switch (powerUp) 
		{
			case "speedPowerUp":
				moveSpeed += 2f;
				moveSpeedCount += 1;
				Invoke ("ReduceSpeed", 5); 
				break;
			case "attackPowerUp":
				attackDamage += 2f;
				attackDamageCount += 1;
				Invoke ("ReduceAttack", 5);
				break;
			case "healthPowerUp":
				currentHealth += 20;
				if (currentHealth > startingHealth)
					currentHealth = startingHealth;
				break;
			case "bossKeyPowerUp":
				acquiredBossKey = true;
				break;
		}
	}

	void ReduceSpeed()
	{
		moveSpeed -= 2f;
		moveSpeedCount -= 1;
	}

	void ReduceAttack()
	{
		attackDamage -= 2f;
		attackDamageCount -= 1;
	}

	void Animating(float h, float v)
	{
		bool walking = h != 0f || v != 0f;
		anim.SetBool ("isWalking", walking);
	}
}
