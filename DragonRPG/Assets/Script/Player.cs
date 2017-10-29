using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable {

	[SerializeField] float maxHeathPoints = 100f;
	[SerializeField] float currentHeathPoints = 100f;
	[SerializeField] float damagePerHit = 10f;
	[SerializeField] float minTimeBetweenHit = 0.5f;
	float lastHitTime = 0f;

	GameObject currentTraget;
	CameraRaycaster cameraRaycaster;

	[SerializeField] Weapon weaponInUse;
	void Start(){
		cameraRaycaster = FindObjectOfType<CameraRaycaster> ();
        PutWeaponInHand();
	}

    [SerializeField] GameObject weaponSocket;
    private void PutWeaponInHand(){
        var weaponPrefab = weaponInUse.GetWeaponPrefab();
        var weapon = Instantiate(weaponPrefab,weaponSocket.transform);
        weapon.transform.localPosition = weaponInUse.gripTransform.localPosition;
        weapon.transform.localRotation = weaponInUse.gripTransform.localRotation;
    }

    [SerializeField] float maxAttackRange = 2f;

	void Update ()
	{
		if (Input.GetMouseButton (0)) {
			if (cameraRaycaster.layerHit == Layer.Enemy) {
				var enemy = cameraRaycaster.hit.collider.gameObject;

				if ((enemy.transform.position - transform.position).magnitude > maxAttackRange) {
					return;
				}

				currentTraget = enemy;
				var enemyCompoenent = enemy.GetComponent<Enemy> ();
				if (Time.time - lastHitTime > minTimeBetweenHit){
					enemyCompoenent.TakeDamage (damagePerHit);
					lastHitTime = Time.time;
				}
			}
		}
	}

	public float healthAsPercentage {
		get {
			return currentHeathPoints / maxHeathPoints;
		}
	}

	public void TakeDamage(float damage){
		currentHeathPoints = Mathf.Clamp (currentHeathPoints - damage, 0f, maxHeathPoints);
	}
}
