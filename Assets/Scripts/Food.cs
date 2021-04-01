using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Food : MonoBehaviour {
	FoodController ctrl;
	
	void Start() {
		ctrl = GetComponentInParent<FoodController>();
	}
	
    void OnTriggerEnter(Collider other) {
	    var sheep = other.gameObject.GetComponent<Sheep>();
	    if (!sheep) return;
	    ctrl.Respawn(this, sheep);
    }
}
