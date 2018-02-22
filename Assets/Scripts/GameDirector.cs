using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour 
{
	public PlayerController playerController;
	public CameraManager cameraManager;
	public TextAnimater textAnimator;
	public BackgroundMovement[] backgrounds;

	void Start()
	{
		textAnimator.AnimateText("Animate this text please", 0.05f, 1f);	
	}

	void OnEnable()
	{
		textAnimator.onTextAnimationFinished += StartGame;
		cameraManager.onInitialMoveFinished += EnableMovement;
	}

	void OnDisable()
	{
		textAnimator.onTextAnimationFinished -= StartGame;
		cameraManager.onInitialMoveFinished -= EnableMovement;
	}

	private void StartGame()
	{
		print ("start game");
		textAnimator.Hide ();
		cameraManager.MoveToInitialStart ();
	}

	private void EnableMovement()
	{
		playerController.StartForwardMovement ();

		foreach (var b in backgrounds) 
		{
			b.StartMovement ();
		}
	}

}
