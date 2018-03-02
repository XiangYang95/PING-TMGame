using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class TempPlayer : MonoBehaviour {

	public float speed = 2f;

	Rigidbody2D rb;

	void Awake () {
		rb = GetComponent<Rigidbody2D> ();
	}

	void Update () {
		rb.velocity = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical")) * speed;
	}
}
