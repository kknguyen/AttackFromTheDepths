using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
	public int startingHealth = 100;            // The amount of health the enemy starts the game with.
	public int currentHealth;                   // The current health the enemy has.
	public float sinkSpeed = 2.5f;              // The speed at which the enemy sinks through the floor when dead.
	public int scoreValue = 10;                 // The amount added to the player's score when the enemy dies.
	public AudioClip deathClip;                 // The sound to play when the enemy dies.

	private Animator anim;                      // Reference to the animator.
	private AudioSource enemyAudio;             // Reference to the audio source.
	private ParticleSystem hitParticles;        // Reference to the particle system that plays when the enemy is damaged.

	private BoxCollider2D boxCollider;          // Reference to the box collider.

	private bool isDead;                        // Whether the enemy is dead.
	private bool isSinking;                     // Whether the enemy has started sinking through the floor.

	// Initialize variables
	void Awake()
	{
		// Stting the current health when the enemy first spawns.
		currentHealth = startingHealth;
	}

	// Set up object references
	void Start()
	{
//		anim = GetComponent <Animator> ();
//		enemyAudio = GetComponent <AudioSource> ();
//		hitParticles = GetComponentInChildren <ParticleSystem> ();
		boxCollider = GetComponent <BoxCollider2D> ();
	}

	void Update()
	{
		if (currentHealth <= 0 && !isDead )
			Death();
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
		//anim.SetTrigger ("Dead");

		// Change the audio clip of the audio source to the death clip and play it (this will stop the hurt clip playing).
		//enemyAudio.clip = deathClip;
		//enemyAudio.Play ();

		Debug.Log("enemy died");
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

	public void EnemyAttack()
	{

	}
}
