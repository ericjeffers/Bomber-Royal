using UnityEngine;
using System.Collections;

public class BombTimer : MonoBehaviour {

	float timer = 3f;
	public int explodeRange;

	public int x;
	public int y;

	GameObject Object1;
	GenerateMap GenerateMapScript;
	//PlayerBehavior PlayerBehaviorScript;
	private GameObject wall;
	private GameObject block;
	private GameObject player;
	private GameObject bomb;
	public GameObject explosion;

	private int rightExplosion = 0;
	private int leftExplosion = 0;
	private int upExplosion = 0;
	private int downExplosion = 0;

	// Use this for initialization
	void Start () {
		Object1 = GameObject.Find ("Generate Map");;
		GenerateMapScript = Object1.GetComponent<GenerateMap> ();
		//PlayerBehaviorScript = Object1.GetComponent<PlayerBehavior> ();

		player = GenerateMapScript.players[0];
		//x = (player.GetComponent("PlayerBehavior") as PlayerBehavior).x;
		//y = (player.GetComponent("PlayerBehavior") as PlayerBehavior).y;

		//Debug.Log ((player.GetComponent("PlayerBehavior") as PlayerBehavior).bombs.Count);
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;

		if (timer <= 0) {
			Destroy (gameObject);
			(player.GetComponent("PlayerBehavior") as PlayerBehavior).bombs.RemoveAt(0);
			(player.GetComponent("PlayerBehavior") as PlayerBehavior).bombCount ++;
			//right explosion
			for (int i = 1; i < explodeRange + 1; i++) {
				for (int j = GenerateMapScript.walls.Count - 1; j >= 0; j--) {
					wall = GenerateMapScript.walls[j];
					if ((wall.GetComponent("WallBehavior") as WallBehavior).x == (x + i) && (wall.GetComponent("WallBehavior") as WallBehavior).y == y) {
						i = explodeRange + 1; //break outer for loop
						break;
					}
				}
				if (i == explodeRange + 1) //instantly break loop
					break;

				for (int j = GenerateMapScript.blocks.Count - 1; j >= 0; j--) {
					block = GenerateMapScript.blocks[j];
					if ((block.GetComponent("BlockBehavior") as BlockBehavior).x == (x + i) && (block.GetComponent("BlockBehavior") as BlockBehavior).y == y) {
						Destroy (block);
						GenerateMapScript.blocks.RemoveAt(j);
						rightExplosion ++;
						i = explodeRange + 1; //break outer for loop
						break;
					}
				}
				if (i == explodeRange + 1) //instantly break loop
					break;

				for (int j = (player.GetComponent("PlayerBehavior") as PlayerBehavior).bombs.Count - 1; j >= 0; j--) {
					bomb = (player.GetComponent("PlayerBehavior") as PlayerBehavior).bombs[j];
					if ((bomb.GetComponent("BombTimer") as BombTimer).x == (x + i) && (bomb.GetComponent("BombTimer") as BombTimer).y == y) {
						(bomb.GetComponent("BombTimer") as BombTimer).timer = 0;
						i = explodeRange + 1; //break outer for loop
						break;
					}
				}
				if (i == explodeRange + 1) //instantly break loop
					break;
				rightExplosion ++;
			}
			//left explosion
			for (int i = 1; i < explodeRange + 1; i++) {
				for (int j = GenerateMapScript.walls.Count - 1; j >= 0; j--) {
					wall = GenerateMapScript.walls[j];
					if ((wall.GetComponent("WallBehavior") as WallBehavior).x == (x - i) && (wall.GetComponent("WallBehavior") as WallBehavior).y == y) {
						i = explodeRange + 1; //break outer for loop
						break;
					}
				}
				if (i == explodeRange + 1) //instantly break loop
					break;

				for (int j = GenerateMapScript.blocks.Count - 1; j >= 0; j--) {
					block = GenerateMapScript.blocks[j];
					//Debug.Log((block.GetComponent("BlockBehavior") as BlockBehavior).x);
					if ((block.GetComponent("BlockBehavior") as BlockBehavior).x == (x - i) && (block.GetComponent("BlockBehavior") as BlockBehavior).y == y)
					{
						Destroy (block);
						GenerateMapScript.blocks.RemoveAt(j);
						leftExplosion ++;
						i = explodeRange + 1; //break for loop
						break;
					}
				}
				if (i == explodeRange + 1) //instantly break loop
					break;

				for (int j = (player.GetComponent("PlayerBehavior") as PlayerBehavior).bombs.Count - 1; j >= 0; j--) {
					bomb = (player.GetComponent("PlayerBehavior") as PlayerBehavior).bombs[j];
					if ((bomb.GetComponent("BombTimer") as BombTimer).x == (x - i) && (bomb.GetComponent("BombTimer") as BombTimer).y == y) {
						(bomb.GetComponent("BombTimer") as BombTimer).timer = 0;
						i = explodeRange + 1; //break outer for loop
						break;
					}
				}
				if (i == explodeRange + 1) //instantly break loop
					break;
				leftExplosion ++;
			}
			//up explosion
			for (int i = 1; i < explodeRange + 1; i++) {
				for (int j = GenerateMapScript.walls.Count - 1; j >= 0; j--) {
					wall = GenerateMapScript.walls[j];
					if ((wall.GetComponent("WallBehavior") as WallBehavior).x == x && (wall.GetComponent("WallBehavior") as WallBehavior).y == (y + i)) {
						i = explodeRange + 1; //break outer for loop
						break;
					}
				}
				if (i == explodeRange + 1) //instantly break loop
					break;

				for (int j = GenerateMapScript.blocks.Count - 1; j >= 0; j--) {
					block = GenerateMapScript.blocks[j];
					//Debug.Log((block.GetComponent("BlockBehavior") as BlockBehavior).x);
					if ((block.GetComponent("BlockBehavior") as BlockBehavior).x == x && (block.GetComponent("BlockBehavior") as BlockBehavior).y == (y + i))
					{
						Destroy (block);
						GenerateMapScript.blocks.RemoveAt(j);
						upExplosion ++;
						i = explodeRange + 1; //break for loop
						break;
					}
				}
				if (i == explodeRange + 1) //instantly break loop
					break;

				for (int j = (player.GetComponent("PlayerBehavior") as PlayerBehavior).bombs.Count - 1; j >= 0; j--) {
					bomb = (player.GetComponent("PlayerBehavior") as PlayerBehavior).bombs[j];
					if ((bomb.GetComponent("BombTimer") as BombTimer).x == x && (bomb.GetComponent("BombTimer") as BombTimer).y == (y + i)) {
						(bomb.GetComponent("BombTimer") as BombTimer).timer = 0;
						i = explodeRange + 1; //break outer for loop
						break;
					}
				}
				if (i == explodeRange + 1) //instantly break loop
					break;
				upExplosion ++;
			}
			//down explosion
			for (int i = 1; i < explodeRange + 1; i++) {
				for (int j = GenerateMapScript.walls.Count - 1; j >= 0; j--) {
					wall = GenerateMapScript.walls[j];
					if ((wall.GetComponent("WallBehavior") as WallBehavior).x == x && (wall.GetComponent("WallBehavior") as WallBehavior).y == (y - i)) {
						i = explodeRange + 1; //break outer for loop
						break;
					}
				}
				if (i == explodeRange + 1) //instantly break loop
					break;

				for (int j = GenerateMapScript.blocks.Count - 1; j >= 0; j--) {
					block = GenerateMapScript.blocks[j];
					//Debug.Log((block.GetComponent("BlockBehavior") as BlockBehavior).x);
					if ((block.GetComponent("BlockBehavior") as BlockBehavior).x == x && (block.GetComponent("BlockBehavior") as BlockBehavior).y == (y - i))
					{
						Destroy (block);
						GenerateMapScript.blocks.RemoveAt(j);
						downExplosion ++;
						i = explodeRange + 1; //break for loop
						break;
					}
				}
				if (i == explodeRange + 1) //instantly break loop
					break;

				for (int j = (player.GetComponent("PlayerBehavior") as PlayerBehavior).bombs.Count - 1; j >= 0; j--) {
					bomb = (player.GetComponent("PlayerBehavior") as PlayerBehavior).bombs[j];
					if ((bomb.GetComponent("BombTimer") as BombTimer).x == x && (bomb.GetComponent("BombTimer") as BombTimer).y == (y - i)) {
						(bomb.GetComponent("BombTimer") as BombTimer).timer = 0;
						i = explodeRange + 1; //break outer for loop
						break;
					}
				}
				if (i == explodeRange + 1) //instantly break loop
					break;
				downExplosion ++;
			}

			Instantiate (explosion, new Vector3(x, y, -1), Quaternion.identity);
			if((player.GetComponent("PlayerBehavior") as PlayerBehavior).x == x && (player.GetComponent("PlayerBehavior") as PlayerBehavior).y == y)
				Debug.Log ("You are dead. -center");
			for (int i = 1; i <= rightExplosion; i++) {
				Instantiate(explosion, new Vector3(x + i, y, -1), Quaternion.identity);
				if((player.GetComponent("PlayerBehavior") as PlayerBehavior).x == (x + i) && (player.GetComponent("PlayerBehavior") as PlayerBehavior).y == y)
					Debug.Log("You are dead. -right");
			}
			for (int i = 1; i <= leftExplosion; i++) {
				Instantiate(explosion, new Vector3(x - i, y, -1), Quaternion.identity);
				if((player.GetComponent("PlayerBehavior") as PlayerBehavior).x == (x - i) && (player.GetComponent("PlayerBehavior") as PlayerBehavior).y == y)
					Debug.Log("You are dead. -left");
			}
			for (int i = 1; i <= upExplosion; i++) {
				Instantiate(explosion, new Vector3(x, y + i, -1), Quaternion.identity);
				if((player.GetComponent("PlayerBehavior") as PlayerBehavior).x == x && (player.GetComponent("PlayerBehavior") as PlayerBehavior).y == (y + i))
					Debug.Log("You are dead. -up");
			}
			for (int i = 1; i <= downExplosion; i++) {
				Instantiate(explosion, new Vector3(x, y - i, -1), Quaternion.identity);
				if((player.GetComponent("PlayerBehavior") as PlayerBehavior).x == x && (player.GetComponent("PlayerBehavior") as PlayerBehavior).y == (y - i))
					Debug.Log("You are dead. -down");
			}
			
				/*if (block != null) {
				if ((block.transform.position.x > transform.position.x && block.transform.position.x <= transform.position.x + explodeRange && block.transform.position.y == transform.position.y) ||
					    (block.transform.position.x == transform.position.x - explodeRange && block.transform.position.y == transform.position.y) ||
				   	 	(block.transform.position.x == transform.position.x && block.transform.position.y == transform.position.y + explodeRange) ||
				   		(block.transform.position.x == transform.position.x && block.transform.position.y == transform.position.y - explodeRange)) {
							Destroy (block);
				}
			}
			for(var i = GenerateMapScript.blocks.Count - 1; i > -1; i--)
			{
				if (GenerateMapScript.blocks[i] == null)
					GenerateMapScript.blocks.RemoveAt(i);
			}*/
		}
	}
}
