using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobSpeech : MonoBehaviour 
{
	public JimSpeech jimSpeech;
	public TextAnimater textAnimator;
	public float initialWait = 3f;
	public string speech = "Shush Jim!!";

	IEnumerator Start()
	{
		yield return new WaitForSeconds (initialWait);

		textAnimator.Show ();
		textAnimator.AnimateText (speech);
		textAnimator.onTextAnimationFinished += OnSpeechFinished;

		yield return new WaitForSeconds (0.8f);

		jimSpeech.Shush ();
	}

	private void OnSpeechFinished()
	{
		textAnimator.onTextAnimationFinished -= OnSpeechFinished;
		textAnimator.Hide ();
	}
}
