using UnityEngine;
using System.Collections;

public class ArcherController : EnemyController
{
	public bool isAttacking;

	protected override void Awake()
	{
		currentHealth = 50;
	}


	public override int EnemyAttack()
	{
		isAttacking = true;
		print ("archer shoot arrow pls");
		return attackDamage;
	}
}
