using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
	public float vertMoveSpeed = 5.0f;
	public float horiMoveSpeed = 5.0f;
	public Animator animator;

	private static readonly int ANIM_TRIGGER = Animator.StringToHash("UpDown");
	private bool move = false;
	private float curVertMoveSpeed = 0f;

	public void StartForwardMovement()
	{
		move = true;
		animator.SetTrigger (ANIM_TRIGGER);
	}

	public void StartVerticalMovement()
	{
		curVertMoveSpeed = vertMoveSpeed;
		animator.SetTrigger (ANIM_TRIGGER);
	}
		
	void Update () 
	{
		if (move) 
		{
			transform.position += new Vector3 (horiMoveSpeed, Input.GetAxis ("Vertical") * curVertMoveSpeed) * Time.deltaTime;
		}
	}
}
