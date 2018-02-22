using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour 
{
	public float moveSpeed = 1.0f;

	private bool shouldMove = false;

	public void StartMovement()
	{
		shouldMove = true;
	}

	// Update is called once per frame
	void Update ()
	{
		if (shouldMove) 
		{
			transform.position -= Vector3.left * moveSpeed * Time.deltaTime;
		}
	}
}
