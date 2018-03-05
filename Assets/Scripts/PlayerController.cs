using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
	public float vertMoveSpeed = 5.0f;
	public float horiMoveSpeed = 5.0f;
	public float origHoriMoveSpeed;
	public Animator animator;
	public TextAnimater textAnimator;

	private static readonly int ANIM_TRIGGER = Animator.StringToHash("UpDown");
	private bool moveForward = false;
	private float curVertMoveSpeed = 0f;

	void Awake()
	{
		moveForward = false;
	}

	void Start()
	{
		origHoriMoveSpeed = horiMoveSpeed;
	}

	public void StartForwardMovement()
	{
		animator.SetTrigger (ANIM_TRIGGER);
	}

	public void StartVerticalMovement()
	{
		curVertMoveSpeed = vertMoveSpeed;
		animator.SetTrigger (ANIM_TRIGGER);
	}

	public void SetMovement(bool move)
	{
		moveForward = move;
	}

		
	void Update () 
	{
		transform.position += Vector3.up * Input.GetAxis ("Vertical") * curVertMoveSpeed * Time.deltaTime;

		if (Input.touches.Length > 0) 
		{
			Touch t = Input.GetTouch (0);

			int move = (t.position.y > Screen.height * 0.5f) ? 1 : -1;

			transform.position += Vector3.up * move * curVertMoveSpeed * Time.deltaTime;
		}

		if (moveForward) 
		{
			transform.position += Vector3.right * horiMoveSpeed * Time.deltaTime;
		}


	}
}
