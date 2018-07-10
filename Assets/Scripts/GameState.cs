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

	// currentScene will be replaced by NextChapterId on hub load
	// if NextChapterId is not -1.
	public int NextChapterId;
	
	// ChapterResource should contain resource name for next chapter
	public string ChapterResource;

	public int CurrentCardGameId;
	
	// Next id for card game
	public int NextCardGameId;
	
	// CardScriptResource should contain resource name for next card game
	public string CardScriptResource;
	
	void Awake()
	{
		currentPower = 2;
		maxPower = 3;
		currentScene = 1;
		currentExperience = 0;

		PlayerName = "Игрок";
		PlayerAppearance = "";

		NextChapterId = -1;
		// By default, ChapterResource will point to the first scenario
		ChapterResource = "Scenarios/Ink/Chapter_01";

		CurrentCardGameId = 1;
		NextCardGameId = -1;
		// By default, CardScriptResource will point to the first card script
		//CardScriptResource = "Cards/Scripts/RandomCardGame";
	}
}
