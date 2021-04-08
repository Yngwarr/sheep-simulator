using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SheepInfo
{
	public float x;
	public float z;
	public float foodX;
	public float foodZ;
	
	public SheepInfo(float _x, float _z, float _foodX, float _foodZ) {
		x = _x;
		z = _z;
		foodX = _foodX;
		foodZ = _foodZ;
	}
}
