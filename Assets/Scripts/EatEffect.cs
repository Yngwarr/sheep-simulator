using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatEffect : GameComponent
{
	public ParticlePool pool;
	ParticleSystem system;
	
	void Start() {
		system = GetComponent<ParticleSystem>();
	}

	void OnParticleSystemStopped() {
		if (!pool) return;
		pool.Free(system);
	}
}
