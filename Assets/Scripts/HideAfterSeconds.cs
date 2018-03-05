using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideAfterSeconds : MonoBehaviour 
{
	public float secondsToHide = 3f;

	public float hideDurationSec = 1f;

	public SpriteRenderer spriteRend;
	public Text textToHide;

	private Color spriteInitColour;
	private Color textInitColour;

	private float curSec = 0f;
	private bool shouldUpdate = true;

	void Start()
	{
		spriteInitColour = spriteRend.color;
		textInitColour = textToHide.color;
	}

	void OnEnable()
	{
		curSec = 0f;
		shouldUpdate = true;
	}

	void Update ()
	{
		if (shouldUpdate) 
		{
			if (curSec >= secondsToHide) 
			{
				shouldUpdate = false;
				StartCoroutine (Hide ());
			}
		
			curSec += Time.deltaTime;
		}
	}

	private IEnumerator Hide()
	{
		float spriteStep = spriteInitColour.a / hideDurationSec;
		float textStep = textInitColour.a / hideDurationSec;

		while (spriteRend.color.a > 0f) 
		{
			spriteRend.color = new Color (spriteInitColour.r, spriteInitColour.g, spriteInitColour.b, spriteRend.color.a - spriteStep * Time.deltaTime);
			textToHide.color = new Color (textInitColour.r, textInitColour.g, textInitColour.b, textToHide.color.a - textStep * Time.deltaTime);

			yield return null;
		}

		gameObject.SetActive (false);
	}
}
