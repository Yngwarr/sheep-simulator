using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCamera : MonoBehaviour
{
	public bool on = false;
	public float speed = 10f;
	public float altSpeed = 5f;
	public float minScale = 1f;
	public float maxScale = 10f;
	public float minAngle = 15f;
	public float maxAngle = 75f;

	float lastTime;
	
	float CropScale(float delta) {
		var scale = transform.position.y;
		return scale <= minScale && delta < 0f || scale >= maxScale && delta > 0f ? 0f : delta;
	}
	
	float CropAngle(float delta) {
		var angle = transform.eulerAngles.x;
		return angle <= minAngle && delta < 0f || angle >= maxAngle && delta > 0f ? 0f : delta;
	}
	
	void Start() {
		lastTime = Time.realtimeSinceStartup;
	}
	
	void Update() {
		var now = Time.realtimeSinceStartup;
		var dt = now - lastTime;
		lastTime = now;
		
		if (!on) return;
		Vector3 input;
		input.x = Input.GetAxisRaw("Horizontal");
		input.y = CropScale(Input.GetAxisRaw("Scale"));
		input.z = Input.GetAxisRaw("Vertical");
		var inputAngle = Input.GetAxisRaw("Rotation");
		var alterSpeed = Input.GetButton("AltSpeed");
		
		var delta = (alterSpeed ? altSpeed : speed) * dt;
		transform.Translate(delta * input.normalized, Space.World);
		transform.Rotate(new Vector3(2 * delta * CropAngle(inputAngle), 0f, 0f));
	}
}
