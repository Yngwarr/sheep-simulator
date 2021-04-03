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
	[NotNull] public GameObject HUD;
	
	[Header("Controllers")]
	[NotNull] public Field field;
	[NotNull] public SheepController sheepCtrl;
	[NotNull] public ParticlePool particlePool;
	
	float defaultDeltaTime;
	
	public float fieldSize {
		get => fieldSizeSlider.value;
		set => fieldSizeSlider.value = value;
	}
	public float population {
		get => populationSlider.value;
		set => populationSlider.value = value;
	}
	public float sheepSpeed {
		get => sheepSpeedSlider.value;
		set => sheepSpeedSlider.value = value;
	}

	void Awake() {
		defaultDeltaTime = Time.fixedDeltaTime;
		fieldSizeSlider.onValueChanged.AddListener(SetMaxPopulation);
		SetSimulationSpeed(0f);
	}
	
	void SetMaxPopulation(float n) {
		populationSlider.maxValue = Mathf.Ceil(n * n / 2f);
	}
	
	public void SetSimulationSpeed(float multiplier) {
#if UNITY_EDITOR
		if (multiplier > 100f) multiplier = 100f;
#endif
		/* effects will be too fast to see anyway, but they still affect performance */
		particlePool.on = multiplier <= 60f;
		Time.timeScale = multiplier;
		Time.fixedDeltaTime = defaultDeltaTime * multiplier;
	}
	
	public void StartSimulation() {
		field.size = (int) fieldSize;
		sheepCtrl.Spawn((int) population, sheepSpeed);
		mainMenu.SetActive(false);
		HUD.SetActive(true);
		SetSimulationSpeed(1f);
	}
}
