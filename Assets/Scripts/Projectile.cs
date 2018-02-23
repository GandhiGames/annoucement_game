using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour 
{
	public Rigidbody2D rigidbody2d;
	public float maxTimeAlive = 20f;
	public float maxDistFromPlayer = 5f;
	public Gun owner;

	private float currentTimeAlive = 0f;
	private bool shouldUpdate = false;

	void OnEnable()
	{
		currentTimeAlive = 0f;
	}

	public void Remove()
	{
		owner.RemoveProjectile (this);
		Destroy (gameObject);
	}

	void Update()
	{
		if (shouldUpdate) 
		{
			currentTimeAlive += Time.deltaTime;
			if (currentTimeAlive >= maxTimeAlive) 
			{
				Remove ();
				
			}
			else if (Mathf.Abs (owner.transform.position.x - transform.position.x) > maxDistFromPlayer) 
			{
				Remove ();
			}
		}
	}

	void OnBecameInvisible()
	{
		Remove ();
	}

	public void Initialise(Gun owner, float speed)
	{
		this.owner = owner;
		rigidbody2d.velocity = Vector2.right * speed;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag ("Enemy")) 
		{
			other.GetComponent<Enemy> ().OnDeath ();
		}
		else if (other.CompareTag ("Egg")) 
		{
			Egg egg = other.GetComponent<Egg> ();
			egg.DoDamage ();
			Destroy (gameObject);
		}
		
	}
}
