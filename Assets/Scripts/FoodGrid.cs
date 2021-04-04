using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class FoodGrid
{
	bool[] grid;
	public int size;
	float step;
	int pointsPerUnit;
	
	public FoodGrid(int _size, int _pointsPerUnit) {
		grid = new bool[_size * _size * _pointsPerUnit * _pointsPerUnit];
		size = _size;
		pointsPerUnit = _pointsPerUnit;
		step = 1f / _pointsPerUnit;
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
	
	public bool InBounds(Vector2Int point) {
		return point.x >= 0 && point.x < size * pointsPerUnit
			&& point.y >= 0 && point.y < size * pointsPerUnit;
	}
	
	public List<int> Around(Vector3 center, float radius) {
		var ori = new Vector3(center.x - radius, center.y, center.z - radius);
		var point = Snap(ori);
		
		var num = (int) radius * 2 * pointsPerUnit;
		var res = new List<int>();
		for (var i = 0; i < num; ++i) {
			for (var j = 0 ; j < num; ++j) {
				if (InBounds(point) && Vector3.Distance(Real(point, center.y), center) < radius) {
					res.Add(Index(point));
				}
				point.x++;
			}
			point.x -= num;
			point.y++;
		}
		return res;
	} 
}
