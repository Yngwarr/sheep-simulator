using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCamera : MonoBehaviour
{
	public float speed = 10f;
	public float altSpeed = 5f;
	public float minScale = 1f;
	public float maxScale = 10f;

	Vector3 input;
	bool alterSpeed = false;
	float lastTime;
	
	float CropScale(float delta) {
		var scale = transform.position.y;
		return scale <= minScale && delta < 0f || scale >= maxScale && delta > 0f ? 0f : delta;
	}
	
	void Start() {
		lastTime = Time.realtimeSinceStartup;
	}
	
	void Update() {
		input.x = Input.GetAxisRaw("Horizontal");
		input.y = CropScale(Input.GetAxisRaw("Scale"));
		input.z = Input.GetAxisRaw("Vertical");
		alterSpeed = Input.GetButton("AltSpeed");
		
		var now = Time.realtimeSinceStartup;
		transform.Translate(
			(alterSpeed ? altSpeed : speed) * (now - lastTime) * input.normalized,
			Space.World
		);
		lastTime = now;
	}
	
	void FixedUpdate() {
	}
}
