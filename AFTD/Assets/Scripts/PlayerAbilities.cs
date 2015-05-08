using UnityEngine;
using System.Collections;

public class PlayerAbilities : MonoBehaviour 
{
	public ParticleSystem aP;
	public Rigidbody2D abilityPrefab;

	void Awake()
	{

	}

	void Start()
	{

	}

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButton(0))
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
