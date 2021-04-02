using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ParticlePool : GameComponent
{
	[NotNull] public ParticleSystem particleObj;
	
	List<ParticleSystem> free = new List<ParticleSystem>();
	int size = 0;
	bool on = true;
	
	void Start() {
		EventManager.Get().foodEaten.AddListener(Spawn);
		Init(5);
	}
	
	void Init(int amount) {
		for (int i = 0; i < amount; ++i) {
			free.Add(New());
		}
		size += amount;
	}
	
	ParticleSystem New() {
		var particle = Instantiate(particleObj, transform);
		particle.GetComponent<EatEffect>().pool = this;
		return particle;
	}
	
	void Spawn(Vector3 position) {
		if (!on) return;
		ParticleSystem particle;
		if (free.Count > 0) {
			particle = free.Last();
			free.RemoveAt(free.Count - 1);
		} else {
			particle = New();
			++size;
			Debug.Log($"Pool size increased to {size}.");
		}
		particle.transform.position = position;
		particle.Play();
	}
	
	public void Free(ParticleSystem particle) {
		free.Add(particle);
	}
}
