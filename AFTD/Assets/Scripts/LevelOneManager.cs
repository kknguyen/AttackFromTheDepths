using UnityEngine;
using System.Collections;

public class LevelOneManager : LevelManager {

	protected EnemyPooler enemies;
	protected float spawnTime = 3f;
	public Texture tint;
	public GameObject bossWallOff;
	private BoxCollider2D boxCollider;

	public GUISkin fontSkin;

	protected override void Awake()
	{
		level = 1;
	}

	protected override void Start()
	{
		boxCollider = GetComponent<BoxCollider2D>();
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
		//print ("im still spawning");
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
			//print("uh oh");
			return;
		}
	}
	
	protected override void StartBoss()
	{
		GameObject.FindGameObjectWithTag("bossHealthBar").GetComponent<CanvasGroup>().alpha = 1;
	}

	void GameOver()
	{

	}

	void OnGUI()
	{
		GUI.skin = fontSkin;
		GUI.skin.button.normal.background = null;
		if (player.isDead)
		{
			GUI.DrawTexture(new Rect(0, 0, Screen.width,Screen.height), tint);
			if(GUI.Button (new Rect(Screen.width/2-100,Screen.height/2-100, 200, 50),"Restart level"))
			{
				Application.LoadLevel("LevelOne");
			}
			if(GUI.Button (new Rect(Screen.width/2-100,Screen.height/2-25, 200, 50),"Quit to Main Menu"))
			{
				Application.LoadLevel("MainMenu");
			}
		}
	}
	void OnTriggerEnter2D(Collider2D collis)
	{
		if (collis.gameObject.tag == "Player") 
		{
			Instantiate (bossWallOff,new Vector2(43.44f,52.87f),Quaternion.identity);
			StartBoss();
			Destroy(boxCollider);
		} 
	}
}
