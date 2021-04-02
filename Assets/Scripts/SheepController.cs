using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using Random = UnityEngine.Random;

public class SheepController : MonoBehaviour
{
	const float HEIGHT = .25f;

	public GameObject prefabSheep;
	public GameObject prefabFood;
	public FoodController foodCtrl;
	public Field field;

	void OnValidate() {
		if (!prefabSheep) {
			Debug.LogError("Prefab Sheep is not set.", this);
		}
		if (!prefabFood) {
			Debug.LogError("Prefab Food is not set.", this);
		}
		if (!foodCtrl) {
			Debug.LogError("Food Controller is not set.", this);
		}
		if (!field) {
			Debug.LogError("Field is not set.", this);
		}
	}
	
	public void Spawn(int amount, float speed) {
		for (int i = 0; i < amount; ++i) {
			Spawn(speed);
		}
	}
	
	void Spawn(float speed) {
		var x = Random.Range(field.bounds.min.x, field.bounds.max.x);
		var z = Random.Range(field.bounds.min.z, field.bounds.max.z);
		
		var sheepObj = Instantiate(prefabSheep, transform);
		var foodObj = Instantiate(prefabFood, foodCtrl.transform);
		
		var sheep = sheepObj.GetComponent<Sheep>();
		var food = foodObj.GetComponent<Food>();
		
		sheep.transform.position = new Vector3(x, HEIGHT, z);
		sheep.food = foodObj;
		sheep.speed = speed;
		food.transform.position = Vector3.up * HEIGHT;
		foodCtrl.Respawn(food, sheep);
	}
}
