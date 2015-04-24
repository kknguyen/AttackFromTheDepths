using UnityEngine;
using System.Collections;

public class PlayerAbilities : MonoBehaviour {
	
	public ParticleSystem aP;

	void start() {
		aP = GetComponent <ParticleSystem> ();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1"))
		{
			if (aP != null) {
				Debug.Log ("SHOOT");
				aP.Play ();
			}
		}

	}

	void cast() {

	}
}
