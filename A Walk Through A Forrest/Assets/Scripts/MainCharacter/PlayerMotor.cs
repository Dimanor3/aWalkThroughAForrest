using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour {
	// Access to the main characters rigidbody
	private Rigidbody rb;

	// Movement stuff
	[SerializeField] private Vector3 movement;		// Move the player

	// Rotation stuff
	[SerializeField] private float rotation;		// Rotate the player
	//private float rotationSpeed;	// Rotation speed

	void Awake () {
		// Initialize the main characters rigidbody
		rb = GetComponent<Rigidbody> ();
	}

	// Use this for initialization
	void Start () {
		// Player movement initialization
		movement = Vector3.zero;

		// Player rotation initialization
		rotation = 0f;
		//rotationSpeed = 0f;
	}
	
	// Run every physics iteration
	void FixedUpdate () {
		movePlayer ();
		rotatePlayer ();

		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
	}

	/// <summary>
	/// Sets the movement.
	/// </summary>
	/// <value>The movement.</value>
	public Vector3 Movement {
		set {
			movement = value;
		}
	}

	/// <summary>
	/// Sets the rotation.
	/// </summary>
	/// <value>The rotation.</value>
	public float Rotation {
		set { 
			rotation = value;
		}
	}

	/// <summary>
	/// Set the players rotation
	/// </summary>
	/*
	public float RotationSpeed {
		set {
			rotationSpeed = value;
		}
	}
	*/

	/// <summary>
	/// Moves the player.
	/// </summary>
	void movePlayer () {
		if (movement != Vector3.zero) {
			rb.MovePosition (rb.position + movement * Time.fixedDeltaTime);

			//rb.AddForce (movement);// * Time.deltaTime);
		}
	}

	void rotatePlayer () {
		if (rotation != 0f) {
			transform.localEulerAngles += Quaternion.Euler (new Vector3 (0f, rotation, 0f)).eulerAngles;
		}
	}
}
