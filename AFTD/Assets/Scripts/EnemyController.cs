using UnityEngine;
using System.Collections;

public abstract class EnemyController : MonoBehaviour
{
	public int startingHealth = 100;            // The amount of health the enemy starts the game with.
	public int currentHealth;                   // The current health the enemy has.
	public int attackDamage = 5;
	public int scoreValue = 10;                 // The amount added to the player's score when the enemy dies.
	public float deadTime = 5;					// Time in seconds that the splatter sprite stays on screen before it disappears.
	public float defaultDeadTime = 5;
	public float hitTime = 0;					// The time inbetween player abilities that an enemy can be damaged by.

	public AudioClip deathClip;                 // The sound to play when the enemy dies.

	public Animator anim;                    // Reference to the animator.
	protected AudioSource enemyAudio;           // Reference to the audio source.
	protected ParticleSystem hitParticles;      // Reference to the particle system that plays when the enemy is damaged.
	protected ItemHandler itemHandler;			// Reference to the ItemHandler to handle dropping items when an enemy dies.
	protected BoxCollider2D boxCollider;        // Reference to the box collider.

	public bool isDead;                      // Whether the enemy is dead.
	public bool wasHit = false;

	// Initialize variables
	protected virtual void Awake()
	{
		// Stting the current health when the enemy first spawns.
		currentHealth = startingHealth;
	}

	// Set up object references
	protected virtual void OnEnable()
	{
		for (int i = 0; i < this.transform.GetChildCount(); ++i)
		{
			this.transform.GetChild(i).gameObject.SetActive(true);
		}
  		anim = this.gameObject.GetComponent<Animator>();
		enemyAudio = this.gameObject.GetComponent <AudioSource> ();
//		hitParticles = GetComponentInChildren <ParticleSystem> ();
		boxCollider = GetComponent <BoxCollider2D> ();
		itemHandler = GameObject.FindGameObjectWithTag ("itemHandler").GetComponent<ItemHandler>();

	}

	protected virtual void Update()
	{
		if(isDead)
		{
			deadTime -= Time.deltaTime;
			if(deadTime <= 0)
			{
				EnemyReset ();
				this.gameObject.SetActive(false);
			}
		}
	}

	protected virtual void OnTriggerStay2D(Collider2D collis)
	{

		if (collis.gameObject.tag == "meteors")
		{	
			hitTime -= Time.deltaTime;
			if (currentHealth > 0 && hitTime <= 0)
			{
				this.transform.GetChild(0).gameObject.SetActive (true);
				wasHit = true;
				TakeDamage (10);
				hitTime = 0.5f;
			}
		}
	}

	public void TakeDamage(int amount)		// Add in Vector2 hitPoint parameter for hit particle animations later.
	{
		// If the enemy is dead...
		if(isDead)
			// ... no need to take damage so exit the function.
			return;

		// Play the hurt sound effect.
		//enemyAudio.Play ();

		// Reduce the current health by the amount of damage sustained.
		currentHealth -= amount;

		// Set the position of the particle system to where the hit was sustained.
		//hitParticles.transform.position = hitPoint;

		// And play the particles.
		//hitParticles.Play();

		// If the current health is less than or equal to zero...
		if(currentHealth <= 0)
		{
			// ... the enemy is dead.
			Death();
		}
	}

	void Death()
	{
		// The enemy is dead.
		isDead = true;
		
		// Turn the collider into a trigger so shots can pass through it.
		boxCollider.isTrigger = true;

		// Tell the animator that the enemy is dead.
		anim.SetTrigger("Dead");
		
		// Change the audio clip of the audio source to the death clip and play it (this will stop the hurt clip playing).
		//enemyAudio.clip = deathClip;
		enemyAudio.Play ();
		
		dropItem();
		// After 2 seconds destory the enemy.
	}

	public void EnemyDestroy()
	{
		// Find and disable the Nav Mesh Agent.
		GetComponent<NavMeshAgent>().enabled = false;

		// Find the rigidbody component and make it kinematic (since we use Translate to sink the enemy).
		GetComponent<Rigidbody>().isKinematic = true;

		// Increase the score by the enemy's score value.
		// ScoreManager.score += scoreValue;
	}

	public abstract int EnemyAttack();

	protected virtual void dropItem()
	{
		itemHandler.DropItem(this.transform.position);
	}

	public void EnemyReset()
	{
		currentHealth = startingHealth;
		isDead = false;
		deadTime = defaultDeadTime;
		boxCollider.isTrigger = false;
		anim.SetTrigger ("respawn");
	}
}
