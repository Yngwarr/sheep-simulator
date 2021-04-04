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
	
	void Set(Vector2Int point, bool value) {
		grid[Index(point)] = value;
	}
	
	public bool InBounds(Vector2Int point) {
		return point.x >= 0 && point.x < size * pointsPerUnit
			&& point.y >= 0 && point.y < size * pointsPerUnit;
	}
	
	public bool IsFree(Vector3 point) {
		return IsFree(Snap(point));
	}
	
	bool IsFree(Vector2Int point) {
		return InBounds(point) && !grid[Index(point)];
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
	
	public Vector3 FreeInCircle(Vector3 center, float radius) {
		var ori = new Vector3(center.x - radius, center.y, center.z - radius);
		var point = Snap(ori);
		
		var num = (int) radius * 2 * pointsPerUnit;
		var res = new List<int>();
		for (var i = 0; i < num; ++i) {
			for (var j = 0 ; j < num; ++j) {
				var real = Real(point, center.y);
				if (IsFree(point) && Vector3.Distance(real, center) < radius) {
					return real;
				}
				point.x++;
			}
			point.x -= num;
			point.y++;
		}
		Debug.Log($"Couldn't find a free point around {center} in radius of {radius}. :(");
		return center;
	} 
	
	public Vector3 RandInCircle(Vector3 center, float radius) {
		for (var i = 0; i < 15; ++i) {
			var rand = Random.insideUnitCircle * radius;
			var point = center + new Vector3(rand.x, center.y, rand.y);
			if (IsFree(point)) {
				/* occupying the point is the caller's concern */
				return point;
			}
		}
		/* fallback in case we're feeling unlucky */
		Debug.Log("Fallback mode on.");
		return FreeInCircle(center, radius);
	}
}
