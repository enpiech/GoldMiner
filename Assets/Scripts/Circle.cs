using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class Circle :MonoBehaviour
{
	[Range(0, 50)]
	public int segments = 50;
	[Range(0, 50)]
	public float xradius = 5;
	[Range(0, 50)]
	public float yradius = 5;
	[Range(0, 360)]
	public float angle;
	LineRenderer line;

	void Start()
	{
		line = gameObject.GetComponent<LineRenderer>();

		line.positionCount = segments + 1;
		line.useWorldSpace = false;
		CreatePoints();
	}

	void CreatePoints()
	{
		float x;
		float y;

		angle = 110f;

		for (int i = 0; i < (segments + 1); i++)
		{
			x = Mathf.Sin(Mathf.Deg2Rad * angle) * xradius;

			y = Mathf.Cos(Mathf.Deg2Rad * angle) * yradius;

			line.SetPosition(i, new Vector3(x, y, 0));

			angle += (140f / segments);
		}
	}
}