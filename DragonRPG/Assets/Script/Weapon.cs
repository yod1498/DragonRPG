using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RPG/Weapon")]
public class Weapon : ScriptableObject {

    public Transform gripTransform; 
    [SerializeField] GameObject weaponPrefab;
    [SerializeField] AnimationClip attackAnimation;
	[SerializeField] float minTimeBetweenHit = 0.5f;
	[SerializeField] float maxAttackRange = 2f;
    [SerializeField] float additionalDamage = 10f;

	public float GetAdditionalDamage()
	{
		return additionalDamage;
	}

    public float GetMinTimeBetweenHits(){
        return minTimeBetweenHit;
    }

	public float GetMaxAttackRange()
	{
		return maxAttackRange;
	}

    public GameObject GetWeaponPrefab(){
        return weaponPrefab;
    }

    public AnimationClip GetAttackAnimClip(){
        RemoveAnimationEvents();
        return attackAnimation;
    }

    // So that asset packs cannot cause crashes
    private void RemoveAnimationEvents()
    {
        attackAnimation.events = new AnimationEvent[0];
    }
}
