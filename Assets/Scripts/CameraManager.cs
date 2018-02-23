using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraManager : MonoBehaviour 
{
	public PlayerController target;
	public float initialMoveSpeed = 1.0f;
	public Transform zoomTarget;
	public Action onInitialMoveFinished;
	public Action onZoomFinished;
	public float zoomSpeed = 1f;
	public float zoomTargetSize = 0.2f;

	public Camera thisCamera;

	private bool shouldFollow = false;

	void Awake()
	{
		thisCamera = GetComponent<Camera> ();
	}

	void Start()
	{
		shouldFollow = false;
	}

	public void MoveToInitialStart(float secEndDelay = 0f)
	{
		StartCoroutine (_MoveToInitialStart (secEndDelay));
	}

	public void Zoom(float secEndDelay = 0f)
	{
		StartCoroutine (_Zoom (secEndDelay));
	}

	public void StartFollow()
	{
		shouldFollow = true;
	}

	public void StopFollow()
	{
		shouldFollow = false;
	}

	private IEnumerator _Zoom(float secEndDelay)
	{
		//transform.position = new Vector3 (zoomTarget.position.x, zoomTarget.position.y, transform.position.z);

		while (thisCamera.orthographicSize > zoomTargetSize) 
		{
			thisCamera.orthographicSize -= zoomSpeed * Time.deltaTime;
			yield return null;

		}

		yield return new WaitForSeconds (secEndDelay);

		if (onZoomFinished != null) 
		{
			onZoomFinished ();
		}
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


			while(Camera.main.WorldToScreenPoint (target.transform.position).x  < Screen.width * 0.3f)
			{
				transform.position -= Vector3.right * (initialMoveSpeed * 0.2f) * Time.smoothDeltaTime;	
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
