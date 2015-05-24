using UnityEngine;
using System.Collections;

public class PlayerAbilities : MonoBehaviour 
{
	public ParticleSystem aP;
	public Rigidbody2D abilityPrefab;
	public GameObject ability2Prefab;
	GameObject bPrefab2;
	
	private bool isPaused;
	
	void Awake()
	{
		
	}
	
	void Start()
	{

		bPrefab2 = Instantiate(ability2Prefab, transform.position, Quaternion.identity) as GameObject;
		bPrefab2.transform.SetParent(GameObject.FindGameObjectWithTag("Player").transform);
		bPrefab2.SetActive(false);

	}
	
	// Update is called once per frame
	void Update () 
	{
		//isPaused = GameObject.FindGameObjectWithTag("HUD").GetComponent<Pause>().paused;
		if (Input.GetMouseButton(0) && Time.timeScale == 1)
		{
			Cast();
		}
		if (Input.GetKey("r") && Time.timeScale == 1)
		{
			Cast2();
			Invoke("Cast2off", 1f);
		}
	}
	
	void Cast() 
	{
		aP.Stop();
		aP.Play();
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
}