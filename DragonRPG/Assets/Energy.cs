using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Energy : MonoBehaviour {

    [SerializeField] RawImage helathBar;
    [SerializeField] float maxEnergyPoints = 100f;
    float currentEnergyPoints;
	// Use this for initialization
	void Start () {
        currentEnergyPoints = maxEnergyPoints;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
