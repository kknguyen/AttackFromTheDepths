using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KingControl : MonoBehaviour {

	public int startingHealth = 300;
	public int currentHealth;
	float movespeed;
	bool isDead;
	bool canMove;

	Vector2 zeroVel = new Vector2(0, 0);

	// Ability cooldown and timers
	float meleeAttackCD;
	float meleeAttackTime;
	float laserAttackCD;
	float laserAttackTime;
	float swordSummonAttackCD;
	float swordSummonAttackTime;
	float teleCD;
	float teleTime;
	int healLimit;
	int healCount;
	
	Vector2[] retreatPoints;

	// References
	Animator anim; 

	AudioSource enemySound;
	public AudioClip meleeAudio;
	public AudioClip laserAudio;
	public AudioClip swordSummonAudio;
	public AudioClip teleAudio;
	PlayerController thePlayerController;
	GameObject thePlayer;
	Rigidbody2D kingRB;

	void Awake()
	{
		retreatPoints = new Vector2[4];
		currentHealth = startingHealth;
		// set 4 corner retreat points
	}

	void Start()
	{
		anim = GetComponent<Animator>();
		enemySound = GetComponent<AudioSource>();
		kingRB = GetComponent<Rigidbody2D>();
		thePlayer = GameObject.FindGameObjectWithTag("Player");
		thePlayerController = thePlayer.GetComponent<PlayerController>();
	}

	void Update()
	{
		UpdateTimers();

		if (currentHealth <= 0)
		{
			Death();
		}
		else
		{
			KingAI();
		}
	}

	void KingAI()
	{
		// if low, teleport away and heal
		if (currentHealth <= 100 && healCount < healLimit && canMove)
		{
			healCount++;
			Retreat();
		}
		else if (DistanceFromPlayer() < 3 && meleeAttackTime <= 0 && canMove)
		{
			MeleeAttack ();
		}
		else if (DistanceFromPlayer() < 10 && (laserAttackTime <= 0 || swordSummonAttackTime <= 0) && canMove)
		{
			RandomRangedAttack();
		}
	}

	void Retreat()
	{
		Vector2 furthestLoc = new Vector2();
		float shortestDist = 100.0F;
		foreach (Vector2 i in retreatPoints)
		{
			if (Vector2.Distance(i, thePlayer.transform.position) < shortestDist)
			{
				shortestDist = Vector2.Distance(i, thePlayer.transform.position);
				furthestLoc = i;
			}
		}
		Teleport(furthestLoc);
		HealingStance();
	}

	void Teleport(Vector2 location)
	{
		teleTime = teleCD;
		anim.SetTrigger("teleport");
		this.transform.position = location;
	}

	void HealingStance()
	{
		canMove = false;
		for (int i = 0; i < 4; i++)
		{
			Invoke("Heal", 0.5F);
		}
		Invoke("SetMove", 2.5F);
	}

	void Heal()
	{
		//set up some kind of healing animation/particle
		currentHealth += 40;
	}

	void SetMove()
	{
		canMove = true;
	}

	void MeleeAttack()
	{
		meleeAttackTime = meleeAttackCD;
		anim.SetTrigger("melee");
		enemySound.clip = meleeAudio;
		enemySound.Play();
		thePlayerController.TakeDamage(20);
	}


	void RandomRangedAttack()
	{
		int i = Random.Range (0, 2);
		if(laserAttackTime <= 0 && swordSummonAttackTime <= 0)
		{
			if (i == 0)
				ShootLaser();
			else
				SummonSword();
		}
		else if (laserAttackTime <= 0 && swordSummonAttackTime > 0)
		{
			ShootLaser();
		}
		else if (laserAttackTime > 0 && swordSummonAttackTime <= 0)
		{
			SummonSword();
		}
	}


	void ShootLaser()
	{
		laserAttackTime = laserAttackCD;
		anim.SetTrigger("laser");
		enemySound.clip = laserAudio;
		enemySound.Play();
		//instantiate laser and set rotations
		//check collision on laser script
		
	}

	void SummonSword()
	{
		swordSummonAttackTime = swordSummonAttackCD;
		anim.SetTrigger("summon");
		enemySound.clip = swordSummonAudio;
		enemySound.Play();
		//instantiate sword and set rotations
		//check collision on sword script
	}

	void Death()
	{
		isDead = true;
		// do other death stuff
	}

	float DistanceFromPlayer()
	{
		return (Vector2.Distance (this.transform.position, thePlayer.transform.position));
	}

	void UpdateTimers()
	{
		meleeAttackTime -= Time.deltaTime;
		laserAttackTime -= Time.deltaTime;
		swordSummonAttackTime -= Time.deltaTime;
		teleTime -= Time.deltaTime;
	}

	public void TakeDamage(int amount)
	{
		currentHealth -= amount;
	}

	void StopMoving()
	{
		anim.SetBool("isWalking", false);
		kingRB.velocity = zeroVel;
	}

	void MoveTo(Vector2 loc)
	{
		anim.SetBool("isWalking", true);
		kingRB.velocity = loc; //not correct
		//implement logic
	}

}
