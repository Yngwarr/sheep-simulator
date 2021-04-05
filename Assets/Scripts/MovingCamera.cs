using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCamera : MonoBehaviour
{
	Vector3 input;
	
	void Update() {
		input.x = Input.GetAxisRaw("Horizontal");
		input.z = Input.GetAxisRaw("Vertical");
	}
	
	void FixedUpdate() {
		transform.Translate(input, Space.World);
	}
}
