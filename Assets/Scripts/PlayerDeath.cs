using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour 
{

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag ("Wall")) 
		{
			Destroy (gameObject);
		}
	}
}
