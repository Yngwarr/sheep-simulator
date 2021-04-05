using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using Random = UnityEngine.Random;

public class SheepController : GameComponent
{
	const float HEIGHT = .25f;

	[NotNull] public GameObject prefabSheep;
	[NotNull] public GameObject prefabFood;
	[NotNull] public FoodController foodCtrl;
	[NotNull] public Field field;

	public void Spawn(int amount, float speed, bool collideWhenInvisible) {
		for (int i = 0; i < amount; ++i) {
			Spawn(speed, collideWhenInvisible);
		}
	}
	
	void Spawn(float speed, bool collideWhenInvisible) {
		var x = Random.Range(field.bounds.min.x, field.bounds.max.x);
		var z = Random.Range(field.bounds.min.z, field.bounds.max.z);
		
		var sheepObj = Instantiate(prefabSheep, transform);
		var foodObj = Instantiate(prefabFood, foodCtrl.transform);
		
		var sheep = sheepObj.GetComponent<Sheep>();
		var food = foodObj.GetComponent<Food>();
		
		sheep.transform.position = new Vector3(x, HEIGHT, z);
		sheep.food = food;
		sheep.speed = speed;
		sheep.collideWhenInvisible = collideWhenInvisible;
		food.transform.position = Vector3.up * HEIGHT;
		foodCtrl.Respawn(food, sheep.transform.position, sheep.speed);
	}
}
