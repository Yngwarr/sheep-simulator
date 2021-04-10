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
	
	public void Set(Vector3 point, bool value) {
		grid[Index(Snap(point))] = value;
	}
	
	public bool InBounds(Vector2Int point) {
		return point.x >= 0 && point.x < size * pointsPerUnit
			&& point.y >= 0 && point.y < size * pointsPerUnit;
	}
	
	bool IsFree(Vector2Int point) {
		return InBounds(point) && !grid[Index(point)];
	}
	
	/* check whether the a is in Moore's neighborhood of b */
	bool IsNeighbor(Vector2Int a, Vector2Int b) {
		return Mathf.Abs(a.x - b.x) <= 1 && Mathf.Abs(a.y - b.y) <= 1;
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
		var centerSnapped = Snap(center);
		for (var i = 0; i < num; ++i) {
			for (var j = 0 ; j < num; ++j) {
				var real = Real(point, center.y);
				if (IsFree(point)
					&& Vector3.Distance(real, center) < radius
					&& !IsNeighbor(point, centerSnapped)) {
					return real;
				}
				point.x++;
			}
			point.x -= num;
			point.y++;
		}
		/* couldn't find any free place around the point */
		return center;
	} 
	
	public Vector3 RandInCircle(Vector3 center, float radius) {
		var centerSnapped = Snap(center);
		for (var i = 0; i < 15; ++i) {
			var rand = Random.insideUnitCircle * radius;
			var point = Snap(center + new Vector3(rand.x, center.y, rand.y));
			if (IsFree(point) && !IsNeighbor(point, centerSnapped)) {
				/* occupying the point is the caller's concern */
				return Real(point, center.y);
			}
		}
		/* fallback in case we're feeling unlucky */
		return FreeInCircle(center, radius);
	}
}
