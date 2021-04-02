using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimulationController : MonoBehaviour
{
	[Header("UI")]
	public EnhancedSlider fieldSizeSlider;
	public EnhancedSlider populationSlider;
	public EnhancedSlider sheepSpeedSlider;
	public GameObject mainMenu;
	
	[Header("Controllers")]
	public Field field;
	public SheepController sheepCtrl;
	
	float defaultDeltaTime;
	
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
		if (!mainMenu) {
			Debug.LogError("Main Menu is not set", this);
		}
	}

	void Awake() {
		defaultDeltaTime = Time.fixedDeltaTime;
		fieldSizeSlider.onValueChanged.AddListener(SetMaxPopulation);
		SetSimulationSpeed(0f);
	}
	
	void SetMaxPopulation(int n) {
		populationSlider.maxValue = Mathf.CeilToInt(n * n / 2f);
	}
	
	public void SetSimulationSpeed(float multiplier) {
		Time.timeScale = multiplier;
		Time.fixedDeltaTime = defaultDeltaTime * multiplier;
	}
	
	public void StartSimulation() {
		field.size = fieldSize;
		sheepCtrl.Spawn(population, sheepSpeed);
		mainMenu.SetActive(false);
		SetSimulationSpeed(1f);
	}
}
