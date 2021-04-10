using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Popup : GameComponent
{
	float lastTime;
	float timer;
	[NotNull] public TMP_Text text;
	
    void OnEnable() {
		lastTime = Time.unscaledTime;
    }

    void Update() {
		var dt = Time.unscaledTime - lastTime;
		lastTime = Time.unscaledTime;
		
		if (timer > 0f) {
			timer -= dt;
		} else {
			gameObject.SetActive(false);
		}
    }
    
    public void Show(string msg, float t = 3f) {
	    text.text = msg;
	    timer = t;
	    gameObject.SetActive(true);
    }
}
