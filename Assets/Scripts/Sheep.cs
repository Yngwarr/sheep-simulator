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
	        foodDist = Vector3.Distance(previousPos, food.transform.position);
        }
        
        var direction = (food.transform.position - previousPos).normalized;
        var angle = Vector3.SignedAngle(Vector3.forward, direction, Vector3.up);
        rigid.rotation = Quaternion.AngleAxis(angle, Vector3.up);
        rigid.MovePosition(previousPos + step * direction);
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
