using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour {

	private float power, duration, slowDownAmount, initialDuration;
	[SerializeField] private Transform camera;
	private bool shouldShake = false;

	[SerializeField] private Vector3 startPosition;

	// Use this for initialization
	void Start () {
		//camera = Camera.main.transform;
		startPosition = camera.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		if (shouldShake) {
			if (duration > 0) {
				camera.localPosition = startPosition + Random.insideUnitSphere * power;
				duration -= Time.deltaTime * slowDownAmount;
			} else {
				shouldShake = false;
				duration = initialDuration;
				camera.localPosition = startPosition;
			}
		}
	}

	/// <summary>
	/// Sets the power.
	/// </summary>
	/// <value>The power.</value>
	public float Power {
		set {
			power = value;
		}
	}

	/// <summary>
	/// Sets the duration.
	/// </summary>
	/// <value>The duration.</value>
	public float Duration {
		set {
			initialDuration = duration = value;
		}
	}

	/// <summary>
	/// Sets the slow down amount.
	/// </summary>
	/// <value>The slow down amount.</value>
	public float SlowDownAmount {
		set {
			slowDownAmount = value;
		}
	}

	/// <summary>
	/// Sets a value indicating whether this <see cref="cameraShaker"/> should shake.
	/// </summary>
	/// <value><c>true</c> if should shake; otherwise, <c>false</c>.</value>
	public bool ShouldShake {
		set {
			shouldShake = value;
		}
	}
}
