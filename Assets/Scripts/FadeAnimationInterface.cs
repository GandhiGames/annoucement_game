using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FadeAnimationInterface : MonoBehaviour 
{
	public Animator animator;
	public Action onFadeFinished;

	public void AnimationFinished()
	{
		if (onFadeFinished != null) 
		{
			onFadeFinished ();
		}
	}
}
