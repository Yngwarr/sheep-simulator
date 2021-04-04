using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridView : GameComponent
{
	[NotNull] public GameObject pointObj;
	[NotNull] public Field field;
	
	[Header("Materials")]
	[NotNull] public Material still;
	[NotNull] public Material highlight;
	[NotNull] public Material aroundMat;
	
	FoodGrid grid;
	List<Renderer> points = new List<Renderer>();
	
	Sheep tracking;
	Renderer lastTracked;
	List<int> lastAround = new List<int>();
	
	public void Fill() {
		var size = (int) field.bounds.size.x;
		grid = new FoodGrid(size, 2);
		for (var i = 0; i < size * 2; ++i) {
			for (var j = 0; j < size * 2; ++j) {
				var point = Instantiate(pointObj, transform);
				points.Add(point.GetComponent<Renderer>());
				point.transform.position = grid.Real(new Vector2Int(i, j), .25f);
			}
		}
	}
	
	public void Track(Sheep sheep) {
		tracking = sheep;
	}

	void FixedUpdate() {
		if (!tracking) return;
		var pos = grid.Snap(tracking.transform.position);
		if (lastTracked) lastTracked.material = still;
		foreach (var i in lastAround) {
			points[i].material = still;
		}
		var point = points[grid.Index(pos)];
		var around = grid.Around(tracking.transform.position, 2);
		foreach (var i in around) {
			points[i].material = aroundMat;
		}
		point.material = highlight;
		lastTracked = point;
		lastAround = around;
	}
}
