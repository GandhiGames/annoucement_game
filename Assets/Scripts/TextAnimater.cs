﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(Text))]
public class TextAnimater : MonoBehaviour 
{
	public Action onTextAnimationFinished;

	public BackgroundAudio audioPlayer;

	public AudioClip onCharClip;

	private Text animText;

	void Awake()
	{
		animText = GetComponent<Text> ();
	}
		
	public void AnimateText(string text, float secsBetweenChar = 0.05f, float secsEndDelay = 1f)
	{
		StartCoroutine (_AnimateText (text, secsBetweenChar, secsEndDelay));
	}

	public bool IsVisible()
	{
		return transform.parent.gameObject.activeSelf && transform.parent.gameObject.activeInHierarchy;
	}

	public void Show()
	{
		transform.parent.gameObject.SetActive (true);
		//animText.enabled = true;
	}

	public void Hide()
	{
		transform.parent.gameObject.SetActive (false);
		//animText.enabled = false;
	}

	private IEnumerator _AnimateText(string toDisplay, float secsBetweenChar, float secEndDelay)
	{
		int i = 0;
		animText.text = "";
		while( i < toDisplay.Length )
		{
			if (onCharClip) 
			{
				audioPlayer.PlayOneShot (onCharClip);
			}

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
