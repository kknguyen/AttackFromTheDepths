﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public int startingHealth = 1000;
	public int currentHealth;

	private Vector3 movement;
	private Animator anim;
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
	}

	void FixedUpdate()
	{
		float h = Input.GetAxisRaw("Horizontal");
		float v = Input.GetAxisRaw("Vertical");
		Move(h, v);
		Turning();
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
		//var mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		//Quaternion rot = Quaternion.LookRotation (transform.position - mousePosition, Vector3.forward);
		//transform.rotation = rot;
		//transform.eulerAngles = new Vector3 (0, 0, transform.eulerAngles.z);

		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);
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
