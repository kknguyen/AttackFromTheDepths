using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	float moveSpeed = 6f;
	Vector3 movement;
	Animator anim;
	Rigidbody2D playerRigidbody2D;
	BoxCollider2D playerBoxCollider2D;
	int floorMask;
	float camRayLength = 100f;
	
	void Awake()
	{
		floorMask = LayerMask.GetMask ("Floor");
		anim = GetComponent<Animator> ();
		playerRigidbody2D = GetComponent<Rigidbody2D> ();
		playerBoxCollider2D = GetComponent<BoxCollider2D> ();
	}
	
	void FixedUpdate()
	{
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");
		Move (h, v);
		Turning ();
		//Animating (h, v);
	}
	
	void Move(float h, float v)
	{
		movement.Set (h, v, 0f);
		movement = movement.normalized * moveSpeed * Time.deltaTime;
		
		playerRigidbody2D.MovePosition (transform.position + movement);
	}
	
	void Turning()
	{
		//var mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		//Quaternion rot = Quaternion.LookRotation (transform.position - mousePosition, Vector3.forward);
		//transform.rotation = rot;
		//transform.eulerAngles = new Vector3 (0, 0, transform.eulerAngles.z);

		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);
	}
	
	//void Animating(float h, float v)
	//{
	//bool walking = h != 0f || v != 0f;
	//anim.SetBool ("IsWalking", walking);
	//}	
}
