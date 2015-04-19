using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	public float speed = 6f;
	
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
		movement = movement.normalized * speed * Time.deltaTime;
		
		playerRigidbody2D.MovePosition (transform.position + movement);
	}
	
	void Turning()
	{
		/*Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit floorHit;
		if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask)) 
		{
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.z = 0f;
			Quaternion newRotation = Quaternion.LookRotation (playerToMouse);
			playerRigidbody.MoveRotation (newRotation);
		}*/
		var mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Quaternion rot = Quaternion.LookRotation (transform.position - mousePosition, Vector3.forward);
		transform.rotation = rot;
		transform.eulerAngles = new Vector3 (0, 0, transform.eulerAngles.z);
	}
	
	//void Animating(float h, float v)
	//{
	//bool walking = h != 0f || v != 0f;
	//anim.SetBool ("IsWalking", walking);
	//}	
}
