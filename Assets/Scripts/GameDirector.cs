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

	void Start()
	{
		textAnimator.AnimateText("Ok boys, we're going in. Activate thrusters!");	
	}

	void OnEnable()
	{
		textAnimator.onTextAnimationFinished += StartGame;
		cameraManager.onInitialMoveFinished += EnableLevel;
	}

	private void StartGame()
	{
		textAnimator.onTextAnimationFinished -= StartGame;
		textAnimator.Hide ();

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
			n.StartMovement ();
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
	}

	private void ActivateThrusters()
	{
		playerTail.enabled = true;

		foreach (var n in npcs) 
		{
			n.trail.enabled = true;
		}
	}

	private void LoadLevel()
	{
		//level.transform.position = new Vector3 (playerController.transform.position.x - 50, 0f);
		//level.gameObject.SetActive (true);
	}

}
