using UnityEngine;
using System.Collections;

public class ItemHandler : MonoBehaviour {

	public static ItemHandler current;
	public GameObject health;
	public GameObject moveSpeed;
	public GameObject attackDamage;
	
	void Awake()
	{
		current = this;
	}
	
	void Start()
	{
		
	}
	
	void Update()
	{
		
	}
	
	public void DropItem(Vector3 position)
	{
		int powerUp = Random.Range(0, 100);
		GameObject obj;
		if (powerUp <= 50) 
		{
			return;
		}
		else if (powerUp <= 90)
		{
			obj = (GameObject)Instantiate(health);
		}
		else if (powerUp <= 95)
		{
			obj = (GameObject)Instantiate(attackDamage);
		}
		else
		{
			obj = (GameObject)Instantiate(moveSpeed);
			
		}
		obj.transform.position = position;
		obj.SetActive(true);
		print(powerUp + " hello");
	}
}
