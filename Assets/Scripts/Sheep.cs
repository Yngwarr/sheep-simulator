using System;
using UnityEngine;
using UnityEngine.UIElements;

public class Sheep : MonoBehaviour
{
    public GameObject food;
    public GameObject field;
    public float speed = 5f;

    Rigidbody rigid;
    
    // Start is called before the first frame update
    void Start() {
	    rigid = GetComponent<Rigidbody>();
        if (!food) {
            Debug.LogError("Food is not set.");
        }
        if (!field) {
            Debug.LogError("Field is not set.");
        }

        if (food) {
	        food.GetComponent<Food>().Respawn(transform.position, speed);
        }
    }

    void FixedUpdate() {
        if (!food) return;
        var foodDirection = food.transform.position;
        foodDirection.y = transform.position.y;
        transform.LookAt(foodDirection);
        rigid.MovePosition(transform.position + Time.deltaTime * speed * transform.forward);
    }
}
