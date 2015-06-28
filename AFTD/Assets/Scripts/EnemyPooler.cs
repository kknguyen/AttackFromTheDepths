using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyPooler : MonoBehaviour
{
	public static EnemyPooler current;
	public GameObject knight;
	public GameObject archer;
	public GameObject warlock;
	public int enemyPoolAmount = 30;
	public bool willGrow = false;
	
	List<GameObject> pooledObjects;
	
	void Awake()
	{
		current = this;
	}
	
	void Start()
	{
		pooledObjects = new List<GameObject>();
		for(int i = 0; i < enemyPoolAmount; i++)
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
			GameObject obj = (GameObject)Instantiate(warlock);
			obj.SetActive(active);
			pooledObjects.Add(obj);
			return obj;
		}
		else if (enemy <= 50)
		{
			GameObject obj = (GameObject)Instantiate(archer);
			obj.SetActive(active);
			pooledObjects.Add(obj);
			return obj;
		}
		else
		{
			GameObject obj = (GameObject)Instantiate(knight);
			obj.SetActive(active);
			pooledObjects.Add(obj);
			return obj;
		}
	}


}
