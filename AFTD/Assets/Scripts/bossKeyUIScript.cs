using UnityEngine;
using System.Collections;

public class bossKeyUIScript : MonoBehaviour {
	
	PlayerController thePlayerController;
	
	// Use this for initialization
	void Start () {
		GameObject thePlayer = GameObject.FindGameObjectWithTag("Player");
		thePlayerController = thePlayer.GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}