using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnhancedSlider : MonoBehaviour
{
	public Slider slider;
	public TMP_InputField input;
	public UnityEvent<int> onValueChanged;
	
	void Start() {
		if (!slider || !input) return;
		input.onEndEdit.AddListener(UpdateSlider);
		slider.onValueChanged.AddListener(UpdateInput);
	}
	
	void OnValidate() {
		if (!slider) {
			Debug.LogError("Slider is not set.", this);
		}
		if (!input) {
			Debug.LogError("Input is not set.", this);
		}
	}
	
	void UpdateSlider(string value) {
		if (!int.TryParse(value, out var num)) {
			num = (int) slider.minValue;
		}
		slider.value = num;
		onValueChanged.Invoke(num);
	}
	
	void UpdateInput(float value) {
		var num = (int) value;
		input.text = num.ToString();
		onValueChanged.Invoke(num);
	}
}
