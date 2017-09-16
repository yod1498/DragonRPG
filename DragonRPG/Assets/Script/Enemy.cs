using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : MonoBehaviour {

	[SerializeField] float maxHeathPoints = 100f;
	float currentHeathPoints = 100f;

	[SerializeField] float attackRadius = 4f;
	AICharacterControl  aICharacterControl = null;
	GameObject player = null;

	public float healthAsPercentage {
		get {
			return currentHeathPoints / maxHeathPoints;
		}
	}

	void Start(){
		player = GameObject.FindGameObjectWithTag ("Player");
		aICharacterControl = GetComponent<AICharacterControl> ();
	}

	void Update(){
		float distanceToPlayer = Vector3.Distance (player.transform.position, transform.position);
		if (distanceToPlayer <= attackRadius) {
			aICharacterControl.SetTarget (player.transform);
		} else {
			aICharacterControl.SetTarget (transform);
		}
	}
}
