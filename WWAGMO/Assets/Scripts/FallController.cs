using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallController : MonoBehaviour
{
	Rigidbody2D rb;
	[SerializeField] private Animator animator;
	bool isIdle;
	public float distFromTop = 0.3f;
	private Vector2 initialPosition;

	// Use this for initialization
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		initialPosition = transform.position;
		isIdle = true;
		animator.SetBool("isIdle", isIdle);
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.GetContact(0).point.y > transform.position.y + distFromTop) {
			if (col.gameObject.name.Equals("Player"))
			{
				GetComponent<UpDownMove>().enabled = false;
				animator.SetBool("isIdle", false);
				Invoke("DropPlatform", 0.5f);

			}
		}

		if (col.gameObject.name.Equals("Ground"))
		{
			Invoke("LandPlatform", 0);
			Invoke("DespawnPlatform", 5f);
			Invoke("RespawnPlatform", 10f);
		}
	}

	void DropPlatform()
	{
		rb.isKinematic = false;
	}

	void LandPlatform()
	{
		rb.isKinematic = true;
	}

	void DespawnPlatform()
	{
		gameObject.SetActive(false);
	}

	void RespawnPlatform()
	{
		gameObject.transform.position = initialPosition;
		gameObject.SetActive(true);
		animator.SetBool("isIdle", true);
		GetComponent<UpDownMove>().enabled = true;
	}

}