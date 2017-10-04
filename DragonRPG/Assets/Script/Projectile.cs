using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	[SerializeField] float projectileSpeed = 10f;
	float damageCaused = 10f;

	public float DamageCaused {
		get {return damageCaused;}
		set {damageCaused = value;}
	}

	public float ProjectileSpeed {
		get {return projectileSpeed;}
		set {projectileSpeed = value;}
	}


	void OnTriggerEnter(Collider collider){
		Component damageableComponent = collider.gameObject.GetComponent (typeof(IDamagable));
		if (damageableComponent) {
			(damageableComponent as IDamagable).TakeDamage (damageCaused);
		}
	}
}
