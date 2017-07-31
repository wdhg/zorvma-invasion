using System.Collections.Generic;
using UnityEngine;

public class RingGenerator : MonoBehaviour {

	public float startRadius;	
	public GameObject alienBlock;
	public float startSpawnRate, maxSpawnRate;
	public int minblockCount, blockCount;
	public bool spawnEnemies;
	[HideInInspector()]
	public float spawnRate;

	private float nextSpawnTime;

	public void ResetEnemies() {
		GameObject[] children = new GameObject[transform.childCount];
		for(int i = 0; i < transform.childCount; i++) {
			children[i] = transform.GetChild(i).gameObject;
		}
		foreach(GameObject child in children) {
			Destroy(child);
		}
	}

	private void Start() {
		spawnEnemies = true;
		spawnRate = startSpawnRate;
	}

	private void GenerateRing(int totalBlockCount, int actualBlockCount) {
		if(totalBlockCount < 3) {
			return;
		}
		float angle = 360f / totalBlockCount;
		List<float> angles = new List<float>();
		for(int i = 0; i < totalBlockCount; i++) {
			angles.Add(angle * i);
		}

		for(int i = 0; i < actualBlockCount; i++) {
			float rndAngle = angles[Random.Range(0, angles.Count)];
			angles.Remove(rndAngle);
			GameObject newBlock = (GameObject) Instantiate(alienBlock, transform.position, Quaternion.Euler(0f, 0f, rndAngle));
			newBlock.transform.parent = transform;
			newBlock.GetComponent<BlockController>().angle = angle;
		}
		nextSpawnTime = Time.time + (1 / spawnRate);
	}

	private void Update() {
		if(spawnEnemies && Time.time >= nextSpawnTime) {
			GenerateRing(blockCount, Random.Range(minblockCount, blockCount));
		}
	}
}