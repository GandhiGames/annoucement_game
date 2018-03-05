using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAudio : MonoBehaviour 
{
	public enum FadeState
	{
		In,
		Out
	}
	
	public float introFadeSec = 1f;

	public float maxGameAudioVol = 0.8f;

	public AudioSource[] audioSources;
	public AudioSource effectsSource;

	private bool playEffects = true;

	void Start()
	{		
		Fade (FadeState.In, 0, introFadeSec, 0.4f);
	}

	public void SetEffectsPlayEnabled(bool enabled)
	{
		playEffects = enabled;
	}

	public void PlayOneShot(AudioClip audioClip)
	{
		if (playEffects) 
		{
			effectsSource.PlayOneShot (audioClip);
		}
	}

	public void Fade(FadeState operation, int source, float secFade, float maxVol = 1f)
	{
		if (source < 0 || source >= audioSources.Length) 
		{
			return;
		}

		if (operation == FadeState.In) 
		{
			StartCoroutine (_FadeIn (source, secFade, maxVol));
		} 
		else if (operation == FadeState.Out) 
		{
			StartCoroutine (_FadeOut (source, secFade));
		}
	}

	public void FadeFromToAudioSources(int fromSource, int toSource, float secFade, float maxVol)
	{
		if (fromSource < 0 || fromSource >= audioSources.Length) 
		{
			return;
		}

		if (toSource < 0 || toSource >= audioSources.Length) 
		{
			return;
		}

		StartCoroutine (_FadeFromToAudioSources (fromSource, toSource, secFade, maxVol));
	}

	private IEnumerator _FadeIn(int source, float secFade, float maxVol = 1f)
	{
		AudioSource fade = audioSources [source];

		float step = maxVol / secFade;

		fade.volume = 0f;
		fade.Play ();

		while (fade.volume < maxVol) 
		{
			fade.volume += step * Time.deltaTime;

			yield return null;
		}

		fade.volume = maxVol;
	}

	private IEnumerator _FadeOut(int source, float secFade)
	{
		AudioSource fade = audioSources [source];

		float step = fade.volume / secFade;

		while (fade.volume > 0f) 
		{
			fade.volume -= step * Time.deltaTime;

			yield return null;
		}

		fade.volume = 0f;
		fade.Stop ();
	
		
	}

	private IEnumerator _FadeFromToAudioSources(int fromSource, int toSource, float secFade, float maxVol = 1f)
	{
		AudioSource from = audioSources [fromSource];
		AudioSource to = audioSources [toSource];

		float fromVol = from.volume;
			
		float fromStep = fromVol / secFade;
		float toStep = maxVol / secFade;

		to.volume = 0;
		to.Play ();

		while (to.volume < maxVol) 
		{
			from.volume -= fromStep * Time.deltaTime;
			to.volume += toStep * Time.deltaTime;

			yield return null;
		}

		from.Stop();
		from.volume = 0;
	}

	
}
