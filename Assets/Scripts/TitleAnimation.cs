using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleAnimation : MonoBehaviour 
{
	public string[] animationTexts = {"Loading Player 4.", "Loading Player 4..", "Loading Player 4..."};
	public float timeBetweenStrings = 0.5f;

	public Text text;

	private int currentIndex = 0;
	private float currentTime = 0f;

	private bool shouldUpdate = false;

	void Start()
	{
		text.text = "";
	}

	public void Show(float secDelay)
	{
		StartCoroutine (_Show (secDelay));
	}

	private IEnumerator _Show(float secDelay)
	{
		yield return new WaitForSeconds (secDelay);

		shouldUpdate = true;
		currentIndex = 0;
		currentTime = 0f;
		text.text = animationTexts [currentIndex];
	}

	// Update is called once per frame
	void Update () 
	{
		if (shouldUpdate) 
		{
			currentTime += Time.deltaTime;

			if (currentTime >= timeBetweenStrings) 
			{
				currentTime = 0f;

				currentIndex = (currentIndex + 1) % animationTexts.Length;

				text.text = animationTexts [currentIndex];
			}
		}
	}
}
