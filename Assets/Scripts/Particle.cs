using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour 
{
	private Rigidbody2D rigidbody2d;
	private SpriteRenderer spriteRenderer;
	private bool shouldUpdate = false;
	private float timeAlive;
	private float alphaStep;
	private float currentAliveTime;
	private Color curColour;

	void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer> ();
		rigidbody2d = GetComponent<Rigidbody2D> ();
	}

	void Start()
	{
		curColour = spriteRenderer.color;
	}

	public void Initialise(float timeAlive, Vector3 velocity)
	{
		currentAliveTime = 0f;
		this.timeAlive = timeAlive;
		rigidbody2d.velocity = velocity;

		alphaStep = 1f / timeAlive;
		shouldUpdate = true;
	}

	void Update()
	{
		if (shouldUpdate) 
		{
			spriteRenderer.color = new Color(curColour.r, curColour.g, curColour.b, spriteRenderer.color.a - alphaStep * Time.deltaTime);

			currentAliveTime += Time.deltaTime;
				
			if (currentAliveTime >= timeAlive) 
			{
				Destroy (gameObject);
			}
		}
	}
}
