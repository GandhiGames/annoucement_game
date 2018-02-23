using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour 
{
	public Projectile bulletPrefab;
	public float secsBetweenBullets = 0.3f;
	public float projectileSpeed = 10f;
	public int preload = 50;
	private bool shooting = false;

	private List<Projectile> projectiles = new List<Projectile>();

	private Queue<Projectile> projPool = new Queue<Projectile>();

	void Start()
	{
		for (int i = 0; i < preload; i++) 
		{
			Projectile proj = (Projectile)Instantiate (bulletPrefab);
			proj.gameObject.SetActive (false);
			projPool.Enqueue (proj);
		}
	}

	public void BeginShooting()
	{
		shooting = true;
		StartCoroutine (Shoot ());
	}

	public void StopShooting()
	{
		shooting = false;
	}

	public void RemoveProjectiles()
	{
		for(int i = projectiles.Count - 1; i >= 0; i--)
		{
			projectiles[i].Remove ();
		}

		projectiles.Clear ();
	}

	public void RemoveProjectile(Projectile proj)
	{
		projectiles.Remove (proj);
	}

	private IEnumerator Shoot()
	{
		while (shooting) 
		{
			Projectile proj;

			if (projPool.Count > 0) 
			{
				proj = projPool.Dequeue ();
				proj.gameObject.SetActive (true);
			} 
			else 
			{
				proj = (Projectile)Instantiate (bulletPrefab);
				Debug.Log ("More projs needed");
			}

			proj.transform.position = transform.position;
			proj.Initialise (this, projectileSpeed);
			projectiles.Add (proj);
			yield return new WaitForSeconds (secsBetweenBullets);
		}
	}


		
}
