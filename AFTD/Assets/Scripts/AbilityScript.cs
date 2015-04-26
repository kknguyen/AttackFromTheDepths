using UnityEngine;
using System.Collections;

public class AbilityScript : MonoBehaviour {


	public Rigidbody2D firePrefab;
	public float bulletSpeed = 500;
	public float yValue = 1f; 
	public float xValue = 0.2f; 

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton(0))
		{
			cast();
		}
	}

	void cast() {
		Rigidbody2D bPrefab = Instantiate(firePrefab, transform.position, Quaternion.identity) as Rigidbody2D;
		bPrefab.GetComponent<Rigidbody2D>().velocity = gameObject.transform.up * 10;

	}
}
