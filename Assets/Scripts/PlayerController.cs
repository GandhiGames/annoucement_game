using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
	public float vertMoveSpeed = 5.0f;
	public float horiMoveSpeed = 5.0f;
	public Animator animator;

	private static readonly int ANIM_TRIGGER = Animator.StringToHash("UpDown");
	private bool moveForward = false;
	private float curVertMoveSpeed = 0f;

	void Start()
	{
		moveForward = true;
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
		
	void Update () 
	{
		transform.position += Vector3.up * Input.GetAxis ("Vertical") * curVertMoveSpeed * Time.deltaTime;
		
		if (moveForward) 
		{
			transform.position += Vector3.right * horiMoveSpeed * Time.deltaTime;
		}
	}
}
