using UnityEngine;
using System.Collections;

public class ArcherAI : EnemyAI
{


	protected override void Awake()
	{
		speed = 2;
		wallLayer = 1 << 8;
		playerLayer = 1 << 9;
		zeroVelocity = new Vector2 (0, 0);
		maxRaycast = 15;
		patrolling = false;
		waitTime = 3;
		maximumRange = 20;
		patrolRaycast = 1.5f;
		attackCooldown = 0;
		defaultCooldown = 1;
	}

	protected override void ChasePlayer()
	{

		if (distance < 10 && ChaseCondition())
		{
			StopMoving();
			transform.rotation = Quaternion.LookRotation(Vector3.forward, playerPosition - transform.position);
			if (attackCooldown <= 0)
			{
				enemyHealth.EnemyAttack();
				attackCooldown = defaultCooldown;

			}
		}
		else if (ChaseCondition())
		{
			this.GetComponent<Rigidbody2D>().velocity = playerDirection.normalized * speed;
			transform.rotation = Quaternion.LookRotation(Vector3.forward, playerPosition - transform.position);
			anim.SetBool("isWalking", true);
			waitTime = 3;
        }
        else
        {
            StopMoving();
            EnemyWait();
        }
    }
}
