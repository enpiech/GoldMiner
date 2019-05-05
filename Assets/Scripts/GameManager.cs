using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public const int SCREEN_WIDTH = 12;

	public const float MIN_CLAW_SHOT_SPEED = 0;
	public const float MAX_CLAW_SHOT_SPEED = 20;

	public const float ROTATION_ANGLE = 1;

	public static float MinX;
	public static float MinY;

	public static float MaxX;
	public static float MaxY;

	public static float ScreenRate;

	public GameObject[] ClawPrefabs;

	[SerializeField]
	private Claw _claw;

	public Claw Claw
	{
		get => _claw;
	}

	// Start is called before the first frame update
	void Start()
	{
		ScreenRate = Screen.width / Screen.height;

		CalculateMinMaxPosition();
	}

	// Update is called once per frame
	void Update()
    {
        
    }

	private void CalculateMinMaxPosition()
	{
		var verticalExtend = Camera.main.orthographicSize;
		var horizontalExtend = verticalExtend * ScreenRate;

		MinX = horizontalExtend - SCREEN_WIDTH / 2;
		MinY = verticalExtend - SCREEN_WIDTH * ScreenRate / 2;

		MaxX = SCREEN_WIDTH / 2 - horizontalExtend;
		MaxY = SCREEN_WIDTH * ScreenRate / 2 - verticalExtend;
	}

	public static bool IsOutside(Vector2 position)
	{
		if (position.x >= MinX
			&& position.x <= MaxX
			&& position.y <= MinY
			&& position.y >= MaxY)
		{
			return false;
		}

		return true;
	}

	public static Vector3 GetTouchPosition(Vector3 position)
	{
		Vector2 pos = Camera.main.ScreenToWorldPoint(position);

		return new Vector3(pos.x, pos.y, 0.0f);
	}
}
