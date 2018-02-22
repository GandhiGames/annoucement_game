using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour 
{
	public Transform NPCs;
	public TextAnimater text;
	public PlayerController playerController;
	public float forwardMoveSpeed = 5f;
	public float vertMoveSpeed = 5f;
	public float distanceFromPlayer = 8f;
	public float winDistance = 1f;
	public float npcCatchupSpeed = 4f;
	public float npcMoveForwardDist = 10f;
	public Animator shieldAnimator;
	public Gun gun;
	public int health = 10;
	public float playerSpeedIncrease = 4f;

	private static readonly int DAMAGE_HASH = Animator.StringToHash("Damage");
	private static readonly int DEFEATED_HASH = Animator.StringToHash ("Defeated");

	private bool playerFound = false;
	private bool defeated = false;

	private enum MoveDirection { None, Up, Down };
	private Dictionary<MoveDirection, Vector3> moveDirections = new Dictionary<MoveDirection, Vector3> 
	{	
		{ MoveDirection.None, Vector3.zero },
		{ MoveDirection.Up, Vector3.up }, 
		{ MoveDirection.Down, Vector3.down } 
	};

	private MoveDirection curDirection = MoveDirection.Up;

	public void DoDamage()
	{
		shieldAnimator.SetTrigger (DAMAGE_HASH);

		if (--health <= 0) 
		{
			OnDefeated ();	
		}
	}
		
	void Update () 
	{
		if (!playerFound && Mathf.Abs(playerController.transform.position.x - transform.position.x) < distanceFromPlayer) 
		{
			playerFound = true;

			text.onTextAnimationFinished += StartBoss;
			text.Show ();
			text.AnimateText ("This is what we've been training for. Full speed ahead!");
		}	

		if (playerFound) 
		{
			transform.position += Vector3.right * forwardMoveSpeed * Time.deltaTime;
			transform.position += moveDirections [curDirection] * vertMoveSpeed * Time.deltaTime;

			if (defeated) 
			{
				if (Mathf.Abs (playerController.transform.position.x - transform.position.x) < winDistance) 
				{
					print ("Won");
				}

				if (Mathf.Abs (playerController.transform.position.y - transform.position.y) < 0.2f) 
				{
					curDirection = MoveDirection.None;
				}
				else if (playerController.transform.position.y > transform.position.y) 
				{
					curDirection = MoveDirection.Up;
				} 
				else 
				{
					curDirection = MoveDirection.Down;	
				}
			}
		
		}
	}

	private void StartBoss()
	{
		text.Hide ();

		StartCoroutine (MoveNPCForward ());
	}

	private IEnumerator MoveNPCForward()
	{
		float x = 0f;

		while (x < npcMoveForwardDist) 
		{
			x += Time.deltaTime * npcCatchupSpeed;

			NPCs.position += Vector3.right * Time.deltaTime * npcCatchupSpeed;

			yield return null;
		}

		gun.BeginShooting ();
	}
		

	private void OnDefeated()
	{
		gun.StopShooting ();
		playerController.horiMoveSpeed += playerSpeedIncrease;
		shieldAnimator.SetTrigger (DEFEATED_HASH);
		defeated = true;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag ("Wall")) 
		{
			if (other.transform.position.y > transform.position.y) 
			{
				curDirection = MoveDirection.Down;
			} 
			else 
			{
				curDirection = MoveDirection.Up;
			}
		
		}
	}
}
