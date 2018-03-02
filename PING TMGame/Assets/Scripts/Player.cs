using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
	[Header("Grid-based Movement")]
	public Grid grid;
	[Tooltip("Number of FixedUpdate frames it takes to reach next grid space")]
	public float timePerMove = 10f;

	// Signals
	[Header("Signals")]

	public GameObject  wSignal; public GameObject aSignal, sSignal, dSignal;
	SpriteRenderer wSprite, aSprite, sSprite, dSprite;
	Vector2 wStart, aStart, sStart, dStart;

	[Header("Scoring")]
	[Tooltip("Amount of time added to total time for failures")]
	public float failurePenalty = 60f;

	[HideInInspector]
	public Computer curr;
	[HideInInspector]
	public Message msg;

	float totalTime;
	int successes;
	public int numberOfMessages = 1;

	float raycastDistance;
	LayerMask tilemapLayer;
	bool isMoving;

    void Awake()
    {
		wSprite = wSignal.GetComponent<SpriteRenderer>();
        aSprite = aSignal.GetComponent<SpriteRenderer>();
        sSprite = sSignal.GetComponent<SpriteRenderer>();
        dSprite = dSignal.GetComponent<SpriteRenderer>();

		curr = Computer.S;
		sSprite.enabled = false;
		totalTime = 0f;
		successes = 0;

		this.gameObject.tag = "Player";

		GridAlign ();
    }

	void GridAlign() {
		Vector3Int space = grid.WorldToCell (transform.position);
		transform.position = grid.GetCellCenterWorld(space);

		wSignal.transform.position = grid.GetCellCenterWorld (grid.WorldToCell(wSignal.transform.position));
		aSignal.transform.position = grid.GetCellCenterWorld (grid.WorldToCell(aSignal.transform.position));
		sSignal.transform.position = grid.GetCellCenterWorld (grid.WorldToCell(sSignal.transform.position));
		dSignal.transform.position = grid.GetCellCenterWorld (grid.WorldToCell(dSignal.transform.position));

		wStart = wSignal.transform.position;
		aStart = aSignal.transform.position;
		sStart = sSignal.transform.position;
		dStart = dSignal.transform.position;
	}

	void Start () {
		tilemapLayer = 1 << LayerMask.NameToLayer ("Tilemap");
		raycastDistance = grid.cellSize.y * grid.gameObject.transform.localScale.y;
		isMoving = false;
	}

	void Update()
	{
		SwitchComputers ();

        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(0);
	}

	void SwitchComputers() {
		if (Input.GetKeyDown(KeyCode.W))
		{
			this.transform.position = new Vector2(wSignal.transform.position.x, wSignal.transform.position.y);
			curr = Computer.W;
			msg = MessageController.mc.wmsg;
			wSprite.enabled = false; aSprite.enabled = true; sSprite.enabled = true; dSprite.enabled = true;
		}
		else if (Input.GetKeyDown(KeyCode.A)) {
			this.transform.position = aSignal.transform.position;
			curr = Computer.A;
			msg = MessageController.mc.amsg;
			aSprite.enabled = false; wSprite.enabled = true; sSprite.enabled = true; dSprite.enabled = true;
		}
		else if (Input.GetKeyDown(KeyCode.S)) { 
			this.transform.position = new Vector2(sSignal.transform.position.x, sSignal.transform.position.y);
			curr = Computer.S;
			msg = MessageController.mc.smsg;
			sSprite.enabled = false; aSprite.enabled = true; wSprite.enabled = true; dSprite.enabled = true;
		}
		else if (Input.GetKeyDown(KeyCode.D))
		{
			this.transform.position = new Vector2(dSignal.transform.position.x, dSignal.transform.position.y);
			curr = Computer.D;
			msg = MessageController.mc.dmsg;
			dSprite.enabled = false; aSprite.enabled = true; sSprite.enabled = true; wSprite.enabled = true;
		}
	}

	void FixedUpdate() {
		TryGridMove();
		MoveSignal ();
    }

	void TryGridMove() {
		if (!isMoving) {
			Vector2 dir;
			if (Input.GetKey (KeyCode.UpArrow)) {
				dir = Vector2.up;
			} else if (Input.GetKey (KeyCode.DownArrow)) {
				dir = Vector2.down;
			} else if (Input.GetKey (KeyCode.LeftArrow)) {
				dir = Vector2.left;
			} else if (Input.GetKey (KeyCode.RightArrow)) {
				dir = Vector2.right;
			} else
				return;

			RaycastHit2D hit = Physics2D.Raycast (transform.position, dir, raycastDistance, tilemapLayer);
			//Debug.DrawRay (transform.position, dir, Color.red);
			if (hit.collider == null) {
				isMoving = true;
				StartCoroutine (Move (dir));
			} 
		}
	}

	IEnumerator Move(Vector2 dir) {
		Vector3Int gridSpace = grid.WorldToCell (transform.position); 
		Vector3Int target = new Vector3Int (gridSpace.x + (int)dir.x, gridSpace.y + (int)dir.y, gridSpace.z);
		Vector3 diff = (grid.GetCellCenterWorld(target) - grid.GetCellCenterWorld(gridSpace))/timePerMove;

		while ((grid.GetCellCenterWorld(target) - transform.position).magnitude > diff.magnitude) {
			transform.position += diff;
			yield return new WaitForFixedUpdate ();
		}

		transform.position = grid.GetCellCenterWorld (target);
		isMoving = false;
	}

	void MoveSignal() {
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
		StartCoroutine (ReturnToStartPosition ());
	}

	IEnumerator ReturnToStartPosition() {
		while (isMoving) {
			yield return new WaitForFixedUpdate ();
		}

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
		if (curr == com) {
			StartCoroutine (ReturnToStartPosition ());
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
		Debug.Log ("Ping: " + (totalTime / numberOfMessages));
		return (totalTime / numberOfMessages);
	}
}
