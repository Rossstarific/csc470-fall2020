using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceController : MonoBehaviour
{
	[SerializeField] private Animator animator;
	bool isIdle;
	public float distFromTop = 0.635255f;
	float bounceVel;

	// Start is called before the first frame update
	void Start()
	{
		animator = GetComponent<Animator>();
		isIdle = true;
		animator.SetBool("isIdle", isIdle);
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.contacts[0].point.y > transform.position.y + distFromTop)
		{
			if (col.gameObject.name.Equals("Player"))
			{
				bounceVel = col.gameObject.GetComponent<PlayerPlatformerController>().jumpTakeOffSpeed;
				col.gameObject.GetComponent<PlayerPlatformerController>().velocity.y = bounceVel * 1.5f;
				animator.SetBool("isIdle", false);

			}
		}
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		animator.SetBool("isIdle", true);
	}
}

