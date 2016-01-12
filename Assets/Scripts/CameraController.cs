using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public int xMin;
	public int xMax;
	public int yMin;
	public int yMax;
	public int zoom;

	private bool followX;
	private bool followY;

	GameObject Object1;
	GenerateMap Script1;

	// Use this for initialization
	void Start () {
		Object1 = GameObject.Find ("Generate Map");
		Script1 = Object1.GetComponent<GenerateMap> ();
	}

	void Update () {
		xMin = 5;
		xMax = Script1.dimension - xMin;
		yMin = -3;
		yMax = Script1.dimension * (-1) - yMin;
	}

	void LateUpdate () {

		followX = false;
		followY = false;

		if (Script1.players [0].transform.position.x > xMin && Script1.players [0].transform.position.x < xMax)
			followX = true;
		if (Script1.players [0].transform.position.y < yMin && Script1.players [0].transform.position.y > yMax)
			followY = true;

		if (followX == true)
			transform.position = new Vector3 (Script1.players [0].transform.position.x, transform.position.y, transform.position.z);
		if (followY == true)
			transform.position = new Vector3 (transform.position.x, Script1.players [0].transform.position.y, transform.position.z);

		if (transform.position.x < xMin)
			transform.position = new Vector3 (xMin, transform.position.y, transform.position.z);
		if (transform.position.z > yMin)
			transform.position = new Vector3 (transform.position.x, yMin, transform.position.z);
	}
}
