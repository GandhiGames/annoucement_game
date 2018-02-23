using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour 
{
	public PlayerController playerController;
	public CameraManager cameraManager;
	public TextAnimater textAnimator;
	public BackgroundMovement[] backgrounds;
	public TrailRenderer playerTail;
	public NPC[] npcs;
	public GameObject level;
	public Gun playerShoot;

	IEnumerator Start()
	{
		yield return new WaitForSeconds (1f);
		//StartGame();
	}


	public void StartGame()
	{

		foreach (var n in npcs) 
		{
			if (n && n.gameObject.activeInHierarchy) 
			{
				n.SetMovement (true);
			}
		}

		playerController.SetMovement (true);

		cameraManager.StartFollow ();
		
		textAnimator.onTextAnimationFinished += _StartGame;
		textAnimator.Show ();
		textAnimator.AnimateText("Ok boys, we're going in. Activate thrusters!");	
	}

	private void _StartGame()
	{
		textAnimator.onTextAnimationFinished -= StartGame;
		textAnimator.Hide ();

		cameraManager.onInitialMoveFinished += EnableLevel;
		cameraManager.StartFollow ();
		EnableMovement ();

		StartCoroutine (StartLevel (1.5f));
	}

	private IEnumerator StartLevel(float secsDelay)
	{
		yield return new WaitForSeconds (secsDelay);

		cameraManager.MoveToInitialStart (0.8f);
	}

	private void EnableMovement()
	{		
		ActivateThrusters ();

		playerController.StartForwardMovement ();

		foreach (var n in npcs) 
		{
			if (n && n.gameObject.activeInHierarchy) 
			{
				n.StartMovement ();
			}
		}

		foreach (var b in backgrounds) 
		{
			b.StartMovement ();
		}
			
	}

	private void EnableLevel()
	{
		cameraManager.onInitialMoveFinished -= EnableLevel;

		playerController.StartVerticalMovement ();

		LoadLevel ();

		playerShoot.BeginShooting ();
	}

	private void ActivateThrusters()
	{
		playerTail.enabled = true;

		foreach (var n in npcs) 
		{
			if (n && n.gameObject.activeInHierarchy) 
			{
				n.trail.enabled = true;
			}
		}
	}

	private void LoadLevel()
	{
		//level.transform.position = new Vector3 (playerController.transform.position.x - 50, 0f);
		//level.gameObject.SetActive (true);
	}

}
