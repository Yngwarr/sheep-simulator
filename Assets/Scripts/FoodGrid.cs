using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class FoodGrid
{
	bool[] grid;
	public int size;
	float step;
	
	public FoodGrid(int _size, int pointsPerUnit) {
		grid = new bool[_size * _size * pointsPerUnit * pointsPerUnit];
		size = _size;
		step = 1f / pointsPerUnit;
		Debug.Log($"grid is {grid.Length}, size = {_size}");
	}
	
	public Vector3 Real(Vector2Int point, float height) {
		var offset = (size - step) / 2f;
		return new Vector3(point.x * step - offset, height, point.y * step - offset);
	}
	
	public Vector2Int Snap(Vector3 point) {
		var offset = (size - step) / 2f;
		return new Vector2Int((int) ((point.x + offset) / step), (int) ((point.z + offset) / step));
	}
	
	public int Index(Vector2Int point) {
		return point.x * size * 2 + point.y;
	}
}
