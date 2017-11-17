using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode] // editor firing callback
public class WeaponPickupPoint : MonoBehaviour {

    [SerializeField] Weapon weaponConfig;
    [SerializeField] AudioClip pickUpSFX;
    AudioSource audioSource;
	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!Application.isPlaying)
        {
            DestroyChildren();
            InstantiateWeapon();
        }
	}

    private void DestroyChildren()
    {
        foreach (Transform child in transform){
            DestroyImmediate(child.gameObject);
        }
    }

    private void InstantiateWeapon()
    {
        var weapon = weaponConfig.GetWeaponPrefab();
        weapon.transform.position = Vector3.zero;
        Instantiate(weapon,gameObject.transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<Player>().PutWeaponInHand(weaponConfig);
        audioSource.PlayOneShot(pickUpSFX);
    }
}
