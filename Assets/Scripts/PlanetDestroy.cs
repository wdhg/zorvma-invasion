using UnityEngine;

public class PlanetDestroy : MonoBehaviour {

	public PlanetController pc;

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Block") {
			pc.Die();
		}
	}
}
