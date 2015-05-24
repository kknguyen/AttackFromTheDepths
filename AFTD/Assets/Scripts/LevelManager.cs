using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class LevelManager : MonoBehaviour
{
	protected int level;
	protected PlayerController player;

	protected GameObject playerObj;
	//protected EnemyPooler enemies;

	protected abstract void Awake();

	protected abstract void Start();
	
	protected abstract void Update();

	protected abstract void SpawnEnemies();

	protected abstract void StartBoss();

}
