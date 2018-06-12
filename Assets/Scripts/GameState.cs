using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{

	public int currentPower;
	public int currentScene;

	void Awake()
	{
		currentPower = 500;
		currentScene = 1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
