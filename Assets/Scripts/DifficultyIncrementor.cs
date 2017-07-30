using UnityEngine;

public class DifficultyIncrementor : MonoBehaviour {

	public RingGenerator rg;
	public float difficultyRating;

	private float startTime;

	public void ResetDifficulty() {
		startTime = Time.time;
	}

	private float CalculateDifficulty() {
		return (Time.time - startTime) / ((Time.time - startTime) + difficultyRating);
	}

	private void Update() {
		rg.spawnRate = Mathf.Lerp(rg.startSpawnRate, rg.maxSpawnRate, CalculateDifficulty());
	}
}