using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class FoodGrid
{
	bool[] grid;
	int size;
	float step;
	
	public FoodGrid(int _size, int pointsPerUnit) {
		grid = new bool[_size * _size];
		size = _size;
		step = 1f / pointsPerUnit;
		Debug.Log($"grid is {grid.Length}, size = {_size}");
	}
	
	public Vector3 Real(Vector2Int point, float height) {
		var offset = (size - step) / 2f;
		return new Vector3(point.x * step - offset, height, point.y * step - offset);
	}
}
