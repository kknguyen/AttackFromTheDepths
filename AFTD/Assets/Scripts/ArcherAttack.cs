﻿using UnityEngine;
using System.Collections;

public class ArcherAttack : MonoBehaviour
{
	public ArcherController theArcher;
	public Rigidbody2D arrowPrefab;

	// Use this for initialization
	void Start()
	{
		theArcher = GetComponentInParent<ArcherController>();
	}
	
	// Update is called once per frame
	void Update()
	{
		if (theArcher.isAttacking)
		{
			ShootArrow();
		}
	}

	void ShootArrow()
	{
		Rigidbody2D bPrefab = Instantiate(arrowPrefab, transform.position, Quaternion.identity) as Rigidbody2D;
		theArcher.isAttacking = false;
	}
}