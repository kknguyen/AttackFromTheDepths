using UnityEngine;
using System.Collections;

public class KnightController : EnemyController
{

	protected override void Awake()
	{
		startingHealth = 150;
		currentHealth = startingHealth;
		attackDamage = 10;
	}

	public override int EnemyAttack()
	{
		//
		return attackDamage;
	}
}
