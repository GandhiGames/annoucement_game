using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JimSpeech : MonoBehaviour 
{
	public TextAnimater textAnimator;

	public string speech = "Woof!";

	void Start()
	{
		textAnimator.Show ();
		textAnimator.onTextAnimationFinished += DoWoof;
		DoWoof ();
	}

	public void Shush()
	{
		textAnimator.onTextAnimationFinished -= DoWoof;
		textAnimator.Hide ();
	}

	private void DoWoof()
	{
		textAnimator.AnimateText (speech, 0.05f, 0.2f);
	}
}
