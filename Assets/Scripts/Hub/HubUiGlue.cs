﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HubUiGlue : MonoBehaviour
{

	public Text powerMeter;
	public Text levelText;

	private GameState gameState;
	

	// Use this for initialization
	void Start ()
	{
		gameState = Toolbox.RegisterComponent<GameState>();
		powerMeter.text = "Current power: " + gameState.currentPower;
		levelText.text = "Следующий уровень: " + gameState.currentScene;
	}

	public void StartNextLevel()
	{
		SceneManager.LoadScene(1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
