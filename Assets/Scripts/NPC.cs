using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour 
{
	public TrailRenderer trail;
	public float forwardMoveSpeed = 5f;
	public Animator animator;

	private static readonly int ANIM_TRIGGER = Animator.StringToHash("UpDown");
	private bool moving = false;

	// Use this for initialization
	void Start () 
	{
		trail.enabled = false;
	}

	public void StartMovement()
	{
		moving = true;
		animator.SetTrigger (ANIM_TRIGGER);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (moving) 
		{
			transform.position += new Vector3 (forwardMoveSpeed, 0f) * Time.deltaTime;
		}
	}
		
}
