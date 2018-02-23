using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTrigger : MonoBehaviour 
{
	public string text = "We're nearly there. Keep pushing forward.";

	private TextAnimater textAnim;
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag ("Player")) 
		{
			var playrController = other.gameObject.GetComponent<PlayerController> ();

			if (playrController) 
			{
				textAnim = playrController.textAnimator;
				textAnim.Show ();
				textAnim.onTextAnimationFinished += HideTextBox;
				textAnim.AnimateText (text, 0.05f, 1.5f);
			}
		}
	}

	private void HideTextBox()
	{
		textAnim.onTextAnimationFinished -= HideTextBox;
		textAnim.Hide ();
	}
}
