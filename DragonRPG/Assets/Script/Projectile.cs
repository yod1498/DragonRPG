using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	[SerializeField] float projectilespeed = 10f;
	float damageCaused = 10f;

	public float DamageCaused { get; set; }
	public float Projectilespeed { get; set; }


	void OnTriggerEnter(Collider collider){
		Component damageableComponent = collider.gameObject.GetComponent (typeof(IDamagable));
		if (damageableComponent) {
			(damageableComponent as IDamagable).TakeDamage (damageCaused);
		}
	}
}
