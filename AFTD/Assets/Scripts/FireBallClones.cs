using UnityEngine;
using System.Collections;

public class FireBallClones : MonoBehaviour {

	public float destroyTime;
	int abilitySpeed = 15;

	// Use this for initialization
	void Start () {
		Destroy (this.gameObject, destroyTime);
		GameObject thePlayer = GameObject.Find ("Player1");
		this.GetComponent<Rigidbody2D>().velocity = thePlayer.transform.up * abilitySpeed;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D()
	{
		Destroy (this.gameObject);
	}
}
