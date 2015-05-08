using UnityEngine;
using System.Collections;

public class KnightController : EnemyController
{

	protected override void Awake()
	{
		currentHealth = 150;
		attackDamage = 10;
	}

	public override int EnemyAttack()
	{
		//
		return attackDamage;
	}
}
