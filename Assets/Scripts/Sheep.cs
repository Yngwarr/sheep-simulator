using System;
using UnityEngine;
using UnityEngine.UIElements;

public class Sheep : GameComponent {
    public GameObject food;
    public float speed = 5f;

    Rigidbody rigid;

    // Start is called before the first frame update
    void Start() {
	    rigid = GetComponent<Rigidbody>();
        if (!food) {
            Debug.LogError("Food is not set.", this);
        }
    }

    void FixedUpdate() {
        if (!food) return;
        var foodDirection = food.transform.position;
        transform.LookAt(foodDirection);
        
        var step = Time.fixedDeltaTime * speed;
        var foodDist = Vector3.Distance(transform.position, food.transform.position);
        rigid.MovePosition(transform.position + (step < foodDist ? step : (foodDist)) * transform.forward);
    }
}
