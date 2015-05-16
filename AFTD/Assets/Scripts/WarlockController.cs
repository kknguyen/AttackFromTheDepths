using UnityEngine;
using System.Collections;

public class WarlockController : EnemyController
{
	public bool isAttacking;
	
	protected override void Awake()
	{
		currentHealth = 120;
	}
	
	
	public override int EnemyAttack()
	{
		isAttacking = true;
		print ("mage attack");
		return attackDamage;
	}
}
