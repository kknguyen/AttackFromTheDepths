using UnityEngine;
using System.Collections;

public class PlayerAbilities : MonoBehaviour {
	
	ParticleSystem abilityParticles;

	void start() {
		abilityParticles = GetComponent<ParticleSystem> ();
		abilityParticles.Stop();
		abilityParticles.Clear();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButton ("Fire1"))
		{
			abilityParticles.Play ();
		}

	}

	void cast() {

	}
}
