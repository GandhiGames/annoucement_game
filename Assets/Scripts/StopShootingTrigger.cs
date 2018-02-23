using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopShootingTrigger : MonoBehaviour 
{
	public Gun playerGun;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag ("Player")) 
		{
			playerGun.StopShooting ();
		} 
		else if (other.CompareTag ("Projectile")) 
		{
			var proj = other.GetComponent<Projectile> ();

			if (proj) 
			{
				Gun owner = proj.owner;

				if (owner) 
				{
					owner.StopShooting ();
				}

				proj.Remove ();
			}
		}
	}
}
