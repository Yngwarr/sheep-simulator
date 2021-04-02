using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationController : MonoBehaviour
{
	public EnhancedSlider fieldSizeSlider;
	public EnhancedSlider populationSlider;
	public EnhancedSlider sheepSpeedSlider;
	
	public int fieldSize {
		get => fieldSizeSlider.value;
		set => fieldSizeSlider.value = value;
	}
	public int population {
		get => populationSlider.value;
		set => populationSlider.value = value;
	}
	public int sheepSpeed {
		get => sheepSpeedSlider.value;
		set => sheepSpeedSlider.value = value;
	}

	void OnValidate() {
		if (!fieldSizeSlider) {
			Debug.LogError("Field Size Slider is not set", this);
		}
		if (!populationSlider) {
			Debug.LogError("Population Slider is not set", this);
		}
		if (!sheepSpeedSlider) {
			Debug.LogError("Sheep Speed Slider is not set", this);
		}
	}

	void Awake() {
		fieldSizeSlider.onValueChanged.AddListener(SetMaxPopulaiton);
	}
	
	void SetMaxPopulaiton(int n) {
		populationSlider.maxValue = Mathf.CeilToInt(n * n / 2);
	}
}
