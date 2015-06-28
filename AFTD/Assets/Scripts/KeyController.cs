using UnityEngine;
using System.Collections;

public class KeyController : MonoBehaviour
{
	private EnemyController knight;
	public GameObject key;
	private bool keyDropped = false;
	
	void Start()
	{
		knight = GetComponentInParent<KnightController> ();
	}
	void Update()
	{
		if (knight.currentHealth <= 0 && !keyDropped) 
		{
			GameObject obj;
			obj = (GameObject)Instantiate (key);
			obj.transform.position = this.transform.position;
			obj.SetActive (true);
			keyDropped = true;
		}
	}
}