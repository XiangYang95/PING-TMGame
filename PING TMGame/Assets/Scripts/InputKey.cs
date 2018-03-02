using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputKey : MonoBehaviour {
	public GameObject aSignal, wSignal, sSignal, dSignal;
    public float speed;
    SpriteRenderer aSprite, wSprite, sSprite, dSprite;

	Vector2 wStart, aStart, sStart, dStart;

	Rigidbody2D rb2D;

	public float failurePenalty = 60f;

	[HideInInspector]
	public Computer curr;
	[HideInInspector]
	public Message msg;

	float totalTime;
	int successes;
	public int numberOfMessages = 1;

    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        aSprite = aSignal.GetComponent<SpriteRenderer>();
        wSprite = wSignal.GetComponent<SpriteRenderer>();
        sSprite = sSignal.GetComponent<SpriteRenderer>();
        dSprite = dSignal.GetComponent<SpriteRenderer>();
		curr = Computer.S;
		sSprite.enabled = false;
		totalTime = 0f;
		successes = 0;
    }

	void Start () {
		wStart = wSignal.transform.position;
		aStart = aSignal.transform.position;
		sStart = sSignal.transform.position;
		dStart = dSignal.transform.position;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.A)) {
            this.transform.position = aSignal.transform.position;
			curr = Computer.A;
			msg = MessageController.mc.amsg;
            aSprite.enabled = false; wSprite.enabled = true; sSprite.enabled = true; dSprite.enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.S)) { 
			this.transform.position = new Vector2(sSignal.transform.position.x, sSignal.transform.position.y);
			curr = Computer.S;
			msg = MessageController.mc.smsg;
            sSprite.enabled = false; aSprite.enabled = true; wSprite.enabled = true; dSprite.enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            this.transform.position = new Vector2(wSignal.transform.position.x, wSignal.transform.position.y);
			curr = Computer.W;
			msg = MessageController.mc.wmsg;
            wSprite.enabled = false; aSprite.enabled = true; sSprite.enabled = true; dSprite.enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            this.transform.position = new Vector2(dSignal.transform.position.x, dSignal.transform.position.y);
			curr = Computer.D;
			msg = MessageController.mc.dmsg;
            dSprite.enabled = false; aSprite.enabled = true; sSprite.enabled = true; wSprite.enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(0);
	}

	void FixedUpdate() {
        rb2D.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * speed;
		if (curr == Computer.W)
			wSignal.transform.position = this.transform.position;
		else if (curr == Computer.A)
			aSignal.transform.position = this.transform.position;
		else if (curr == Computer.S)
			sSignal.transform.position = this.transform.position;
		else
			dSignal.transform.position = this.transform.position;
    }

	public void OnArrive() {
		successes++;
		totalTime += msg.RemoveMessage ();
		Debug.Log ("Arrived!");
		if (curr == Computer.W) {
			this.transform.position = wStart;
			wSignal.transform.position = wStart;
		} else if (curr == Computer.A) {
			this.transform.position = aStart;
			aSignal.transform.position = aStart;
		} else if (curr == Computer.S) {
			this.transform.position = sStart;
			sSignal.transform.position = sStart;
		} else if (curr == Computer.D) {
			this.transform.position = dStart;
			dSignal.transform.position = dStart;
		}
	}

	public void OnFail(Computer com) {
		Debug.Log ("Fail!");
		if (curr == com) {
			if (curr == Computer.W) {
				this.transform.position = wStart;
				wSignal.transform.position = wStart;
			} else if (curr == Computer.A) {
				this.transform.position = aStart;
				aSignal.transform.position = aStart;
			} else if (curr == Computer.S) {
				this.transform.position = sStart;
				sSignal.transform.position = sStart;
			} else if (curr == Computer.D) {
				this.transform.position = dStart;
				dSignal.transform.position = dStart;
			}
		} else {
			if (com == Computer.W) {
				wSignal.transform.position = wStart;
			} else if (com == Computer.A) {
				aSignal.transform.position = aStart;
			} else if (com == Computer.S) {
				sSignal.transform.position = sStart;
			} else if (com == Computer.D) {
				dSignal.transform.position = dStart;
			}
		}
	}

	public float AveragePing() {
		while (successes <= numberOfMessages) {
			totalTime += failurePenalty;
			successes++;
		}
		return (totalTime / numberOfMessages);
	}
}
