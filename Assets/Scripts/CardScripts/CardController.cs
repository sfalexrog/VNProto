using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Relation
{
	FAMILY = 0,
	FRIENDS = 1,
	COUPLE = 2,
	CLASS = 3
}

public class CardController : MonoBehaviour
{

	private GameState _gameState;

	private ICardGenerator _generator;

	// Relation values
	private float[] _relations; 

	private const int MAX_RELATIONS = 10;
	
	void Awake()
	{
		_gameState = Toolbox.RegisterComponent<GameState>();
		
		_generator = ICardGenerator.FromScript(_gameState.CardScriptResource, (int)_gameState.PlayerGender);
		
		// TODO: Load relations from GameState

		_relations = new float[4];
		for (int i = 0; i < 4; ++i)
		{
			_relations[i] = MAX_RELATIONS / 2;
		}
	}

	private Card _currentCard;

	public Card CurrentCard	{
		get
		{
			return _currentCard;	
		}
	}

	public Card GetNextCard()
	{
		_currentCard = _generator.YieldCard();
		return _currentCard;
	}

	public int GetCardsRemaining()
	{
		return _generator.GetNumCardsRemaining();
	}

	public float GetCurrentRelations(Relation faction)
	{
		return _relations[(int) faction];
	}

	public float GetMaxRelations(Relation faction)
	{
		return MAX_RELATIONS;
	}

	public float[] GetRelationsChange(int answer)
	{
		var result = new float[4];
		Card.Outcome[] outcomes;
		if (answer == 0) // left answer
		{
			outcomes = _currentCard.LeftOutcomes;
		}
		else
		{
			outcomes = _currentCard.RightOutcomes;
		}

		foreach (var outcome in outcomes)
		{
			var idx = -1;
			switch (outcome.Faction)
			{
				case "Семья":
					idx = (int) Relation.FAMILY;
					break;
				case "Класс":
					idx = (int) Relation.CLASS;
					break;
				case "Друзья":
					idx = (int) Relation.FRIENDS;
					break;
				case "Пара":
					idx = (int) Relation.COUPLE;
					break;
				default:
					Debug.LogWarning("Unknown faction: " + outcome.Faction);
					break;
			}

			if (idx >= 0)
			{
				result[idx] = outcome.Change;
			}
		}

		return result;
	}

	public void ApplyChange(int answer)
	{
		var change = GetRelationsChange(answer);
		for (int i = 0; i < 4; ++i)
		{
			_relations[i] += change[i];
		}
	}

	/**
	 * Check if the game is over
	 */
	public bool IsGameOverState()
	{
		bool result = false;
		foreach(var relation in _relations)
		{
			if ((relation <= 0) || (relation >= MAX_RELATIONS))
			{
				result = true;
			}
		}

		return result;
	}
	
	/**
	 * Finish the game
	 */
	public void OnCardGameFinish()
	{
		
	}
}
