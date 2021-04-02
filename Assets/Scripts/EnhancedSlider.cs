using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnhancedSlider : GameComponent
{
	public int defaultValue = 0;
	public int initialMinValue = 0;
	public int initialMaxValue = 100;
	
	[NotNull] public Slider slider;
	[NotNull] public TMP_InputField input;
	public UnityEvent<int> onValueChanged;
	
	public int value {
		get => (int) slider.value;
		/* doesn't invoke onValueChanged to avoid recursion */
		set {
			slider.value = value;
			/* slider will automatically cap its value to its bounds */
			input.text = ((int) slider.value).ToString();
		}
	}
	
	public int minValue {
		get => (int) slider.minValue;
		set => slider.minValue = value;
	}
	
	public int maxValue {
		get => (int) slider.maxValue;
		set => slider.maxValue = value;
	}
	
	void Start() {
		if (!slider || !input) return;
		input.onEndEdit.AddListener(UpdateSlider);
		slider.onValueChanged.AddListener(UpdateInput);
		
		minValue = initialMinValue;
		maxValue = initialMaxValue;
		value = defaultValue;
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
