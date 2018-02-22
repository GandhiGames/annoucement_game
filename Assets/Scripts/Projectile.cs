using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour 
{
	public Rigidbody2D rigidbody2d;
	public float maxTimeAlive = 20f;

	private float currentTimeAlive = 0f;

	void OnEnable()
	{
		currentTimeAlive = 0f;
	}

	void Update()
	{
		currentTimeAlive += Time.deltaTime;
		if (currentTimeAlive >= maxTimeAlive) 
		{
			Destroy (gameObject);
		}
	}

	public void Initialise(float speed)
	{
		rigidbody2d.velocity = Vector2.right * speed;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag ("Egg")) 
		{
			Egg egg = other.GetComponent<Egg> ();
			egg.DoDamage ();
			Destroy (gameObject);
		}
	}
}
