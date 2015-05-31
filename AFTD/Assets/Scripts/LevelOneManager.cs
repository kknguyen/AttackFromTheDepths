using UnityEngine;
using System.Collections;

public class LevelOneManager : LevelManager {

	protected EnemyPooler enemies;
	protected float spawnTime = 3f;
	public Texture tint;


	protected override void Awake()
	{
		level = 1;
	}

	protected override void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		playerObj = GameObject.FindGameObjectWithTag("Player");
		InvokeRepeating ("SpawnEnemies", spawnTime, spawnTime);
	}

	protected override void Update()
	{
		if (player.isDead)
		{

		}
	}

	protected override void SpawnEnemies()
	{
		print ("im still spawning");
		GameObject enemy = EnemyPooler.current.GetPooledObject();
		
		if (enemy == null) return;

		int block = Random.Range(1, 4);
		float x, y;
		Vector3 temp = new Vector3(0, 0, 0);
		switch(block)
		{
		case 1:
			x = Random.Range(-20f, 20f);
			y = Random.Range(8f, 12f);
			temp = new Vector3(playerObj.transform.position.x + x, playerObj.transform.position.y + y, 0);
			break;
		case 2:
			x = Random.Range(16f, 20f);
			y = Random.Range(-8f, 8f);
			temp = new Vector3(playerObj.transform.position.x + x, playerObj.transform.position.y + y, 0);
			break;
		case 3:
			x = Random.Range(-20f, 20f);
			y = Random.Range(-12f, -8f);
			temp = new Vector3(playerObj.transform.position.x + x, playerObj.transform.position.y + y, 0);
			break;
		case 4:
			x = Random.Range(-20f, -16f);
			y = Random.Range(-8f, 8f);
			temp = new Vector3(playerObj.transform.position.x + x, playerObj.transform.position.y + y, 0);
			break;
		}
		if (temp.x > 45)
			temp.x = 45;
		if (temp.x < -45)
			temp.x = -45;
		if (temp.y > 45)
			temp.y = 45;
		if (temp.y < -45)
			temp.y =-45;



		var checkResult = Physics2D.OverlapCircle(temp, 0.25f);
		if (checkResult == null)
		{
			enemy.transform.position = temp;
			enemy.SetActive(true);
		}
		else
		{
			print("uh oh");
			return;
		}
	}
	
	protected override void StartBoss()
	{

	}

	void GameOver()
	{

	}

	void OnGUI()
	{
		if (player.isDead)
		{
			GUI.DrawTexture(new Rect(0, 0, Screen.width,Screen.height), tint);
			if(GUI.Button (new Rect(Screen.width/2-100,Screen.height/2-100, 200, 50),"Restart level"))
			{
				Application.LoadLevel("LevelOneTest");
			}
			if(GUI.Button (new Rect(Screen.width/2-100,Screen.height/2-25, 200, 50),"Quit to Main Menu"))
			{
				Application.LoadLevel("MainMenu");
			}
		}
	}
}
