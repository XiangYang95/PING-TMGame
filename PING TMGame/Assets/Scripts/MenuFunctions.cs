using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFunctions : MonoBehaviour {

	public Canvas mainCanvas;
	public Canvas controlsCanvas;

	Rigidbody2D rb;

	public void FlipControlsScreen() {
		bool state = mainCanvas.enabled;
		mainCanvas.enabled = !state;
		controlsCanvas.enabled = state;
	}

	public void StartGame() {
		SceneManager.LoadScene ("Story");
	}

	public void QuitGame() {
		Application.Quit ();
	}
}
