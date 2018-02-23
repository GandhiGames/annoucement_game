using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipAnimationInterface : MonoBehaviour 
{
	public PlayerDeath playerDeath;

	public void ResetPlayer()
	{
		playerDeath.ResetPlayer ();
	}

	public void RestartMovement()
	{
		playerDeath.RestartMovement ();
	}
}
