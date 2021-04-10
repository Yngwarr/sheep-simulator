using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Food : GameComponent {
	FoodController ctrl;
	Renderer _renderer;

	void Start() {
		ctrl = GetComponentInParent<FoodController>();
		_renderer = GetComponent<Renderer>();
	}

	void OnTriggerStay(Collider other) {
	    var sheep = other.gameObject.GetComponent<Sheep>();
	    if (!sheep) return;
	    if (sheep.food != this) return;
	    
		Respawn(sheep.transform.position, sheep.speed);
    }
	
	public void Respawn(Vector3 sheepPos, float sheepSpeed) {
	    EventManager.Get().foodEaten.Invoke(transform.position, _renderer.isVisible);
		ctrl.Respawn(this, sheepPos, sheepSpeed);
	}
}
