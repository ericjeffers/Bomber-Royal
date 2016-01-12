using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenerateMap : MonoBehaviour {

	public GameObject floor;
	public GameObject wallPiece;
	public GameObject block;
	public GameObject player1;
	public GameObject player2;
	public GameObject player3;
	public GameObject player4;

	public List<GameObject> walls = new List<GameObject> ();
	public List<GameObject> blocks = new List<GameObject> ();
	public List<GameObject> players = new List<GameObject> ();
	
	private string[] halfMap;
	private string[] mapHold;
	public int dimension;
	public int[,] fullMap;

	private int blockCount;

	private void FillBlocks(int minX, int maxX, int minY, int maxY) {
		blockCount = 0;
		while (blockCount < (dimension / 2 * 3)) {
			for (int x = minX; x < maxX; x++) {
				for (int y = minY; y < maxY; y++) {					//mapHold.Length; if not square, but need to make work for fullMap too
					if (fullMap [x, y] == 0) {
						var number = Random.Range (0, 10);
						if (number > 8) {
							fullMap [x, y] = 2;
							blockCount ++;
						}
					}
				}
			}
			/*if (blockCount1 == (dimension / 2 * 3 - 1)) {
				break;
			}*/
		}
	}
	
	// Use this for initialization
	void Start () {
		string input = System.IO.File.ReadAllText(@"Assets/Maps/map1.txt");

		halfMap = input.Split(new char[] {','});
		dimension = halfMap.Length;
		fullMap = new int[dimension,dimension];

		for (int x = 0; x < dimension; x++) {
			mapHold = halfMap[x].Split(new char[] {' '});
			for (int y = 0; y < dimension; y++) {					//mapHold.Length; if not square, but need to make work for fullMap too
				fullMap[x,y] = int.Parse(mapHold[y]);
			}
		}

		FillBlocks(1, (dimension / 2), 1, (dimension / 2));
		FillBlocks(((dimension / 2) + 1), dimension, 1, (dimension / 2));
		FillBlocks(1, (dimension / 2), ((dimension / 2) + 1), dimension);
		FillBlocks(((dimension / 2) + 1), dimension, ((dimension / 2) + 1), dimension);
		

		//int dimension = map.GetLength (0);

		Quaternion spawnRotation = Quaternion.Euler(270,0,0);

		for (int x = 0; x < dimension; x++) {
			for (int y = 0; y < dimension; y++) {
				//Debug.Log (fullMap[x,z]);
				switch (fullMap[y,x]) {
				case 0:
					Instantiate(floor, new Vector3(x, y*(-1), 0), spawnRotation);

					//blockCount = 0;
					//while (blockCount < (dimension * dimension / 6)
					//Instantiate(block, new Vector3(x, 0, z*(-1)), Quaternion.identity);

					break;
				case 1:
					Instantiate(floor, new Vector3(x, y*(-1), 0), spawnRotation);
					//Instantiate(wallPiece, new Vector3(x, 0, z*(-1)), Quaternion.identity);
					GameObject wallObject = Instantiate(wallPiece, new Vector3(x, y*(-1), -0.5f), Quaternion.identity) as GameObject;
					(wallObject.GetComponent("WallBehavior") as WallBehavior).x = x;
					(wallObject.GetComponent("WallBehavior") as WallBehavior).y = y*-1;
					walls.Add(wallObject);
					break;
				case 2:
					Instantiate(floor, new Vector3(x, y*(-1), 0), spawnRotation);
					GameObject blockObject = Instantiate(block, new Vector3(x, y*(-1), -0.5f), Quaternion.identity) as GameObject;
					(blockObject.GetComponent("BlockBehavior") as BlockBehavior).x = x;
					(blockObject.GetComponent("BlockBehavior") as BlockBehavior).y = y*-1;
					blocks.Add (blockObject);
					break;
				case 3:
					Instantiate(floor, new Vector3(x, y*(-1), 0), spawnRotation);
					break;
				case 4:
					break;
				case 5:
					break;
				case 6:
					Instantiate(floor, new Vector3(x, y*(-1), 0), spawnRotation);
					GameObject p1 = Instantiate(player1, new Vector3(x, y*(-1), -0.5f), Quaternion.identity) as GameObject;
					(p1.GetComponent("PlayerBehavior") as PlayerBehavior).x = x;
					(p1.GetComponent("PlayerBehavior") as PlayerBehavior).y = y*-1;
					players.Add(p1);
					break;
				case 7:
					Instantiate(floor, new Vector3(x, y*(-1), 0), spawnRotation);
					GameObject p2 = Instantiate(player2, new Vector3(x, y*(-1), -0.5f), Quaternion.identity) as GameObject;
					//(p2.GetComponent("PlayerBehavior") as PlayerBehavior).x = x;
					//(p2.GetComponent("PlayerBehavior") as PlayerBehavior).y = y*-1;
					players.Add(p2);
					break;
				case 8:
					Instantiate(floor, new Vector3(x, y*(-1), 0), spawnRotation);
					GameObject p3 = Instantiate(player3, new Vector3(x, y*(-1), -0.5f), Quaternion.identity) as GameObject;
					//(p3.GetComponent("PlayerBehavior") as PlayerBehavior).x = x;
					//(p3.GetComponent("PlayerBehavior") as PlayerBehavior).y = y*-1;
					players.Add(p3);
					break;
				case 9:
					Instantiate(floor, new Vector3(x, y*(-1), 0), spawnRotation);
					GameObject p4 = Instantiate(player4, new Vector3(x, y*(-1), -0.5f), Quaternion.identity) as GameObject;
					//(p4.GetComponent("PlayerBehavior") as PlayerBehavior).x = x;
					//(p4.GetComponent("PlayerBehavior") as PlayerBehavior).y = y*-1;
					players.Add(p4);
					break;
				}
			}
		}
		/*
		for (int i = 0; i < numPlayers; i++)
		{
			switch(i)
			{
			case 0:
				spawnX = playerSpawn[0,0];
				spawnZ = playerSpawn[0,1];
				break;
			case 1:
				spawnX = playerSpawn[1,0];
				spawnZ = playerSpawn[1,1];
				break;
			case 2:
				spawnX = playerSpawn[2,0];
				spawnZ = playerSpawn[2,1];
				break;
			case 3:
				spawnX = playerSpawn[3,0];
				spawnZ = playerSpawn[3,1];
				break;
			}
			
			Instantiate(player, new Vector3(spawnX, 0.5f, spawnZ*(-1)), Quaternion.identity);
		}
*/

		//Debug.Log(walls[62].transform.position.x);
		/*foreach (GameObject blah in walls)
			Debug.Log (blah.transform.position.x);*/
	}

	// Update is called once per frame
	void Update () {
	
	}
}
