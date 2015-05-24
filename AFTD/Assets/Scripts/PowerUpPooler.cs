using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerUpPooler : MonoBehaviour
{
	public static PowerUpPooler current;
	public GameObject health;
	public GameObject moveSpeed;
	public GameObject attackDamage;
	public int powerUpPoolAmount = 30;
	public bool willGrow = true;
	
	List<GameObject> pooledObjects;
	
	void Awake()
	{
		current = this;
	}
	
	void Start()
	{
		pooledObjects = new List<GameObject>();
		for(int i = 0; i < powerUpPoolAmount; i++)
		{
			addObject (false);
			
		}
	}
	
	public GameObject GetPooledObject()
	{
		for(int i = 0; i < pooledObjects.Count; i++)
		{
			if(!pooledObjects[i].activeInHierarchy)
			{
				return pooledObjects[i];
			}
		}
		if(willGrow)
		{
			return addObject(true);
		}
		
		return null;
	}
	void Update()
	{
		
	}
	
	GameObject addObject(bool active)
	{
		int enemy = Random.Range(0, 100);
		if (enemy <= 20)
		{
			GameObject obj = (GameObject)Instantiate(attackDamage);
			obj.SetActive(active);
			pooledObjects.Add(obj);
			return obj;
		}
		else if (enemy <= 50)
		{
			GameObject obj = (GameObject)Instantiate(moveSpeed);
			obj.SetActive(active);
			pooledObjects.Add(obj);
			return obj;
		}
		else
		{
			GameObject obj = (GameObject)Instantiate(health);
			obj.SetActive(active);
			pooledObjects.Add(obj);
			return obj;
		}
	}
	
	
}
