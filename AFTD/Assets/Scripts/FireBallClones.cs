﻿using UnityEngine;
using System.Collections;

public class FireBallClones : MonoBehaviour
{
	private float destroyTime;

	//Timed animation value to show the fireball hitting something
	// before getting destroyed/disappearing. Will need to be
	// changed if the animation changes
	private float destroyTimeHit;
	private int abilitySpeed = 20;

	private GameObject thePlayer;
	private Animator anim;

	void Awake()
	{
		destroyTime = 2;
		destroyTimeHit = 0.75f;
	}

	void Start()
	{
		anim = this.gameObject.GetComponent<Animator>();
		Destroy(this.gameObject, destroyTime);
		thePlayer = GameObject.FindGameObjectWithTag("Player");
		this.GetComponent<Rigidbody2D>().velocity = thePlayer.transform.up * abilitySpeed;

		//This rotates the fireball so it looks like it is shooting straight out of the dragon's mouth.
		transform.rotation = Quaternion.LookRotation(Vector3.forward, transform.position - thePlayer.transform.position);
	}

	void Update()
	{

	}

	void OnCollisionEnter2D(Collision2D collis)
	{
		if (collis.gameObject.tag == "wall")
		{
			this.GetComponent<CircleCollider2D>().isTrigger = true;
			anim.SetTrigger("Hit");
			this.GetComponent<Rigidbody2D>().velocity = thePlayer.transform.up * 0;
			Destroy (this.gameObject, destroyTimeHit);
		}
		else if (collis.gameObject.tag == "enemy")
		{
			EnemyController enemyHealth = collis.gameObject.GetComponent<EnemyController>();
			if (enemyHealth.currentHealth > 0)
			{
				this.GetComponent<CircleCollider2D>().isTrigger = true;
				collis.transform.GetChild(0).gameObject.SetActive (true);
				anim.SetTrigger("Hit");
				this.GetComponent<Rigidbody2D>().velocity = thePlayer.transform.up * 0;
				Destroy (this.gameObject, destroyTimeHit);
				enemyHealth.TakeDamage (10); //Changed to 100 for testing purposes.
			}
		}
		else if(collis.gameObject.tag == "theKing")
		{
			KingControl enemyHealth = collis.gameObject.GetComponent<KingControl>();
			print ("kings health" + enemyHealth.currentHealth);
			if (enemyHealth.currentHealth > 0)
			{
				this.GetComponent<CircleCollider2D>().isTrigger = true;
				//collis.transform.GetChild(0).gameObject.SetActive (true);
				anim.SetTrigger("Hit");
				this.GetComponent<Rigidbody2D>().velocity = thePlayer.transform.up * 0;
				Destroy (this.gameObject, destroyTimeHit);
				enemyHealth.TakeDamage (10); //Changed to 100 for testing purposes.
			}
		}
	}
}
