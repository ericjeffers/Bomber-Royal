using UnityEngine;
using System.Collections;

public class ExplosionBehavior : MonoBehaviour {

	public int x;
	public int y;

	float timer = 1.5f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		
		if (timer <= 0) {
			Destroy (gameObject);
		}
	}
}
