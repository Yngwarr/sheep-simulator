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
	public float boundsMargin = 5f;

	float lastTime;
	Bounds bounds;
	
	float BoundAngle(float delta) {
		var angle = transform.eulerAngles.x;
		return angle <= minAngle && delta < 0f || angle >= maxAngle && delta > 0f ? 0f : delta;
	}
	
	Vector3 BoundPosition(float dx, float dy, float dz) {
		var pos = transform.position;
		var x = pos.x <= bounds.min.x - boundsMargin && dx < 0f
			|| pos.x >= bounds.max.x + boundsMargin && dx > 0f ? 0f : dx;
		var y = pos.y <= minScale && dy < 0f || pos.y >= maxScale && dy > 0f ? 0f : dy;
		var z = pos.z <= bounds.min.z - boundsMargin && dz < 0f 
		    || pos.z >= bounds.max.z + boundsMargin && dz > 0f ? 0f : dz;
		return new Vector3(x, y, z);
	}
	
	public void Init(Bounds _bounds) {
		bounds = _bounds;
	}
	
	void Start() {
		lastTime = Time.realtimeSinceStartup;
	}
	
	void Update() {
		var now = Time.realtimeSinceStartup;
		var dt = now - lastTime;
		lastTime = now;
		
		if (!on) return;
		Vector3 input = BoundPosition(
			Input.GetAxisRaw("Horizontal"),
			Input.GetAxisRaw("Scale"),
			Input.GetAxisRaw("Vertical")
		);
		var inputAngle = Input.GetAxisRaw("Rotation");
		var alterSpeed = Input.GetButton("AltSpeed");
		
		var delta = (alterSpeed ? altSpeed : speed) * dt;
		transform.Translate(delta * input.normalized, Space.World);
		transform.Rotate(new Vector3(2 * delta * BoundAngle(inputAngle), 0f, 0f));
	}
}
