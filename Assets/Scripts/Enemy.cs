using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour 
{
	public float vertMoveSpeed = 5f;
	public float horiMoveSpeed = 0f;
	public Particle deathParticle;

	public int minDeathParticles = 5;
	public int maxDeathParticles = 10;

	public float minParticleSpeed = 1f;
	public float maxParticleSpeed = 8f;

	public float minParticleTimeAlive = 1f;
	public float maxParticleTimeAlive = 5;

	public float rotateSpeed = 100f;

	public MoveDirection curDirection = MoveDirection.Up;

	public AudioClip[] audioOnDeath;

	public BackgroundAudio audioPlayer;

	private static readonly int DAMAGE_HASH = Animator.StringToHash("Death");
	private static Transform player;
	private Collider2D collider2d;
	private Animator animator;
	private bool shouldUpdate = true;

	private Dictionary<MoveDirection, Vector3> moveDirections = new Dictionary<MoveDirection, Vector3> 
	{	
		{ MoveDirection.None, Vector3.zero },
		{ MoveDirection.Up, Vector3.up }, 
		{ MoveDirection.Down, Vector3.down } 
	};


	private List<Particle> particles = new List<Particle>();

	void Awake()
	{
		if (player == null) 
		{
			GameObject playerObj = GameObject.FindGameObjectWithTag ("Player");

			if (playerObj) 
			{
				player = playerObj.transform;
			}
		}

		animator = GetComponent<Animator> ();
		collider2d = GetComponent<Collider2D> ();
	}

	void Start()
	{
		int numParticles =  UnityEngine.Random.Range (minDeathParticles, maxDeathParticles);

		for (int i = 0; i < numParticles; i++) 
		{
			Particle particle = (Particle)Instantiate (deathParticle);
			particle.gameObject.SetActive (false);
			particles.Add (particle);
		}

		collider2d.enabled = false;
	}

	void OnBecameVisible()
	{
		collider2d.enabled = true;
	}

	public void OnRemoval()
	{
		foreach(var particle in particles)
		{
			particle.gameObject.SetActive (true);
			Vector3 dir = new Vector3 (UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));
			particle.transform.position = transform.position + dir * 0.1f;
			float speed = UnityEngine.Random.Range (minParticleSpeed, maxParticleSpeed);
			float timeAlive = UnityEngine.Random.Range (minParticleTimeAlive, maxParticleTimeAlive);
			particle.Initialise (timeAlive, dir * speed);
		}

		Destroy (gameObject);
	}


	public void OnDeath()
	{
		audioPlayer.PlayOneShot (audioOnDeath [UnityEngine.Random.Range (0, audioOnDeath.Length)]);
		shouldUpdate = false;
		animator.SetTrigger (DAMAGE_HASH);
	}

	public void SetMovement(bool moving)
	{
		shouldUpdate = moving;
	}

	// Update is called once per frame
	void Update () 
	{
		if(shouldUpdate && Mathf.Abs(player.transform.position.x - transform.position.x) < 15)
		{
			transform.position += moveDirections [curDirection] * vertMoveSpeed * Time.deltaTime;
			transform.position += Vector3.right * horiMoveSpeed * Time.deltaTime;

			transform.Rotate (Vector3.forward * rotateSpeed * Time.deltaTime);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag ("Wall")) 
		{
			if (other.transform.position.y > transform.position.y) 
			{
				curDirection = MoveDirection.Down;
			} 
			else 
			{
				curDirection = MoveDirection.Up;
			}

		}
	}

}
