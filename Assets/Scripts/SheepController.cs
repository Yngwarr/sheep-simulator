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

	public void SpawnBunch(int amount, float speed) {
		for (var i = 0; i < amount; ++i) {
			var sheep = Spawn(speed);
			foodCtrl.Respawn(sheep.food, sheep.transform.position, sheep.speed);
		}
	}
	
	Sheep Spawn(float speed) {
		return SpawnAt(
			Random.Range(field.bounds.min.x, field.bounds.max.x),
			Random.Range(field.bounds.min.z, field.bounds.max.z),
			speed
		);
	}
	
	public void SpawnAt(float x, float z, float foodX, float foodZ, float speed) {
		var sheep = SpawnAt(x, z, speed);
		foodCtrl.Spawn(foodX, foodZ, sheep.food);
	}
	
	Sheep SpawnAt(float x, float z, float speed) {
		var sheepObj = Instantiate(prefabSheep, transform);
		var foodObj = Instantiate(prefabFood, foodCtrl.transform);
		
		var sheep = sheepObj.GetComponent<Sheep>();
		var food = foodObj.GetComponent<Food>();
		
		sheep.transform.position = new Vector3(x, HEIGHT, z);
		sheep.food = food;
		sheep.speed = speed;
		food.transform.position = Vector3.up * HEIGHT;
		return sheep;
	}
}
