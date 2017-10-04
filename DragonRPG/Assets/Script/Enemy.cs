using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : MonoBehaviour, IDamagable{

	[SerializeField] float maxHeathPoints = 100f;
	[SerializeField] float currentHeathPoints = 100f;
	[SerializeField] float chaseRadius = 6f;
	[SerializeField] float attackRadius = 4f;

	[SerializeField] float damagePerShot = 9f;
	[SerializeField] GameObject projectileToUse;
	[SerializeField] GameObject projectileSocket;
	[SerializeField] float secondBetweenShots = 0.5f;

	bool isAttacking = false;
	AICharacterControl  aICharacterControl = null;
	GameObject player = null;

	public void TakeDamage(float damage){
		currentHeathPoints = Mathf.Clamp (currentHeathPoints - damage, 0f, maxHeathPoints);
		if (currentHeathPoints <= 0) { Destroy (gameObject);}
	}

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

		if (distanceToPlayer <= attackRadius && !isAttacking) {
			isAttacking = true;
			InvokeRepeating ("SpawnProjectile", 0f, secondBetweenShots);
		}

		if (distanceToPlayer > attackRadius) {
			isAttacking = false;
			CancelInvoke ();
		}

		if (distanceToPlayer <= chaseRadius) {
			aICharacterControl.SetTarget (player.transform);
		} else {
			aICharacterControl.SetTarget (transform);
		}
	}

	[SerializeField] Vector3 aimOffset = new Vector3(0,1f,0);

	void SpawnProjectile(){
		GameObject newProjecttile = Instantiate (projectileToUse, projectileSocket.transform.position, Quaternion.identity);
		Projectile projectileComponent = newProjecttile.GetComponent<Projectile> ();
		projectileComponent.DamageCaused = damagePerShot;

		Vector3 unitVectorToPlayer = (player.transform.position + aimOffset - projectileSocket.transform.position).normalized;
		float projectileSpeed = projectileComponent.ProjectileSpeed;
		newProjecttile.GetComponent<Rigidbody> ().velocity = unitVectorToPlayer * projectileSpeed;
	}

	void OnDrawGizmos(){
		//Draw attack sphere
		Gizmos.color = new Color(255f,0f,0,.5f);
		Gizmos.DrawWireSphere (transform.position, attackRadius);

		//Draw chase sphere
		Gizmos.color = new Color(0f,0f,255f,.5f);
		Gizmos.DrawWireSphere (transform.position, chaseRadius);
	}
}
