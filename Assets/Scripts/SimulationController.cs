using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class SimulationController : GameComponent
{
	const string SAVE_FILE = "save.txt";

	[Header("UI")]
	[NotNull] public EnhancedSlider fieldSizeSlider;
	[NotNull] public EnhancedSlider populationSlider;
	[NotNull] public EnhancedSlider sheepSpeedSlider;
	[NotNull] public GameObject mainMenu;
	[NotNull] public GameObject HUD;
	[NotNull] public Popup popup;
	[NotNull] public EnhancedSlider simSpeedSlider;
	[NotNull] public Button startButton;
	[NotNull] public Button loadButton;
	[NotNull] public Button saveButton;

	[Header("Controllers")]
	[NotNull] public Field field;
	[NotNull] public SheepController sheepCtrl;
	[NotNull] public FoodController foodCtrl;
	[NotNull] public ParticlePool particlePool;

	[Header("Misc")]
	[NotNull] public MovingCamera mainCamera;

	float defaultDeltaTime;
	float lastTime;

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
		loadButton.interactable = File.Exists(SAVE_FILE);
	}

	void Update() {
		var speedInput = Input.GetAxisRaw("SimulationSpeed");

		if (!Mathf.Approximately(speedInput, 0f)) {
			var delta = Input.GetButton("AltSpeed") ? .5f : .1f;
			AddSimulationSpeed(speedInput * delta);
		}

		if (Input.GetButtonDown("Pause")) {
			SetSimulationSpeed(Time.timeScale > 0f ? 0f : 1f);
		}
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
		simSpeedSlider.value = Time.timeScale;
	}

	void AddSimulationSpeed(float delta) {
		var scale = Time.timeScale + delta;
		SetSimulationSpeed(scale <= 0f ? 0f : scale);
	}

	public void StartSimulation(bool load) {
		loadButton.interactable = false;
		startButton.interactable = false;

		var sheeps = new List<SheepInfo>();
		if (load) {
			if (!TryLoad(out var err, out sheeps)) {
				popup.Show(err);
				startButton.interactable = true;
				return;
			}
		}

		field.size = (int) fieldSize;
		foodCtrl.Init(field);

		if (load) {
			foreach (var s in sheeps) {
				sheepCtrl.SpawnAt(s.x, s.z, s.foodX, s.foodZ, sheepSpeed);
			}
		} else {
			sheepCtrl.SpawnBunch((int) population, sheepSpeed);
		}

		particlePool.Init((int) population);
		mainMenu.SetActive(false);
		HUD.SetActive(true);
		mainCamera.on = true;
		mainCamera.Init(field.bounds);
		SetSimulationSpeed(1f);
	}

	public void PauseSave() {
		saveButton.interactable = false;
		SetSimulationSpeed(0f);
		Save();
		saveButton.interactable = true;
	}

	void Save() {
		/* not writing right away to reduce the chance of file corruption */
		var sb = new StringBuilder();
		sb.Append($"{fieldSize} {population} {sheepSpeed}\n");
		var sheeps = sheepCtrl.GetComponentsInChildren<Sheep>();
		foreach (var sheep in sheeps) {
			var sheepPos = sheep.transform.position;
			var foodPos = sheep.food.transform.position;
			sb.Append($"{sheepPos.x} {sheepPos.z} {foodPos.x} {foodPos.z}\n");
		}

		using StreamWriter sw = File.CreateText(SAVE_FILE);
		sw.Write(sb.ToString());
		popup.Show("Done!");
	}

	bool TryLoad(out string err, out List<SheepInfo> sheeps) {
		sheeps = new List<SheepInfo>();

		using StreamReader sr = File.OpenText(SAVE_FILE);
		var s = sr.ReadLine();
		if (s == null) {
			err = "Can't read from the file.";
			return false;
		}

		var subs = s.Split(' ');
		if (subs.Length != 3) {
			err = "Invalid header.";
			return false;
		}

		if (!ParseFloats(subs, out var vals)) {
			err = "Invalid header.";
			return false;
		}

		fieldSize = vals[0];
		population = vals[1];
		sheepSpeed = vals[2];

		while ((s = sr.ReadLine()) != null) {
			subs = s.Split(' ');
			if (subs.Length != 4) continue;

			if (!ParseFloats(subs, out vals)) continue;
			sheeps.Add(new SheepInfo(vals[0], vals[1], vals[2], vals[3]));
		}

		err = "done";
		return true;
	}

	bool ParseFloats(string[] ss, out float[] fs) {
		fs = new float[ss.Length];
		for (var i = 0; i < ss.Length; ++i) {
			if (!float.TryParse(ss[i], out var val)) {
				return false;
			}
			fs[i] = val;
		}
		return true;
	}
}
