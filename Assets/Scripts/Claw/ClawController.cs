using System.Collections;
using UnityEngine;

public class ClawController : MonoBehaviour
{
	private Rigidbody2D _rigidbody;
	private Transform _transform;

	private Vector3 _shotPosition;

	private bool _resetAnim = true;

	private ClawState _state;

	[SerializeField]
	private float _shotSpeed = GameManager.MIN_CLAW_SHOT_SPEED;
	[SerializeField]
	private float _retractSpeed = 20;

	private float _currentSpeed = 0;

	public bool IsReady()
	{
		return _state == ClawState.STATE_STANDING || _state == ClawState.STATE_MOVING;
	}

	public void SetShotSpeed(float speed)
	{
		_shotSpeed = speed;
	}

	// Start is called before the first frame update
	void Start()
	{
		Init();
	}

	protected void Init()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
		_transform = transform;

		_shotPosition = _transform.position;
		_state = ClawState.STATE_STANDING;
	}

	protected void Update()
	{
		if (_state == ClawState.STATE_RETRACTING)
		{
			StartCoroutine(RetractDelay());
		}

		print(_state);
	}

	protected void HandleInput()
	{
		if (Input.touchCount != 1)
		{
			return;
		}

		Touch touch = Input.GetTouch(0);

		switch (_state)
		{
			case ClawState.STATE_RETRACTING:
				PauseRetract();
				break;
			case ClawState.STATE_PAUSING:
				if (touch.tapCount == 2)
				{
					print("as");
					Retract();
				}
				break;
			case ClawState.STATE_SHOTTING:
				if (touch.tapCount == 2)
				{
					Retract();
				}
				else
				{
					PauseRetract();
				}
				break;
			case ClawState.STATE_STANDING:
				break;
			default:
				break;
		}
	}

	public void Shot()
	{
		print("asdfa");
		_state = ClawState.STATE_SHOTTING;
		_shotPosition = _transform.position;
		_rigidbody.AddForce(_transform.up * -1 * _shotSpeed, ForceMode2D.Impulse);
	}

	public void PauseRetract()
	{
		_state = ClawState.STATE_PAUSING;
		_rigidbody.velocity = Vector2.zero;
	}

	public void Retract()
	{
		_state = ClawState.STATE_RETRACTING;
	}

	public void RotateWithGun()
	{
		_state = ClawState.STATE_MOVING;
		_transform.localPosition = new Vector3(0, -5, 0);
		_transform.localRotation = Quaternion.identity;
	}

	protected IEnumerator RetractDelay()
	{
		_resetAnim = false;
		_rigidbody.velocity = _transform.up * _retractSpeed * Time.deltaTime;

		yield return new WaitForSecondsRealtime(1f);

		if ((_shotPosition - _transform.position).sqrMagnitude < 1f)
		{
			_state = ClawState.STATE_STANDING;
			_rigidbody.position = _shotPosition;
			_rigidbody.velocity = Vector2.zero;
		}
	}
}
