using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour 
{
	public Projectile bulletPrefab;
	public float secsBetweenBullets = 0.3f;
	public float projectileSpeed = 10f;

	private bool shooting = false;

	public void BeginShooting()
	{
		shooting = true;
		StartCoroutine (Shoot ());
	}

	public void StopShooting()
	{
		shooting = false;
	}

	private IEnumerator Shoot()
	{
		while (shooting) 
		{
			Projectile proj = (Projectile)Instantiate (bulletPrefab);
			proj.transform.position = transform.position;
			proj.Initialise (projectileSpeed);

			yield return new WaitForSeconds (secsBetweenBullets);
		}
	}
		
}
