using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallController : MonoBehaviour
{
	Rigidbody2D rb;
	[SerializeField] private Animator animator;
	bool isIdle;

	// Use this for initialization
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		isIdle = true;
		animator.SetBool("isIdle", isIdle);
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.name.Equals("Player"))
		{
			isIdle = false;
			animator.SetBool("isIdle", isIdle);
			Invoke("DropPlatform", 0.5f);
		}

		if (col.gameObject.name.Equals("Ground"))
		{
			Invoke("LandPlatform", 0.5f);
			Destroy(gameObject, 6f);
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
}