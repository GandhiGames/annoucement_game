using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour 
{
	public TrailRenderer trail;
	public float forwardMoveSpeed = 5f;

	// Use this for initialization
	void Start () 
	{
		//trail.enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.position += new Vector3 (forwardMoveSpeed, 0f) * Time.deltaTime;
	}
}
