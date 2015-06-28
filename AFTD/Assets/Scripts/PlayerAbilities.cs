using UnityEngine;
using System.Collections;

public class PlayerAbilities : MonoBehaviour 
{
	public ParticleSystem aP;
	public Rigidbody2D abilityPrefab;
	public GameObject ability2Prefab;
	public GameObject ability3Prefab;
	GameObject bPrefab2;
	GameObject bPrefab3;

	public Animator anim;
	
	private bool isPaused;

	public float attack1CD = 0.5f;
	public float attack1Time = 0f;

	public float attack2CD = 5f;
	public float attack2Time = 0f;

	public float attack3CD = 8f;
	public float attack3Time = 0f;


	void Awake()
	{
		
	}
	
	void Start()
	{
		bPrefab2 = Instantiate(ability2Prefab, transform.position, Quaternion.identity) as GameObject;
		bPrefab2.transform.SetParent(GameObject.FindGameObjectWithTag("Player").transform);
		bPrefab2.SetActive(false);

		bPrefab3 = Instantiate(ability3Prefab, transform.position, Quaternion.identity) as GameObject;
		bPrefab3.transform.SetParent(GameObject.FindGameObjectWithTag("Player").transform);
		bPrefab3.SetActive(false);
        
	}
	
	// Update is called once per frame
	void Update () 
	{
		attack1Time -= Time.deltaTime;
		attack2Time -= Time.deltaTime;
		attack3Time -= Time.deltaTime;
		//isPaused = GameObject.FindGameObjectWithTag("HUD").GetComponent<Pause>().paused;
		if (Input.GetMouseButton(0) && Time.timeScale == 1 && attack1Time <= 0)
		{
			//anim.SetBool ("isWalking", false);
			anim.SetTrigger ("fireball");
			attack1Time = attack1CD;
			Cast();
		}
		if (Input.GetKey("e") && Time.timeScale == 1 && attack2Time <= 0)
		{
			//anim.SetBool ("isWalking", false);
			anim.SetTrigger ("flamethrow");
			attack2Time = attack2CD;
			Cast2();
			Invoke("Cast2off", 1.417f);
		}
		if (Input.GetKey("r") && Time.timeScale == 1 && attack3Time <= 0)
		{
			anim.SetBool ("isWalking", false);
			anim.SetTrigger ("meteor");
			attack3Time = attack3CD;
			Cast3();
            //Invoke("Cast3off", 1.417f);
        }
    }
	
	void Cast() 
	{
		//aP.Stop();
		//aP.Play();

		Rigidbody2D bPrefab = Instantiate(abilityPrefab, transform.position, Quaternion.identity) as Rigidbody2D;
	}

	void Cast2()
	{
		bPrefab2.SetActive (true);
	}

	void Cast2off()
	{
		bPrefab2.SetActive(false);
	}

	void Cast3()
	{
		Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		bPrefab3 = Instantiate(ability3Prefab, mousePos, Quaternion.identity) as GameObject;
		//bPrefab3.SetActive (true);
	}
	
//	void Cast3off()
//	{
//		bPrefab3.SetActive(false);
//    }
}