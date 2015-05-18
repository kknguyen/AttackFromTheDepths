using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class moveSpeedUIScript : MonoBehaviour {

	private int count;
	PlayerController thePlayerController;
	Text counter;

	// Use this for initialization
	void Start () {
		GameObject thePlayer = GameObject.FindGameObjectWithTag("Player");
		thePlayerController = thePlayer.GetComponent<PlayerController>();
		counter = GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		count = thePlayerController.moveSpeedCount;
		counter.text = "x" + count;

		if (count == 0)
			Destroy (this.gameObject);

	}
}
