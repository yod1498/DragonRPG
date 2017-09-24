using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable {

	[SerializeField] float maxHeathPoints = 100f;
	float currentHeathPoints = 100f;

	public float healthAsPercentage {
		get {
			return currentHeathPoints / maxHeathPoints;
		}
	}

	public void TakeDamage(float damage){
		// ("TakeDamage = " + damage);
		currentHeathPoints = Mathf.Clamp (currentHeathPoints - damage, 0f, maxHeathPoints);
	}
}
