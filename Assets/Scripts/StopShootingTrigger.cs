using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopShootingTrigger : MonoBehaviour 
{
	public Gun playerGun;

	private bool ownerStopped = false;

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
				if (!ownerStopped) 
				{
					ownerStopped = true;

					Gun owner = proj.owner;

					if (owner && Mathf.Abs (owner.transform.position.x - transform.position.x) < 0.5f) 
					{
						owner.StopShooting ();
					}
				}

				proj.Remove ();
			}
		}
	}
}
