using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(Text))]
public class TextAnimater : MonoBehaviour 
{
	public Action onTextAnimationFinished;
	
	private Text animText;

	void Awake()
	{
		animText = GetComponent<Text> ();
	}
		
	public void AnimateText(string text, float secsBetweenChar, float secsEndDelay)
	{
		StartCoroutine (_AnimateText (text, secsBetweenChar, secsEndDelay));
	}

	public void Show()
	{
		animText.enabled = true;
	}

	public void Hide()
	{
		animText.enabled = false;
	}

	private IEnumerator _AnimateText(string toDisplay, float secsBetweenChar, float secEndDelay)
	{
		int i = 0;
		animText.text = "";
		while( i < toDisplay.Length )
		{
			animText.text += toDisplay[i++];
			yield return new WaitForSeconds(secsBetweenChar);
		}

		yield return new WaitForSeconds (secEndDelay);

		if (onTextAnimationFinished != null) 
		{
			onTextAnimationFinished ();
		}
	}
}
