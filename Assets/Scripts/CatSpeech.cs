using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSpeech : MonoBehaviour 
{
	public TextAnimater text;
	public TitleAnimation titleAnim;
	public GameDirector director;

	public string[] speech = { "So we've got something we would like to share with you...", "And for some reason I thought this would be a good way to do it"};

	private int speechIndex = 0;

	void Start () 
	{
		text.Show ();
		text.onTextAnimationFinished += OnSpeechOver;
		text.AnimateText (speech [speechIndex], 0.04f, 0.8f);
	}

	private void OnSpeechOver()
	{
		speechIndex = (speechIndex + 1) % speech.Length;

		if (speechIndex != 0) 
		{
			text.AnimateText (speech [speechIndex], 0.04f, 1.2f);
		} 
		else 
		{
			//titleAnim.Show (0.5f);
			director.LoadGameScene();
			text.onTextAnimationFinished -= OnSpeechOver;
			text.Hide ();
		}
	}
}
