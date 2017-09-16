using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	[SerializeField] float maxHeathPoints = 100f;
	float currentHeathPoints = 100f;

	public float healthAsPercentage {
		get {
			return currentHeathPoints / maxHeathPoints;
		}
	}
}
