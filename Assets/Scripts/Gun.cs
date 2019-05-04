using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
	[SerializeField]
	private float _rotationSpeed = 200f;

	[Range(GameManager.MIN_CLAW_SHOT_SPEED, GameManager.MAX_CLAW_SHOT_SPEED)]
	[SerializeField]
	protected float _minShotSpeed = GameManager.MIN_CLAW_SHOT_SPEED;
	[Range(GameManager.MIN_CLAW_SHOT_SPEED, GameManager.MAX_CLAW_SHOT_SPEED)]
	[SerializeField]
	protected float _maxShotSpeed = GameManager.MAX_CLAW_SHOT_SPEED;
	[Range(GameManager.MIN_CLAW_SHOT_SPEED, GameManager.MAX_CLAW_SHOT_SPEED)]
	[SerializeField]
	protected float _shotSpeed = 1;

	private LineController _line;
	private ClawController _claw;
	private Transform _transform;
	private Rigidbody2D _rigidbody;

	private bool _isShotted = false;

	private Vector3 _originPosition;
	private Vector3 _targetPosition;

	public void SetShotSpeed(float speed)
	{
		if (speed < _minShotSpeed)
			speed = _minShotSpeed;
		else if (speed > _maxShotSpeed)
			speed = _maxShotSpeed;

		_shotSpeed = speed;
	}

	public void CalculateShotPower(Vector2 targetPosition)
	{
		float str = Mathf.Sqrt((targetPosition - (Vector2)_transform.position).sqrMagnitude);

		SetShotSpeed(str);
	}

	void Awake()
	{
		// Init component
		_transform = transform;
		_rigidbody = GetComponent<Rigidbody2D>();
		_originPosition = _transform.position;

		// Init value
		_isShotted = false;

		_claw = GetComponentInChildren<ClawController>();
		_line = GetComponentInChildren<LineController>();
	}

	void Update()
	{
		//RotateAroundOrigin();
	}

	public void Stop()
	{
		if (!CanRetract())
		{
			Debug.Log("The claw is not shotted!");
			return;
		}
		_claw.PauseRetract();
	}

	public void Retract()
	{
		if (!CanRetract())
		{
			Debug.Log("The claw is not shotted!");
			return;
		}

		_claw.Retract();
	}

	protected bool CanRetract()
	{
		if (!_isShotted)
		{
			return false;
		}
		return true;
	}

	public void Shot()
	{
		if (!CanShot())
		{
			Debug.Log("The claw has to be collected first!");
			return;
		}

		_isShotted = true;

		StartCoroutine(DelayShot());
	}

	protected bool CanShot()
	{
		if (_isShotted)
		{
			return false;
		}
		return true;
	}

	private IEnumerator DelayShot()
	{
		yield return new WaitForSeconds(0.3f);

		_claw.SetShotSpeed(_shotSpeed);
		_claw.Shot();

		_isShotted = true;
	}

	public void Move(Vector3 position)
	{
		if (!CanRotate())
		{
			return;
		}

		if (position.y > _originPosition.y)
		{
			position.y = _originPosition.y;
		}

		_targetPosition = position;

		var angle = Mathf.Atan2((_targetPosition - _originPosition).x, (_targetPosition - _originPosition).y) * Mathf.Rad2Deg;
		_rigidbody.SetRotation(180 - angle);

		_claw.RotateWithGun();
	}

	// DONT NEED
	private void RotateAroundOrigin()
	{
		if (!CanRotate())
			return;

		var angle = Mathf.Atan2((_targetPosition - _originPosition).x, (_targetPosition - _originPosition).y) * Mathf.Rad2Deg;
		_transform.rotation = Quaternion.AngleAxis(180 - angle, Vector3.forward);
	}

	private bool CanRotate()
	{
		if (!_claw.IsReady())
		{
			return false;
		}
		else
		{
			_isShotted = false;
		}

		if (_isShotted)
		{
			return false;
		}

		return true;
	}
}
