using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FoodController : GameComponent
{
	const float SPAWN_RANGE = 5f;
	const float MIN_RADIUS = 1f;
	
	[NotNull] public Field field;

	Vector2 Cartesian(Vector3 center, float radius, float angle) {
		return new Vector2(center.x + radius * Mathf.Cos(angle), center.z + radius * Mathf.Sin(angle));
	}

	public void Respawn(Food food, Vector3 sheepPos, float sheepSpeed) {
	    /* starts with one because we don't want to overlap the sheep */
	    /* TODO 1f -> sheep.scale/2 + food.scale/2, given the standard radius ~ 1 */
	    var center = sheepPos;
	    
	    var radius = Random.Range(MIN_RADIUS, sheepSpeed * SPAWN_RANGE);
	    var angle = Random.value * Mathf.PI * 2;
	    
		Vector2 point;
		while (true) {
			point = Cartesian(center, radius, angle);
			
			/* rotate food 90 degrees around the sheep when the point was generated outbounds */
			var i = 0;
			for (; !field.Contains(point) && i < 3; ++i) {
				angle += Mathf.PI / 2;
				point = Cartesian(center, radius, angle);
			}
			if (i < 3) break;
			/* in rare case when the field is too small for the sheep, shrink radius and try again */
			radius = Mathf.Max(radius / 2, MIN_RADIUS);
		}

		food.transform.position = new Vector3(point.x, food.transform.position.y, point.y);
	}
}
