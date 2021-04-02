using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : GameComponent {
	const int MAX_SIZE = 1000;
	const int MIN_SIZE = 2;
	
	Renderer _renderer;
	
	int _size = 10;
	public int size {
		get => _size;
		set {
			if (value < MIN_SIZE) {
				value = MIN_SIZE;
			}
			if (value > MAX_SIZE) {
				value = MAX_SIZE;
			}
			var scale = value / 10f;
			transform.localScale = new Vector3(scale, 1, scale);
			_renderer.material.mainTextureScale = new Vector2(scale, scale);
			_size = value;
		}
	}
	
	public Bounds bounds => _renderer.bounds;
	
	public bool Contains(Vector2 point) {
		return bounds.Contains(new Vector3(point.x, transform.position.y, point.y));
	}
	
	void Awake() {
		_renderer = GetComponent<Renderer>();
		size = 15;
	}
}
