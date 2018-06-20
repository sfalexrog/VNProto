using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{

	private GameState _gameState;
	
	void Awake()
	{
		_gameState = Toolbox.RegisterComponent<GameState>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
