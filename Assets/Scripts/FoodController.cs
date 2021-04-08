using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FoodController : GameComponent
{
	const float SPAWN_RANGE = 5f;
	const float MIN_RADIUS = 1f;
	
	public Field field;
	FoodGrid grid;

	public void Init(Field f) {
		field = f;
		grid = new FoodGrid(field.size, 2);
	}
	
	Vector2 Cartesian(Vector3 center, float radius, float angle) {
		return new Vector2(center.x + radius * Mathf.Cos(angle), center.z + radius * Mathf.Sin(angle));
	}

	public void Respawn(Food food, Vector3 sheepPos, float sheepSpeed) {
	    var center = new Vector3(sheepPos.x, food.transform.position.y, sheepPos.z);
	    
		var radius = Mathf.Min(sheepSpeed * SPAWN_RANGE, field.bounds.size.x);
	    var point = grid.RandInCircle(center, radius);
	    grid.Set(food.transform.position, false);
	    grid.Set(point, true);

		food.transform.position = point;
	}
}
