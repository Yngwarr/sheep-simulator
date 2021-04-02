using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;

public class GameComponent : MonoBehaviour
{
	protected virtual void OnValidate() {
		foreach (var fieldInfo in GetType().GetFields(BindingFlags.Public | BindingFlags.Instance)) {
			foreach (var attr in fieldInfo.GetCustomAttributes(true)) {
				if (!(attr is NotNullAttribute)) continue;
				
				var value = fieldInfo.GetValue(this);
				
				if (value is Object obj ? !obj : value == null) {
					Debug.LogError($"{fieldInfo.Name} is not set.", this);
				}
			}
		}
	}
}
