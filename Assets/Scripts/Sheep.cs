using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class Sheep : GameComponent {
    public Food food;
    public float speed = 5f;
    public bool collideWhenInvisible = true;
    
    Rigidbody rigid;
    int defaultLayer;
    int invisibleLayer;

    // Start is called before the first frame update
    void Start() {
	    rigid = GetComponent<Rigidbody>();
	    defaultLayer = gameObject.layer;
	    invisibleLayer = LayerMask.NameToLayer("InvisibleSheeps");
        if (!food) {
            Debug.LogError("Food is not set.", this);
        }
    }

    void FixedUpdate() {
        if (!food) return;
        
        var step = Time.fixedDeltaTime * speed;
        var foodDist = Vector3.Distance(transform.position, food.transform.position);
        
        /* this is here just for the sake of fair simulation */
        var previousPos = transform.position;
        while (step > foodDist) {
	        step -= foodDist;
	        previousPos = food.transform.position;
	        food.Respawn(previousPos, speed);
        }
        
        transform.position = previousPos;
        transform.LookAt(food.transform.position);
        rigid.MovePosition(previousPos + step * transform.forward);
    }

    void OnBecameVisible() {
	    if (collideWhenInvisible) return;
		gameObject.layer = defaultLayer;
    }

    void OnBecameInvisible() {
	    if (collideWhenInvisible) return;
		gameObject.layer = invisibleLayer;
    }
}
