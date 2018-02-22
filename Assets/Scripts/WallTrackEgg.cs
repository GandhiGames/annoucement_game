using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTrackEgg : MonoBehaviour 
{
	public Transform egg;
	
	// Update is called once per frame
	void LateUpdate () 
	{
		transform.position = new Vector3(egg.position.x, 
			transform.position.y, transform.position.z);	
	}
}
