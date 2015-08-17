using UnityEngine;
using System.Collections;

public class WarlockAttack : MonoBehaviour
{
	public WarlockController theWarlock;
	public CircleCollider2D spellPhase1;
	public CircleCollider2D spellPhase2;
	GameObject thePlayer;
	float abilityTime;
	Vector3 castSpot;

	
	// Use this for initialization
	void Start()
	{
		theWarlock = GetComponent<WarlockController>();
		thePlayer = GameObject.FindGameObjectWithTag("Player");
		castSpot = thePlayer.transform.position;
	}
	
	// Update is called once per frame
	void Update()
	{
		if (theWarlock.isAttacking)
		{
			castSpot = thePlayer.transform.position;
			CastSpell();
		}
	}
	
	void CastSpell()
	{

		CircleCollider2D phase1 = Instantiate(spellPhase1, castSpot, Quaternion.identity) as CircleCollider2D;
		Invoke("startPhase2", 2.0f);
		theWarlock.isAttacking = false;
	}
	
	void startPhase2()
	{
		CircleCollider2D phase2 = Instantiate(spellPhase2, castSpot, Quaternion.identity) as CircleCollider2D;
	}
}
