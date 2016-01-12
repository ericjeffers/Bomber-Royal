using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerBehavior : MonoBehaviour {

	public int x;
	public int y;

	public float speed;
	public bool moving = false;
	private bool canMove;
	private bool canBomb;

	public List<GameObject> bombs = new List<GameObject> ();

	public GameObject bomb;
	public WallBehavior WallBehavior;
	private GameObject wall;
	private GameObject block;

	public GameObject Map;
	public GenerateMap GenerateMapScript;

	public int bombCount;
	public int explodeRange;

	/*
	private float lastX = 0f;
	private float lastY = 0f;
	private float xDiff;
	private float yDiff;
	private bool goUp = false;
	private bool goDown = false;
	private bool goLeft = false;
	private bool goRight = false;

	public CNAbstractController MovementJoystick;
*/
	
	IEnumerator MoveFromTo(Vector3 pointA, Vector3 pointB, float time, int moveX, int moveY) {
		if (!moving) { // do nothing if already moving
			moving = true; // signals "I'm moving, don't bother me!"
			bool hasMoved = false; //used for changing x/y coordinates halfway through moving
			float t = 0f;
			while (t < 1f) {
				t += Time.deltaTime / time; // sweeps from 0 to 1 in time seconds
				transform.position = Vector3.Lerp(pointA, pointB, t); // set position proportional to t
				yield return 0; // leave the routine and return here in the next frame
				if ( t > 0.5 && hasMoved == false) { //dynamically keep track of position mid-movement
					x = x + moveX;
					y = y + moveY;
					hasMoved = true;
				}
			}
			//Debug.Log(x + ", " + y);
			moving = false; // finished moving
		}
	}

	// Use this for initialization
	void Start () {
		Map = GameObject.Find ("Generate Map");
		GenerateMapScript = Map.GetComponent<GenerateMap> ();

		bombCount = 3;
		explodeRange = 5;
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (CrossPlatformInputManager.GetAxisRaw("HorizontalControl"));
		//Debug.Log (MovementJoystick.GetAxis ("Vertical"));

		//map = GenerateMapScript.fullMap;
		/*
		xDiff = Mathf.Abs(CrossPlatformInputManager.GetAxisRaw ("HorizontalControl")) - Mathf.Abs (lastX);
		yDiff = Mathf.Abs(CrossPlatformInputManager.GetAxisRaw ("VerticalControl")) - Mathf.Abs (lastY);

		if (Mathf.Abs (xDiff) > Mathf.Abs (yDiff)) {
			if (CrossPlatformInputManager.GetAxisRaw ("HorizontalControl") > 0) {
				goRight = true;
				goLeft = false;
				goUp = false;
				goDown = false;
			}
			if (CrossPlatformInputManager.GetAxisRaw ("HorizontalControl") < 0) {
				goRight = false;
				goLeft = true;
				goUp = false;
				goDown = false;
			}
		}

		if (Mathf.Abs (xDiff) < Mathf.Abs (yDiff)) {
			if (CrossPlatformInputManager.GetAxisRaw ("VerticalControl") > 0) {
				goRight = false;
				goLeft = false;
				goUp = true;
				goDown = false;
			}
			if (CrossPlatformInputManager.GetAxisRaw ("VerticalControl") < 0) {
				goRight = false;
				goLeft = false;
				goUp = false;
				goDown = true;
			}
		}

		if (CrossPlatformInputManager.GetAxisRaw ("HorizontalControl") == 0 && CrossPlatformInputManager.GetAxisRaw ("VerticalControl") == 0) {
			goRight = false;
			goLeft = false;
			goUp = false;
			goDown = false;
		}

		//Debug.Log ("Up: " + goUp + "   " + "Down: " + goDown + "   " + "Left: " + goLeft + "   " + "Right: " + goRight);	*/
	}

	void LateUpdate () {

		if (Input.GetKey ("k")) {
			for (int i = GenerateMapScript.walls.Count - 1; i >= 0; i--) {
				//WallBehavior = gameObject.GetComponent<WallBehavior>();
				wall = GenerateMapScript.walls[i];
				Debug.Log (wall.transform.position.x + ", " + wall.transform.position.y);
				Debug.Log ((wall.GetComponent("WallBehavior") as WallBehavior).x);
			}
		}

		if (Input.GetKey ("space")) {
			canBomb = true;
			for (int i = 0; i < bombs.Count; i++) {
				if ((bombs[i].GetComponent("BombTimer") as BombTimer).x == x && (bombs[i].GetComponent("BombTimer") as BombTimer).y == y)
					canBomb = false;
			}
			if (bombCount > 0 && canBomb == true) {
				bombCount = bombCount - 1;
				GameObject bombObject = Instantiate(bomb, new Vector3(x, y, 0), Quaternion.identity) as GameObject;
				(bombObject.GetComponent("BombTimer") as BombTimer).x = x;
				(bombObject.GetComponent("BombTimer") as BombTimer).y = y;
				(bombObject.GetComponent("BombTimer") as BombTimer).explodeRange = explodeRange;
				bombs.Add (bombObject);
			}
		}

		if (Input.GetKey ("left")) {
			canMove = true;
			for (int i = GenerateMapScript.walls.Count - 1; i >= 0; i--) {
				wall = GenerateMapScript.walls[i];
				if (((wall.GetComponent("WallBehavior") as WallBehavior).x == (x - 1)) && ((wall.GetComponent("WallBehavior") as WallBehavior).y == y)) {
					canMove = false;
				}
			}
			for (int i = GenerateMapScript.blocks.Count - 1; i >= 0; i--) {
				block = GenerateMapScript.blocks[i];
				if (((block.GetComponent("BlockBehavior") as BlockBehavior).x == (x - 1)) && ((block.GetComponent("BlockBehavior") as BlockBehavior).y == y)) {
					canMove = false;
				}
			}
			if (canMove == true) {
				StartCoroutine(MoveFromTo(transform.position, transform.position + new Vector3 (-1, 0, 0), 0.4f, -1, 0));
			}
		}

		else if (Input.GetKey("right")) {
			canMove = true;
			for (int i = GenerateMapScript.walls.Count - 1; i >= 0; i--) {
				wall = GenerateMapScript.walls[i];
				if (((wall.GetComponent("WallBehavior") as WallBehavior).x == (x + 1)) && ((wall.GetComponent("WallBehavior") as WallBehavior).y == y)) {
					canMove = false;
				}
			}
			for (int i = GenerateMapScript.blocks.Count - 1; i >= 0; i--) {
				block = GenerateMapScript.blocks[i];
				if (((block.GetComponent("BlockBehavior") as BlockBehavior).x == (x + 1)) && ((block.GetComponent("BlockBehavior") as BlockBehavior).y == y)) {
					canMove = false;
				}
			}
			if (canMove == true)
				StartCoroutine(MoveFromTo(transform.position, transform.position + new Vector3 (1, 0, 0), 0.4f, 1, 0));
		}

		else if (Input.GetKey("up")) {
			canMove = true;
			for (int i = GenerateMapScript.walls.Count - 1; i >= 0; i--) {
				wall = GenerateMapScript.walls[i];
				if (((wall.GetComponent("WallBehavior") as WallBehavior).x == x) && ((wall.GetComponent("WallBehavior") as WallBehavior).y == (y + 1))) {
					canMove = false;
				}
			}
			for (int i = GenerateMapScript.blocks.Count - 1; i >= 0; i--) {
				block = GenerateMapScript.blocks[i];
				if (((block.GetComponent("BlockBehavior") as BlockBehavior).x == x) && ((block.GetComponent("BlockBehavior") as BlockBehavior).y == (y + 1))) {
					canMove = false;
				}
			}
			if (canMove == true)
				StartCoroutine(MoveFromTo(transform.position, transform.position + new Vector3 (0, 1, 0), 0.4f, 0, 1));
		}

		else if (Input.GetKey("down")) {
			canMove = true;
			for (int i = GenerateMapScript.walls.Count - 1; i >= 0; i--) {
				wall = GenerateMapScript.walls[i];
				if (((wall.GetComponent("WallBehavior") as WallBehavior).x == x) && ((wall.GetComponent("WallBehavior") as WallBehavior).y == (y - 1))) {
					canMove = false;
				}
			}
			for (int i = GenerateMapScript.blocks.Count - 1; i >= 0; i--) {
				block = GenerateMapScript.blocks[i];
				if (((block.GetComponent("BlockBehavior") as BlockBehavior).x == x) && ((block.GetComponent("BlockBehavior") as BlockBehavior).y == (y - 1))) {
					canMove = false;
				}
			}
			if (canMove == true)
				StartCoroutine(MoveFromTo(transform.position, transform.position + new Vector3 (0, -1, 0), 0.4f, 0, -1));
			//Debug.Log(GenerateMapScript.players[0].transform.position.x + ", " + GenerateMapScript.players[0].transform.position.z);
			//Debug.Log(transform.position);
		}

		//lastX = CrossPlatformInputManager.GetAxisRaw ("HorizontalControl");
		//lastY = CrossPlatformInputManager.GetAxisRaw ("VerticalControl");
	}
}
