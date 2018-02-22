using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraManager : MonoBehaviour 
{
	public PlayerController target;
	public float initialMoveSpeed = 1.0f;

	public Action onInitialMoveFinished;

	private bool shouldFollow = false;

	void Start()
	{
		shouldFollow = true;
	}

	public void MoveToInitialStart(float secEndDelay = 0f)
	{
		StartCoroutine (_MoveToInitialStart (secEndDelay));
	}

	public void StartFollow()
	{
		shouldFollow = true;
	}

	private IEnumerator _MoveToInitialStart(float secEndDelay)
	{
		if (target) 
		{
			while(Camera.main.WorldToScreenPoint (target.transform.position).x  > Screen.width * 0.3f)
			{
				transform.position += Vector3.right * (target.horiMoveSpeed + initialMoveSpeed) * Time.smoothDeltaTime;	
				yield return null;
			}

			StartFollow ();

			if (secEndDelay > 0f) 
			{
				yield return new WaitForSeconds (secEndDelay);
			}

			if (onInitialMoveFinished != null) 
			{
				onInitialMoveFinished ();
			}

		}
	}
	
	// Update is called once per frame
	void LateUpdate () 
	{
		if (shouldFollow && target) 
		{
			transform.position += Vector3.right * target.horiMoveSpeed * Time.deltaTime;	
		}
	}
}
