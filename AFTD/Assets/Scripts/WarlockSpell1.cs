using UnityEngine;
using System.Collections;

public class WarlockSpell1 : MonoBehaviour
{

	private float destroyTime = 2;


	void Start ()
	{
		Destroy(this.gameObject, destroyTime);
	}
	

	void Update ()
	{
	
	}
}
