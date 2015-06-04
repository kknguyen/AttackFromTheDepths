using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KingControl : MonoBehaviour {

	public int startingHealth = 130;
	public int currentHealth;
	float movespeed;
	bool isDead;
	bool canMove;

	Vector2 zeroVel = new Vector2(0, 0);

	// Ability cooldown and timers
	float meleeAttackCD = 0.7F;
	float meleeAttackTime;
	float laserAttackCD = 5F;
	float laserAttackTime;
	float swordSummonAttackCD = 5F;
	float swordSummonAttackTime;
	float teleCD = 5F;
	float teleTime;
	int healLimit;
	int healCount;
	
	Vector2[] retreatPoints;
	Vector2 teleLoc;

	// References
	Animator anim; 

	AudioSource enemySound;
	public AudioClip meleeAudio;
	public AudioClip laserAudio;
	public AudioClip swordSummonAudio;
	public AudioClip teleAudio;
	PlayerController thePlayerController;
	GameObject thePlayer;
	Vector2 playerDirection;
	Rigidbody2D kingRB;

	protected BoxCollider2D boxCollider;

	void Awake()
	{
		currentHealth = startingHealth;
		retreatPoints = new Vector2[4];
		retreatPoints[0] = new Vector2(27, 76);
		retreatPoints[1] = new Vector2(44, 76);
		retreatPoints[2] = new Vector2(25, 56);
		retreatPoints[3] = new Vector2(45, 56);

		canMove = true;
		meleeAttackCD = 0.7f;
		laserAttackCD = 5f;
		swordSummonAttackCD = 5f;
		teleCD = 5f;
		movespeed = 3f;
		healLimit = 5;
	}

	void Start()
	{
		anim = GetComponent<Animator>();
		enemySound = GetComponent<AudioSource>();
		boxCollider = GetComponent <BoxCollider2D> ();
		kingRB = GetComponent<Rigidbody2D>();
		thePlayer = GameObject.FindGameObjectWithTag("Player");
		thePlayerController = thePlayer.GetComponent<PlayerController>();
	}

	void Update()
	{
		playerDirection = thePlayer.transform.position - transform.position;
		UpdateTimers();

		if (kingRB.velocity == zeroVel)
			anim.SetBool ("isWalking", false);
		else
			anim.SetBool ("isWalking", true);

		if (true)
		{
			this.transform.rotation = Quaternion.LookRotation(Vector3.forward, thePlayer.transform.position - transform.position);
		}

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
		if (currentHealth <= 100 && healCount < healLimit && canMove) {
			print ("testing retreat");
			StopMoving();
			healCount++;
			Retreat();
		} else if (DistanceFromPlayer () < 3  && meleeAttackTime <= 0 && canMove) {
			StopMoving();
			MeleeAttack();
		} else if (DistanceFromPlayer () < 10 && (laserAttackTime <= 0 || swordSummonAttackTime <= 0) && canMove) {
			StopMoving();
			RandomRangedAttack ();
		} else if (DistanceFromPlayer () >= 15 && teleTime <= 0 && canMove) {
			Vector2 playerLoc = thePlayer.transform.position;
			StopMoving();
			Teleport (thePlayer.transform.position);
		} 
		else
		{
			MoveTo(playerDirection.normalized);
		}
	}

	void Retreat()
	{
		Vector2 furthestLoc = new Vector2(0, 0);
		float furthestDist = 1.0F;
		foreach (Vector2 i in retreatPoints)
		{
			print (i + "location@");
			if (Vector2.Distance(i, thePlayer.transform.position) > furthestDist)
			{

				furthestDist = Vector2.Distance(i, thePlayer.transform.position);
				furthestLoc = i;
			}
		}

		Teleport(furthestLoc, false);
		HealingStance();
	}

	void Teleport(Vector2 location, bool toPlayer = true)
	{
		if(!toPlayer)
		{
			teleTime = teleCD;
			teleLoc = location;
			anim.SetTrigger("teleport");
			Invoke ("ChangeLoc", 0.75f);
		}
		else
		{
			teleTime = teleCD;
			teleLoc = thePlayer.transform.position;
			anim.SetTrigger("teleport");
			Invoke ("ChangeLoc", 0.75f);
		}
	}

	void ChangeLoc()
	{
		this.transform.position = teleLoc;
		Invoke("SetMove", 2.5F);
	}

	void HealingStance()
	{
		for (int i = 0; i < 4; i++)
		{
			Invoke("Heal", 1F);
		}
		Invoke("SetMove", 4F);
	}

	void Heal()
	{
		//set up some kind of healing animation/particle
		currentHealth += 50;
		print ("healing, Hp = " + currentHealth);
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
		Invoke("SetMove", 0.667F);
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
		Invoke("SetMove", 1.417F);
	}

	void SummonSword()
	{
		swordSummonAttackTime = swordSummonAttackCD;
		anim.SetTrigger("summon");
		enemySound.clip = swordSummonAudio;
		enemySound.Play();
		//instantiate sword and set rotations
		//check collision on sword script
		Invoke("SetMove", 1.417F);
	}

	void Death()
	{
		isDead = true;

		// Turn the collider into a trigger so shots can pass through it.
		boxCollider.isTrigger = true;
		
		// Tell the animator that the enemy is dead.
		anim.SetTrigger("Dead");
		Destroy(this.gameObject, 10);
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
		canMove = false;
		kingRB.velocity = zeroVel;
	}

	void MoveTo(Vector2 direction)
	{
		if (canMove)
		{
			kingRB.velocity = direction * movespeed; //not correct
			//implement logic
		}
	}

}
