using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JimMovement : MonoBehaviour 
{
	public float moveSpeed = 1f;
	public float moveDistance = 5f;
	public Text text;

	private Vector3[] directions = new Vector3[]{ Vector3.right, Vector3.left };

	private int moveIndex = 0;
	private float moveStep = 0f;

	// Update is called once per frame
	void Update () 
	{
		float move = moveSpeed * Time.deltaTime;
		moveStep += moveSpeed;
		if (moveStep > moveDistance) 
		{
			moveIndex = (moveIndex + 1) % directions.Length;
			moveStep = 0f;
			Flip (transform);
			Flip (text.transform);
		}

		transform.position += directions [moveIndex] * move;
	}

	private void Flip(Transform t)
	{
		var curScale = t.localScale;
		t.localScale = new Vector3 (curScale.x * -1, curScale.y, curScale.z);
	}
}
