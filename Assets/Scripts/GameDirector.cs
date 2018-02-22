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
	public GameObject speechBubble;
	public GameObject level;

	void Start()
	{
		textAnimator.AnimateText("Ok boys, we're going in! Activate thrusters", 0.05f, 1f);	
	}

	void OnEnable()
	{
		textAnimator.onTextAnimationFinished += StartGame;
		cameraManager.onInitialMoveFinished += EnableLevel;
	}

	void OnDisable()
	{
		textAnimator.onTextAnimationFinished -= StartGame;
		cameraManager.onInitialMoveFinished -= EnableLevel;
	}

	private void StartGame()
	{
		textAnimator.Hide ();

		cameraManager.StartFollow ();
		EnableMovement ();

		StartCoroutine (StartLevel (1.5f));
	}

	private IEnumerator StartLevel(float secsDelay)
	{
		yield return new WaitForSeconds (secsDelay);

		cameraManager.MoveToInitialStart ();
	}

	private void EnableMovement()
	{
		speechBubble.SetActive (false);
		
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
		level.transform.position = new Vector3 (playerController.transform.position.x + 100, 0f);
		level.gameObject.SetActive (true);
	}

}
