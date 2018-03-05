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
	public GameObject preScene;
	public GameObject gameScene;
	public FadeAnimationInterface fadeAnimator;
	public BobUpDown bobUpDown;
	public GameObject endFade;
	public GameObject endScene;
	public Egg egg;
	public TitleAnimation title;
	public BackgroundAudio backAudio;

	public float fadeSecsBetweenMenuAndGame = 1f;

	private static readonly int FADE_HASH = Animator.StringToHash ("Fade");

	private float cameraOrigZoom;
	private Vector3 cameraOrigPos;

	void Start()
	{
		gameScene.SetActive (false);
		endScene.SetActive (false);
		preScene.SetActive (true);
	}

	public void LoadGameScene()
	{
		title.Show (0f);
		cameraOrigZoom = cameraManager.thisCamera.orthographicSize;
		cameraOrigPos = cameraManager.transform.position;
		cameraManager.onZoomFinished += Fade;
		cameraManager.Zoom (.2f);
		bobUpDown.Stop ();
		backAudio.FadeFromToAudioSources (0, 1, fadeSecsBetweenMenuAndGame, 0.1f);
	}
		
	public void LoadEndScene()
	{
		playerController.horiMoveSpeed = 0f;
		foreach (var n in npcs) 
		{
			if (n && n.gameObject.activeInHierarchy) 
			{
				n.SetMovement (false);
			}
		}

		StartCoroutine (_LoadEndScene ());
	}

	private IEnumerator _LoadEndScene()
	{

		endFade.SetActive (true);

		yield return new WaitForSeconds (2.5f);

		gameScene.SetActive (false);
		endScene.transform.position = new Vector3(cameraManager.transform.position.x, cameraManager.transform.position.y, 0f);
		endScene.SetActive (true);
	}

	private void Fade()
	{
		cameraManager.onZoomFinished -= Fade;

		fadeAnimator.onFadeFinished += StartGame;

		fadeAnimator.animator.SetTrigger (FADE_HASH);
	}

	private void StartGame()
	{
		fadeAnimator.onFadeFinished -= StartGame;
		//fadeAnimator.gameObject.SetActive (false);

		cameraManager.transform.position = cameraOrigPos;
		cameraManager.thisCamera.orthographicSize = cameraOrigZoom;

		preScene.SetActive (false);
		gameScene.SetActive (true);

		foreach (var n in npcs) 
		{
			if (n && n.gameObject.activeInHierarchy) 
			{
				n.SetMovement (true);
			}
		}

		playerController.SetMovement (true);


		foreach (var b in backgrounds) 
		{
			b.StartMovement ();
		}

		cameraManager.StartFollow ();
		
		textAnimator.onTextAnimationFinished += NextText;
		textAnimator.Show ();
		textAnimator.AnimateText("Ok boys, this is it. Give it your all.");	
	}

	private void NextText()
	{
		textAnimator.onTextAnimationFinished -= NextText;
		textAnimator.onTextAnimationFinished += _StartGame;
		textAnimator.AnimateText("Activate thrusters! We're going in.");	

	}

	private void _StartGame()
	{
		textAnimator.onTextAnimationFinished -= _StartGame;
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
