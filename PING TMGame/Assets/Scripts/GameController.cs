using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController:MonoBehaviour {
    public Canvas canvas;
    public GameObject win, lose;

	public float goodPing = 45f;
	public string nextLevel = "Matt";
	public float endLevelWaitTime = 3f;

	bool gameOver = false;

    void Start()
    {
        canvas.gameObject.SetActive(false);
        lose.SetActive(false);
        win.SetActive(false);
    }

    void Update()
    {
        if (!gameOver && GameObject.FindGameObjectsWithTag("message").Length == 0)
        {
			gameOver = true;
            canvas.gameObject.SetActive(true);
            float ping = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().AveragePing();
			if (ping > goodPing) {
				lose.SetActive (true);
				StartCoroutine (ChangeLevel (SceneManager.GetActiveScene ().name));
			} else {
				win.SetActive (true);
				StartCoroutine (ChangeLevel (nextLevel));
			}
        }
    }

	IEnumerator ChangeLevel(string lvl) {
		yield return new WaitForSeconds (endLevelWaitTime);
		SceneManager.LoadScene (lvl);
	}
}
