using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	float moveSpeed = 6f;
	Vector3 movement;
	Animator anim;
	Rigidbody2D playerRigidbody2D;
	BoxCollider2D playerBoxCollider2D;
	int floorMask;
	float camRayLength = 100f;
	int floorLevel;
	public int startingHealth = 100;
	public int currentHealth;
	bool isDead;
	
	void Awake()
	{
		floorMask = LayerMask.GetMask ("Floor");
		//floorLevel = int.Parse(Application.loadedLevelName);
		anim = GetComponent<Animator> ();
		playerRigidbody2D = GetComponent<Rigidbody2D> ();
		playerBoxCollider2D = GetComponent<BoxCollider2D> ();
		currentHealth = startingHealth;
	}
	
	void FixedUpdate()
	{
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");
		Move (h, v);
		Turning ();
		//Animating (h, v);
	}
	
	void Move(float h, float v)
	{
		movement.Set (h, v, 0f);
		movement = movement.normalized * moveSpeed * Time.deltaTime;
		
		playerRigidbody2D.MovePosition (transform.position + movement);
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

	public void TakeDamage (int amount)		// Add in Vector2 hitPoint parameter for hit particle animations later.
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
			//Death ();
			print ("Player is dead.");
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "upstairs") {
			floorLevel++;
			string floorString = floorLevel.ToString ();
			Application.LoadLevel (floorString);
		}
	}
	//void Animating(float h, float v)
	//{
	//bool walking = h != 0f || v != 0f;
	//anim.SetBool ("IsWalking", walking);
	//}	
}
