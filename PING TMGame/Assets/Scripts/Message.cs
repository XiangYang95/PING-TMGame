using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour {
    public float timeLimit = 30.0f;
	public float appearanceTime = 30.0f;
	[TextArea(3,10)]
	public string messageText = "";
	public Computer sendingComp;
	public Computer destinationComp;
	bool arrived;

	float timer;

    void Start () {
		timer = appearanceTime;
		arrived = false;
		messageText = "\t" + messageText;
    }

	void Update () {
		timer -= Time.deltaTime;
		if (timer <= 0f) {
			if (timer > -timeLimit && !arrived) {
				MessageController.mc.DisplayMission (this, DesComp());
			} else {
				if (!arrived) {
					MessageController.mc.DisplayMission (this, "");
					GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ().OnFail (sendingComp);
				}
                Destroy(gameObject);
			}
		}
    }

    string DesComp()
	{
		return (ComputerDict.cd.CompToString [destinationComp] + (int)(timer + timeLimit));
	}

	public float RemoveMessage() { //let timer run out and this will destroy itself
		Debug.Log("Remove Message");
		arrived = true;
		MessageController.mc.DisplayMission(this, "");
		MessageController.mc.DisplayMessage (this);
		return -timer;
	}
}
