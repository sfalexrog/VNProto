using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{

	public int currentPower;
	public int currentScene;
	public int maxPower;

	void Awake()
	{
		currentPower = 75;
		maxPower = 150;
		currentScene = 1;
	}
}
