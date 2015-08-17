using UnityEngine;
using System.Collections;

public class RightClick : MonoBehaviour {
	
	private float destroyTime;
	
	void Awake()
	{
		destroyTime = 0.5f;
	}
	
	void Start()
	{	
		Destroy (this.gameObject, destroyTime);
	}
}

