using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour 
{
	public float moveSpeed = 1.0f;

	public bool shouldMove = false;

	public void StartMovement()
	{
		shouldMove = true;
	}

	public void StopMovement()
	{
		shouldMove = false;
	}

	// Update is called once per frame
	void Update ()
	{
		if (shouldMove) 
		{
			transform.position += Vector3.left * moveSpeed * Time.deltaTime;
		}
	}
}
