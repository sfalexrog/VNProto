using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerGender
{
	Boy = 0,
	Girl = 1
}

public class GameState : MonoBehaviour
{

	public int currentPower;
	public int currentScene;
	public int maxPower;

	public int currentExperience;

	public string PlayerName;
	public string PlayerAppearance;

	public PlayerGender PlayerGender = PlayerGender.Boy;
	
	void Awake()
	{
		currentPower = 0;
		maxPower = 150;
		currentScene = 1;
		currentExperience = 0;

		PlayerName = "Player";
		PlayerAppearance = "";
	}
}
