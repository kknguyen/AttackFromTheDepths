using UnityEngine;
using System.Collections;

public class ArcherController : EnemyController
{
	public bool isAttacking;

	protected override void Awake()
	{
		startingHealth = 50;
		currentHealth = startingHealth;
	}


	public override int EnemyAttack()
	{
		anim.SetTrigger("attack");
		isAttacking = true;
		return attackDamage;
	}
}
