using UnityEngine;
using System.Collections;

public class PlayerAbilities : MonoBehaviour 
{
	public ParticleSystem aP;
	public Rigidbody2D abilityPrefab;
	
	private bool isPaused;
	
	void Awake()
	{
		
	}
	
	void Start()
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		//isPaused = GameObject.FindGameObjectWithTag("HUD").GetComponent<Pause>().paused;
		if (Input.GetMouseButton(0) && Time.timeScale == 1)
		{
			Cast();
		}
	}
	
	void Cast() 
	{
		aP.Stop();
		aP.Play();
		Rigidbody2D bPrefab = Instantiate(abilityPrefab, transform.position, Quaternion.identity) as Rigidbody2D;
	}
}