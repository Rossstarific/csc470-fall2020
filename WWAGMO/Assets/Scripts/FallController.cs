using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallController : MonoBehaviour
{
	Rigidbody2D rb;
	[SerializeField] private Animator animator;
	bool isIdle;
	private Vector2 initialPosition;
	private Vector2 playerColPointY;
	private Vector2 playerNewPos;

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
		if (col.gameObject.tag.Equals("Player"))
		{
			playerColPointY = col.gameObject.transform.GetChild(1).gameObject.transform.position;
			if (playerColPointY.y > transform.position.y)
			{
				GetComponent<UpDownMove>().enabled = false;
				animator.SetBool("isIdle", false);
				Invoke("DropPlatform", 0.5f);
				Invoke("DespawnPlatform", 5f);
				Invoke("RespawnPlatform", 10f);
			}
		}

		else if (col.gameObject.name.Equals("Ground"))
		{
			rb.isKinematic = true;
			Invoke("DespawnPlatform", 5f);
			Invoke("RespawnPlatform", 10f);
		}
	}

	void DropPlatform()
	{
		rb.isKinematic = false;
	}

	void DespawnPlatform()
	{
		gameObject.SetActive(false);
	}

	void RespawnPlatform()
	{
		rb.isKinematic = true;
		gameObject.transform.position = initialPosition;
		gameObject.SetActive(true);
		animator.SetBool("isIdle", true);
		GetComponent<UpDownMove>().enabled = true;
	}

}