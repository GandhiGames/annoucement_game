using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobUpDown : MonoBehaviour 
{
	public float floatStrength;

	private Vector2 floatY;
	private float originalY;
	private bool shouldUpdate = true;

	void Start ()
	{
		this.originalY = transform.position.y;
	}

	public void Stop()
	{
		shouldUpdate = false;
		//transform.position = new Vector2 (transform.position.x, originalY);
	}

	void Update () 
	{
		if (shouldUpdate || Mathf.Abs(originalY - transform.position.y) > 0.04f) 
		{
			transform.position = new Vector2 (transform.position.x, 
				originalY + (Mathf.Sin (Time.time) * floatStrength));
		}
	}
}
