﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveDirection { None, Up, Down };

public class Egg : MonoBehaviour 
{
	public GameDirector director;
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
	public float rotateSpeed = 40f;
	public BackgroundAudio gameAudio;
	public AudioClip[] onDamageAudioClips;

	private static readonly int DAMAGE_HASH = Animator.StringToHash("Damage");
	private static readonly int DEFEATED_HASH = Animator.StringToHash ("Defeated");

	private bool playerFound = false;
	private bool defeated = false;
	private bool playerReached = false;
	private bool shieldsDown = false;
	private Collider2D collider2d;

	private Dictionary<MoveDirection, Vector3> moveDirections = new Dictionary<MoveDirection, Vector3> 
	{	
		{ MoveDirection.None, Vector3.zero },
		{ MoveDirection.Up, Vector3.up }, 
		{ MoveDirection.Down, Vector3.down } 
	};

	private MoveDirection curDirection = MoveDirection.Up;

	void Awake()
	{
		collider2d = GetComponent<Collider2D> ();
	}

	void Start()
	{
		collider2d.enabled = false;
	}

	public void DoDamage()
	{
		shieldAnimator.SetTrigger (DAMAGE_HASH);

		if (--health <= 0) 
		{
			OnDefeated ();	
		}

		if (onDamageAudioClips != null && onDamageAudioClips.Length > 0) 
		{
			gameAudio.PlayOneShot (onDamageAudioClips[UnityEngine.Random.Range(0, onDamageAudioClips.Length)]);
		}
	}
		
	void Update () 
	{
		if (playerReached) 
		{
			return;
		}
		
		if (!shieldsDown && !playerFound && Mathf.Abs(playerController.transform.position.x - transform.position.x) < distanceFromPlayer) 
		{
			playerFound = true;

			text.onTextAnimationFinished += StartBoss;
			text.Show ();
			text.AnimateText ("This is what we've been training for. Full speed ahead!");
			gun.StopShooting ();
			gun.RemoveProjectiles ();
			collider2d.enabled = true;
		}	

		if (playerFound) 
		{
			transform.position += Vector3.right * forwardMoveSpeed * Time.deltaTime;
			transform.position += moveDirections [curDirection] * vertMoveSpeed * Time.deltaTime;
			transform.Rotate (Vector3.forward * rotateSpeed * Time.deltaTime);

			if (defeated) 
			{
				if (Mathf.Abs (playerController.transform.position.x - transform.position.x) < winDistance) 
				{
					playerReached = true;
					director.LoadEndScene ();
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

		if (!shieldsDown) 
		{
			gun.BeginShooting ();
		}
	}
		

	private void OnDefeated()
	{
		if (defeated || shieldsDown) 
		{
			return;
		}

		gameAudio.SetEffectsPlayEnabled (false);
		gameAudio.Fade (BackgroundAudio.FadeState.Out, 1, 7f);
		shieldAnimator.SetTrigger (DEFEATED_HASH);
		shieldsDown = true;
		gun.StopShooting ();
		text.onTextAnimationFinished += PenetrateEgg;
		text.Show ();
		text.AnimateText ("The shields are down, we did it!");
	}

	private void PenetrateEgg()
	{
		text.onTextAnimationFinished -= PenetrateEgg;
		text.Hide ();

		defeated = true;

		playerController.horiMoveSpeed += playerSpeedIncrease;
		playerController.vertMoveSpeed = 0f;
	}

	private void HideText()
	{
		text.onTextAnimationFinished -= HideText;
		text.Hide ();
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
