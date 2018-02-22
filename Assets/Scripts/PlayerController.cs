using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
	public float vertMoveSpeed = 5.0f;
	public float horiMoveSpeed = 5.0f;

	private bool moveForward = false;

	public void StartForwardMovement()
	{
		moveForward = true;
	}
		
	void Update () 
	{
		float x = moveForward ? horiMoveSpeed : 0f;
		transform.position += new Vector3 (x, Input.GetAxis ("Vertical") * vertMoveSpeed) * Time.deltaTime;
	}
}
