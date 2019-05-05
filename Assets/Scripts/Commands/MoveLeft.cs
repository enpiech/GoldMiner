using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
	private Claw _claw;

	public MoveLeft()
	{
		GameManager _gameManager = new GameManager();
		_claw = _gameManager.Claw;
	}
}
