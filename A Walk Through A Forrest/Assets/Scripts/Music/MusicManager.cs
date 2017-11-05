using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

	[SerializeField] private AudioSource earlyLevels;

	public AudioSource EarlyLevels {
		get { 
			return earlyLevels;
		}
	}
}
