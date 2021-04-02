using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnhancedSlider : GameComponent
{
	public float defaultValue = 0;
	public float initialMinValue = 0;
	public float initialMaxValue = 100;
	public bool wholeNumbers = true;
	
	[NotNull] public Slider slider;
	[NotNull] public TMP_InputField input;
	public UnityEvent<float> onValueChanged;
	
	public float value {
		get => slider.value;
		/* doesn't invoke onValueChanged to avoid recursion */
		set {
			slider.value = value;
			/* slider will automatically cap its value to its bounds */
			input.text = slider.value.ToString(wholeNumbers ? "0" : "0.0");
		}
	}
	
	public float minValue {
		get => slider.minValue;
		set => slider.minValue = value;
	}
	
	public float maxValue {
		get => slider.maxValue;
		set => slider.maxValue = value;
	}
	
	void Start() {
		if (!slider || !input) return;
		slider.wholeNumbers = wholeNumbers;
		input.onEndEdit.AddListener(UpdateSlider);
		slider.onValueChanged.AddListener(UpdateInput);
		
		minValue = initialMinValue;
		maxValue = initialMaxValue;
		value = defaultValue;
	}
	
	void UpdateSlider(string value) {
		if (!float.TryParse(value, out var num)) {
			num = slider.minValue;
		}
		slider.value = num;
		onValueChanged.Invoke(num);
	}
	
	void UpdateInput(float value) {
		var num = value;
		input.text = num.ToString(wholeNumbers ? "0" : "0.0");
		onValueChanged.Invoke(num);
	}
}
