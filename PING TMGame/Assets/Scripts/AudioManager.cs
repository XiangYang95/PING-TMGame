using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AudioManager : MonoBehaviour {

	public bool HushInEditor = true;

	public float volumeIncreaseSpeed = 0.5f;

	AudioSource audio;
	public AudioClip mainTheme;

	void Awake () {
		audio = this.GetComponent<AudioSource> ();
		audio.loop = true;

		if (EditorApplication.isPlaying && HushInEditor)
			audio.mute = true;
		else
			StartCoroutine (PlayMainTheme ());
	}

	IEnumerator PlayMainTheme() {
		yield return new WaitForSecondsRealtime (audio.clip.length);
		audio.clip = mainTheme;
		audio.volume = 0f;
		audio.Play ();
		StartCoroutine (FadeIn ());
	}

	IEnumerator FadeIn() {
		while (audio.volume < 1f) {
			audio.volume += Time.deltaTime * Time.deltaTime * volumeIncreaseSpeed;
			yield return new WaitForEndOfFrame ();
		}
	}
}
