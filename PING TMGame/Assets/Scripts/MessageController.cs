using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageController : MonoBehaviour {

	public static MessageController mc;
	public Text wScreen, aScreen, sScreen, dScreen;
	public Text wSpeech, aSpeech, sSpeech, dSpeech;

	public AnimationManager am;

	[HideInInspector]
	public Message wmsg;
	[HideInInspector]
	public Message amsg;
	[HideInInspector]
	public Message smsg;
	[HideInInspector]
	public Message dmsg;

	// Use this for initialization
	void Awake () {
		if (mc == null)
			mc = this;
		else
			Destroy (this.gameObject);
	}

	void Start() {
		wScreen.text = ""; aScreen.text = ""; sScreen.text = ""; dScreen.text = "";
        wSpeech.text = ""; aSpeech.text = ""; sSpeech.text = ""; dSpeech.text = "";

    }

	public void DisplayMessage(Message m) {
		if (m.sendingComp == Computer.W) {
			//wSpeech.text = m.messageText;
			am.TriggerBubble (Computer.W);
			StartCoroutine (MessageTimer(Computer.W, m));
		} else if (m.sendingComp == Computer.A) {
			//aSpeech.text = m.messageText;
			am.TriggerBubble (Computer.A);
			StartCoroutine (MessageTimer(Computer.A, m));
		} else if (m.sendingComp == Computer.S) {
			//sSpeech.text = m.messageText;
			am.TriggerBubble (Computer.S);
			StartCoroutine (MessageTimer(Computer.S, m));
		} else if (m.sendingComp == Computer.D) {
			//dSpeech.text = m.messageText;
			am.TriggerBubble (Computer.D);
			StartCoroutine (MessageTimer(Computer.D, m));
		}
	}

	IEnumerator MessageTimer (Computer comp, Message m) {
		yield return new WaitForSeconds (0.2f);
		if (comp == Computer.W) {
			wSpeech.text = m.messageText;
		} else if (comp == Computer.A) {
			aSpeech.text = m.messageText;
		} else if (comp == Computer.S) {
			sSpeech.text = m.messageText;
		} else if (comp == Computer.D) {
			dSpeech.text = m.messageText;
		}
		yield return new WaitForSeconds (4.8f);
		if (comp == Computer.W) {
			wSpeech.text = "";
		} else if (comp == Computer.A) {
			aSpeech.text = "";
		} else if (comp == Computer.S) {
			sSpeech.text = "";
		} else if (comp == Computer.D) {
			dSpeech.text = "";
		}
	}

	public void DisplayMission(Message m, string mission) {
        if (m.sendingComp == Computer.W)
        {
            wScreen.text = mission;
			if (mission == "")
				wmsg = null;
			else
				wmsg = m;
        }
        else if (m.sendingComp == Computer.A)
        {
            aScreen.text = mission;
			if (mission == "")
				amsg = null;
			else
				amsg = m;
        }
        else if (m.sendingComp == Computer.S)
        {
            sScreen.text = mission;
			if (mission == "")
				smsg = null;
			else
				smsg = m;
        }
        else if (m.sendingComp == Computer.D)
        {
            dScreen.text = mission;
			if (mission == "")
				dmsg = null;
			else
				dmsg = m;
        }
	}
}
