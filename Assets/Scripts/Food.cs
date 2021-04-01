using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Food : MonoBehaviour {
	const float SPAWN_RANGE = 5f;

    public void Respawn(Vector3 center, float speed) {
	    /* starts with one because we don't want to overlap the sheep */
	    /* TODO 1f -> sheep.scale/2 + food.scale/2, given the standard radius ~ 1 */
	    var radius = Random.Range(1f, speed * SPAWN_RANGE);
	    var angle = Random.value * Mathf.PI * 2;
	    
	    transform.position = new Vector3(
		    center.x + radius * Mathf.Cos(angle),
		    transform.position.y,
		    center.z + radius * Mathf.Sin(angle)
		);
	    Debug.Log($"{radius * Mathf.Cos(angle)} {radius * Mathf.Sin(angle)}");
    }
	
    void OnTriggerEnter(Collider other) {
	    var sheep = other.gameObject.GetComponent<Sheep>();
	    if (!sheep) return;
	    Respawn(sheep.transform.position, sheep.speed);
    }
}
