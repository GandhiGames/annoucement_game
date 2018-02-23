using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSpeech : MonoBehaviour 
{
	public TextAnimater text;
	public TitleAnimation titleAnim;

	public string[] speech = { "So we've got something we would like to share with you...", "And for some reason I thought this would be a good way to do it"};

	private int speechIndex = 0;

	void Start () 
	{
		text.Show ();
		text.onTextAnimationFinished += OnSpeechOver;
		text.AnimateText (speech [speechIndex]);
	}
	
	private void OnSpeechOver()
	{
		speechIndex = (speechIndex + 1) % speech.Length;

		if (speechIndex != 0) 
		{
			text.AnimateText (speech [speechIndex]);
		} 
		else 
		{
			titleAnim.Show (2f);
			text.onTextAnimationFinished -= OnSpeechOver;
			text.Hide ();
		}
	}
}
