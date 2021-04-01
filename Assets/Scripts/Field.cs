using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour {
	const int MAX_SIZE = 1000;
	const int MIN_SIZE = 2;
	
	Renderer _renderer;
	
	int _size = 10;
	public int Size {
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
		}
	}
	
	public bool Contains(Vector2 point) {
		return _renderer.bounds.Contains(new Vector3(point.x, transform.position.y, point.y));
	}
	
	void Start() {
		_renderer = GetComponent<Renderer>();
		Size = 15;
	}
}
