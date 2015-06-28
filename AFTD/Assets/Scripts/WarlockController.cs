using UnityEngine;
using System.Collections;

public class WarlockController : EnemyController
{
	public bool isAttacking;
	
	protected override void Awake()
	{
		startingHealth = 120;
		currentHealth = startingHealth;
	}
	
	
	public override int EnemyAttack()
	{
		isAttacking = true;
		anim.SetTrigger("attack");
		return attackDamage;
	}
}
