using UnityEngine;

public class ProjectileController : MonoBehaviour {

	public float speed;
	public float lifeTime;

	private Rigidbody2D rb;
	private float endLifeTime;

	private void Start() {
		rb = GetComponent<Rigidbody2D>();
		endLifeTime = Time.time + lifeTime;
	}

	private void Update() {
		rb.velocity = transform.right * speed;
		if(Time.time > endLifeTime) {
			Destroy(gameObject);
		}
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Block") {
			Destroy(other.transform.parent.gameObject);
			((ScoreController) FindObjectOfType(typeof(ScoreController))).IncreaseScore();
		}
		if(other.tag != "Planet") {
			Destroy(gameObject);
		}
	}
}