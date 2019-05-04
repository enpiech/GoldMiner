using UnityEngine;
using UnityEngine.EventSystems;

public class ClickDetector : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
	private Gun _gunController;
	private Vector2 _direction;

	private Transform _transform;

	private void Start()
	{
		_transform = transform;
		_gunController = GetComponentInChildren<Gun>();
		AddPhysics2DRaycaster();
	}

	private void AddPhysics2DRaycaster()
	{
		Physics2DRaycaster physics2DRaycaster = GameObject.FindObjectOfType<Physics2DRaycaster>();
		if (physics2DRaycaster == null)
		{
			Camera.main.gameObject.AddComponent<Physics2DRaycaster>();
		}
	}

	#region drag
	void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
	{
	}

	void IDragHandler.OnDrag(PointerEventData eventData)
	{
		Vector3 position = GameManager.GetTouchPosition(eventData.position);
		_gunController.Move(position);
	}

	void IEndDragHandler.OnEndDrag(PointerEventData eventData)
	{
		Vector2 touchPos = GameManager.GetTouchPosition(eventData.position);
		if (Vector2.Distance(touchPos, _gunController.gameObject.transform.position) > 5)
		{
			_gunController.CalculateShotPower(targetPosition: touchPos);
			_gunController.Shot();
		}		
	}
	#endregion
}
