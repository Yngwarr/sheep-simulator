using System;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    public GameObject food;
    public float speed = 5f;

    Rigidbody rigid;
    
    // Start is called before the first frame update
    void Start() {
	    rigid = GetComponent<Rigidbody>();
        if (!food) {
            Debug.LogError("Food is not set.");
        }
    }

    void FixedUpdate() {
        if (!food) return;
        transform.LookAt(food.transform);
        rigid.MovePosition(transform.position + Time.deltaTime * speed * transform.forward);
    }
}
