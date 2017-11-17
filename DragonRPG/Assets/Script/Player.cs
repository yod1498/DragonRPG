using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.CameraUI;

public class Player : MonoBehaviour, IDamagable {
    
	[SerializeField] float maxHeathPoints = 100f;
	[SerializeField] float currentHeathPoints = 100f;
	[SerializeField] float damagePerHit = 10f;
	//[SerializeField] float minTimeBetweenHit = 0.5f;
	//[SerializeField] float maxAttackRange = 2f;
	[SerializeField] GameObject weaponSocket;

	void Update()
	{
		if (Input.GetMouseButton(0))
		{
			if (cameraRaycaster.layerHit == Layer.Enemy)
			{
				var enemy = cameraRaycaster.hit.collider.gameObject;

				if ((enemy.transform.position - transform.position)
                    .magnitude > weaponInUse.GetMaxAttackRange())
				{
					return;
				}

				currentTraget = enemy;
				var enemyCompoenent = enemy.GetComponent<Enemy>();
				if (Time.time - lastHitTime > weaponInUse.GetMinTimeBetweenHits())
				{
                    OverrideAnimatorController();
					animator.SetTrigger("Attack");
                    enemyCompoenent.TakeDamage(damagePerHit + weaponInUse.GetAdditionalDamage());
					lastHitTime = Time.time;
				}
			}
		}
	}

	float lastHitTime = 0f;

	GameObject currentTraget;
	CameraRaycaster cameraRaycaster;

	[SerializeField] Weapon weaponInUse;
	void Start(){
		cameraRaycaster = FindObjectOfType<CameraRaycaster> ();
        PutWeaponInHand(weaponInUse);
        OverrideAnimatorController();
	}

    GameObject weaponObject;
    public void PutWeaponInHand(Weapon weaponConfig)
	{
		weaponInUse = weaponConfig;
        var weaponPrefab = weaponConfig.GetWeaponPrefab();
		Destroy(weaponObject);//empty hands
        weaponObject = Instantiate(weaponPrefab, weaponSocket.transform);
		weaponObject.transform.localPosition = weaponInUse.gripTransform.localPosition;
		weaponObject.transform.localRotation = weaponInUse.gripTransform.localRotation;
	}

	[SerializeField] AnimatorOverrideController animatorOverrideController;
	Animator animator;
	private void OverrideAnimatorController()
	{
		animator = GetComponent<Animator>();
		animator.runtimeAnimatorController = animatorOverrideController;
		animatorOverrideController["Default Attack"] = weaponInUse.GetAttackAnimClip();
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
