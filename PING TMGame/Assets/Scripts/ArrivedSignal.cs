using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrivedSignal : MonoBehaviour {

	public Computer comp;

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			Player player = other.gameObject.GetComponent<Player> ();
			if (player.msg.destinationComp == comp) {
				player.OnArrive ();
			}
		}
	}
}
