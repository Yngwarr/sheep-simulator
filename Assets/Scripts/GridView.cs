using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridView : GameComponent
{
	[NotNull] public GameObject pointObj;
	[NotNull] public Field field;
	
	FoodGrid grid;
	
	public void Fill() {
		var size = (int) field.bounds.size.x;
		grid = new FoodGrid(size, 2);
		for (var i = 0; i < size * 2; ++i) {
			for (var j = 0; j < size * 2; ++j) {
				var point = Instantiate(pointObj, transform);
				point.transform.position = grid.Real(new Vector2Int(i, j), .25f);
			}
		}
	}
}
