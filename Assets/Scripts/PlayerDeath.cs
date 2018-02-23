using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour 
{
	public PlayerController playerController;
	public NPC[] npcs;
	public Enemy[] enemies;
	public Particle deathParticle;
	public Animator animator;
	public TextAnimater text;
	public Collider2D collider2d;
	public CameraManager cameraManager;
	public LayerMask enemyMask;
	public BackgroundMovement[] backgrounds;
	public string[] textOnDeath = { "Oh man, better watch out!", "Yikes" };


	private static readonly int DEATH_TRIGGER_HASH = Animator.StringToHash ("Death");
	private static readonly int RESET_TRIGGER_HASH = Animator.StringToHash ("Reset");

	private bool damageRunning = false;

	public void ResetPlayer()
	{
		Vector3 centredPos =  new Vector3 (transform.position.x, 0f);
		 
		
		// if enemy in centre - destroy
		Collider2D[] enemies =  Physics2D.OverlapCircleAll(centredPos, 1f, enemyMask);

		if (enemies != null && enemies.Length > 0) 
		{
			for (int i = enemies.Length - 1; i >= 0; i--) 
			{
				var enemy = enemies [i].GetComponent<Enemy> ();

				if (enemy) 
				{
					enemy.OnDeath ();
				}
			}
		}

		// move player to centre screen 
		transform.position = centredPos;

		// flash player - reset animation
		animator.SetTrigger(RESET_TRIGGER_HASH);
	}

	public void RestartMovement()
	{
		// write some text
		if (!text.IsVisible ()) 
		{
			text.Show ();
			text.onTextAnimationFinished += HideText;
			text.AnimateText (textOnDeath [UnityEngine.Random.Range (0, textOnDeath.Length)]);
		}
		// after animation/text finished: restart movements

		cameraManager.MoveToInitialStart ();

		playerController.SetMovement(true);

		foreach (var n in npcs) 
		{
			if (n) 
			{
				n.SetMovement (true);
			}
		}
			
		foreach (var e in enemies) 
		{
			if (e) 
			{
				e.SetMovement (true);
			}
		}

		foreach (var b in backgrounds) 
		{
			b.StartMovement ();
		}

		collider2d.enabled = true;

		damageRunning = false;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (damageRunning) 
		{
			return;
		}
		
		if (other.gameObject.CompareTag ("Wall") || other.gameObject.CompareTag("Enemy")) 
		{
			damageRunning = true;
			// stop camera movement
			cameraManager.StopFollow();
			
			//if collide with enemy then destroy
			if (other.gameObject.CompareTag ("Enemy")) 
			{
				Enemy enemy = other.gameObject.GetComponent<Enemy> ();
				if (enemy) 
				{
					enemy.OnDeath ();
				}
			}
			
			// pause player movement
			playerController.SetMovement(false);

			// pause npc movement
			foreach (var n in npcs) 
			{
				if (n) 
				{
					n.SetMovement (false);
				}
			}

			// pause enemy movement
			foreach (var e in enemies) 
			{
				if (e) 
				{
					e.SetMovement (false);
				}
			}


			foreach (var b in backgrounds) 
			{
				b.StopMovement ();
			}

			//disable collider
			collider2d.enabled = false;

			//play death animation
			animator.SetTrigger(DEATH_TRIGGER_HASH);

			// create particles at death site - death aniamtion
			int numParticles =  UnityEngine.Random.Range (5, 10);

			for (int i = 0; i < numParticles; i++) 
			{
				Particle particle = (Particle)Instantiate (deathParticle);
				particle.gameObject.SetActive (true);
				Vector3 dir = new Vector3 (UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));
				particle.transform.position = transform.position + dir * 0.1f;
				float speed = UnityEngine.Random.Range (2, 5);
				float timeAlive = UnityEngine.Random.Range (0.5f, 2f);
				particle.Initialise (timeAlive, dir * speed);
			}
				


		}
	}

	private void HideText()
	{
		text.onTextAnimationFinished -= HideText;
		text.Hide ();
	}
		
}
