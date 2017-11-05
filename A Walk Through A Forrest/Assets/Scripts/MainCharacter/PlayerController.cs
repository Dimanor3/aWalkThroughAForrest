using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (PlayerMotor))]
[RequireComponent (typeof (Stamina))]
[RequireComponent (typeof (CameraShaker))]
public class PlayerController : MonoBehaviour {
	// Camera Shaker
	private CameraShaker shake;

	// Player movement
	private PlayerMotor motor;

	// Status bars
	[SerializeField] private StatusBar staminaBar;

	// Used to move the player
	[SerializeField] private float moveSpeed, runSpeed;
	[SerializeField] private float run;												// Is the player running? Is the player crawling?

	// Used to rotate the player
	[SerializeField] private float rotationSpeed;
	private float rotation;

	// Player stamina
	private Stamina stamina;
	[SerializeField] private float playerStamina, useStaminaSpeed, staminaRegen;

	[SerializeField] private float horizontalMovement;
	[SerializeField] private float verticalMovement;
	[SerializeField] private float mouseX;

	private Vector3 moveHorizontal, moveVertical, movement;

	[SerializeField] private float timer;
	private int rand;

	[SerializeField] private Camera cam;

	[SerializeField] private MusicManager musicManager;
	private bool startDec;

	[SerializeField] private Light light;

	private bool case2 = true, case3 = true, case4 = true, case5 = true, case6 = true, case7 = true, case8 = true, case9 = true;

	void Awake () {
		// Initialize access to all outside classes
		motor = GetComponent<PlayerMotor> ();
		stamina = GetComponent<Stamina> ();
		shake = GetComponent<CameraShaker> ();
	}

	// Use this for initialization
	void Start () {
		// Initialize all required variables
		moveSpeed = 5f;
		runSpeed = 15f;
		playerStamina = 1000f;
		useStaminaSpeed = 5f;
		staminaRegen = 2f;
		rotationSpeed = 2f;
		timer = 30f;
		startDec = true;

		cam = Camera.main;

		// Initialize stamina properties
		stamina.StaminaSG = playerStamina;
		stamina.StaminaLoss = useStaminaSpeed;
		stamina.StaminaRegen = staminaRegen;
		staminaBar.MaxValue = playerStamina;

		musicManager.EarlyLevels.Play ();
		musicManager.EarlyLevels.loop = true;
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;

		if (timer <= 0f) {
			timer = 30f;

			randomEffect ();
		}

		run = Input.GetAxis ("Run");

		// Main Character left right up and down movement
		horizontalMovement = Input.GetAxis ("Horizontal");
		verticalMovement = Input.GetAxis ("Vertical");

		// Main Character turn left and right
		mouseX = Input.GetAxis ("Mouse X");

		// Calculations for main characters movements
		moveHorizontal = transform.right * horizontalMovement;
		moveVertical = transform.forward * verticalMovement;
		movement = (moveHorizontal + moveVertical).normalized;

		// Calculates Rotation
		rotation = mouseX * rotationSpeed;

		// Set player's movement speed
		movement *= running (run);

		// Use the player's stamina?
		useStamina (run, horizontalMovement, verticalMovement);

		// Regen the player's stamina?
		regenStamina (run, horizontalMovement, verticalMovement);

		// Move the player
		motor.Movement = movement;
		motor.Rotation = rotation;
	}

	/// <summary>
	/// Determines whether or not the player is running or walking
	/// and outputs the speed accordingly
	/// </summary>
	/// <param name="run">To see whether the player is pressing the running key.</param>
	/// <param name="crawl">To see whether the player is pressing the crawl key.</param>
	/// <returns>Returns the correct movement multiplyer.</returns>
	float running (float run) {
		if (run != 0 && stamina.StaminaSG > 0) {
			return runSpeed;
		} else {
			return moveSpeed;
		}
	}

	/// <summary>
	/// Checks to see if stamina is being used and uses it if it is.
	/// </summary>
	/// <param name="run">To see whether the player is pressing the running key.</param>
	/// <param name="hM">To see whether the player is moving horizontally.</param>
	/// <param name="vM">To see whether the player is moving vertically.</param>
	void useStamina (float run, float hM, float vM) {
		if (run != 0 && stamina.StaminaSG > 0 && (hM != 0 || vM != 0)) {
			stamina.useStamina ();
			staminaBar.Value = stamina.StaminaSG; //changes the status bar when stamina drains
		}
	}

	/// <summary>
	/// Checks to see whether or not the player can regen their stamina.
	/// </summary>
	/// <param name="run">To see whether the player is pressing the running key.</param>
	/// <param name="hM">To see whether the player is moving horizontally.</param>
	/// <param name="vM">To see whether the player is moving vertically.</param>
	void regenStamina (float run, float hM, float vM) {
		if (run == 0 && hM == 0 && vM == 0) {
			stamina.regen ();
			staminaBar.Value = stamina.StaminaSG; //changes the status bar when stamina regens
		}

		if (run == 0 && (hM != 0 || vM != 0)) {
			stamina.halfRegen ();
			staminaBar.Value = stamina.StaminaSG; //changes the status bar when stamina regens by half
		}

		if (stamina.StaminaSG >= stamina.MaxStamina) {
			stamina.StaminaSG = stamina.MaxStamina;
			staminaBar.Value = stamina.StaminaSG;
		}
	}

	/// <summary>
	/// Sets a random effect.
	/// </summary>
	void randomEffect () {
		while (!case2 || !case3 || !case4 || !case5 || !case6 || !case7 || !case8 || !case9) {
			rand = Random.Range (0, 3);
		}

		switch (rand) {
		case 0:
			shake.Power = Random.Range (.5f, 1.5f);
			shake.SlowDownAmount = Random.Range (1f, 5f);
			shake.Duration = 15f;
			shake.ShouldShake = true;
			break;

		case 1:
			if (musicManager.EarlyLevels.pitch <= 0) {
				startDec = false;
			}

			if (musicManager.EarlyLevels.pitch >= 1) {
				startDec = true;
			}

			if (startDec) {
				musicManager.EarlyLevels.pitch -= .04f;
			} else {
				musicManager.EarlyLevels.pitch += .04f;
			}
			break;

		case 2:
			light.intensity -= .07f;

			if (light.intensity <= 0) {
				light.enabled = false;
				case2 = false;
			}
			break;

		case 3:
			break;
		}
	}
}
