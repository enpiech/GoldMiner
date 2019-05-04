using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineController :MonoBehaviour
{
	private const int START_INDEX = 0;
	private const int END_INDEX = 1;

	private LineRenderer _lineRenderer;

	[SerializeField]
	private Transform _from;
	[SerializeField]
	private Transform _to;

	private void Awake()
	{
		// Init Component
		_lineRenderer = GetComponent<LineRenderer>();		
	}

	private void Update()
	{
		_lineRenderer.SetPosition(START_INDEX, _from.position);
		_lineRenderer.SetPosition(END_INDEX, _to.position);
	}
}
