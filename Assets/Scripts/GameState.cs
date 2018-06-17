using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{

	public int currentPower;
	public int currentScene;
	public int maxPower;

	public int currentExperience;

	public string PlayerName;
	public string PlayerAppearance;
	
	void Awake()
	{
		currentPower = 75;
		maxPower = 150;
		currentScene = 1;
		currentExperience = 0;

		PlayerName = "Player";
		PlayerAppearance = "";
	}
}
