using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour {

	public float animationLengthMin = 1f;
	public float animationLengthMax = 3f;

	[Header("Number of Animations per Controller")]
	public int numAnimW = 3;
	Animator w;
	Animator a;
	public int numAnimA = 3;
	Animator s;
	public int numAnimS = 2;
	Animator d;
	public int numAnimD = 2;

	[Header("Speech Bubbles")]
	public Animator wbubble;
	public Animator abubble, sbubble, dbubble;

	void Awake() {
		w = GameObject.FindGameObjectWithTag ("Weeb").GetComponent<Animator> ();
		a = GameObject.FindGameObjectWithTag ("Aggro").GetComponent<Animator> ();
		s = GameObject.FindGameObjectWithTag ("Spam").GetComponent<Animator> ();
		d = GameObject.FindGameObjectWithTag ("Dank").GetComponent<Animator> ();
	}

	void Start () {
		StartCoroutine (NextAnimW ());
		StartCoroutine (NextAnimA ());
		StartCoroutine (NextAnimS ());
		StartCoroutine (NextAnimD ());
	}

	IEnumerator NextAnimW() {
		float waitTime = Random.Range (1f, 7f);
		yield return new WaitForSeconds (waitTime);
		int anim = Random.Range (1, numAnimW+1);
		w.SetInteger ("Anim", anim);
		float animationLength = Random.Range (animationLengthMin, animationLengthMax);
		yield return new WaitForSeconds (animationLength);
		w.SetInteger ("Anim", 0);
		StartCoroutine (NextAnimW ());
	}

	IEnumerator NextAnimA() {
		float waitTime = Random.Range (1f, 7f);
		yield return new WaitForSeconds (waitTime);
		int anim = Random.Range (1, numAnimA+1);
		a.SetInteger ("Anim", anim);
		float animationLength = Random.Range (animationLengthMin, animationLengthMax);
		yield return new WaitForSeconds (animationLength);
		a.SetInteger ("Anim", 0);
		StartCoroutine (NextAnimA ());
	}

	IEnumerator NextAnimS() {
		float waitTime = Random.Range (1f, 7f);
		yield return new WaitForSeconds (waitTime);
		int anim = Random.Range (1, numAnimS+1);
		s.SetInteger ("Anim", anim);
		float animationLength = Random.Range (animationLengthMin, animationLengthMax);
		yield return new WaitForSeconds (animationLength);
		s.SetInteger ("Anim", 0);
		StartCoroutine (NextAnimS ());
	}

	IEnumerator NextAnimD() {
		float waitTime = Random.Range (1f, 7f);
		yield return new WaitForSeconds (waitTime);
		int anim = Random.Range (1, numAnimD+1);
		d.SetInteger ("Anim", anim);
		float animationLength = Random.Range (animationLengthMin, animationLengthMax);
		yield return new WaitForSeconds (animationLength);
		d.SetInteger ("Anim", 0);
		StartCoroutine (NextAnimD ());
	}

	public void TriggerBubble(Computer comp) {
		Debug.Log ("TriggerBubble");
		if (comp == Computer.W) {
			wbubble.SetTrigger ("Normal");
		} else if (comp == Computer.A) {
			abubble.SetTrigger ("Normal");
		} else if (comp == Computer.S) {
			sbubble.SetTrigger ("Normal");
		} else {
			dbubble.SetTrigger ("Normal");
		}
	}
}
