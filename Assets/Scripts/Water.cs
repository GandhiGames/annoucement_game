using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour 
{
	public float moveSpeed = 1f;

	void Update () 
	{
		transform.position = new Vector3 (transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
	}
}
