using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimulationController : GameComponent
{
	[Header("UI")]
	[NotNull] public EnhancedSlider fieldSizeSlider;
	[NotNull] public EnhancedSlider populationSlider;
	[NotNull] public EnhancedSlider sheepSpeedSlider;
	[NotNull] public GameObject mainMenu;
	
	[Header("Controllers")]
	[NotNull] public Field field;
	[NotNull] public SheepController sheepCtrl;
	
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
