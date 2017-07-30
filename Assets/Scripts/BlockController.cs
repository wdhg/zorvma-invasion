using UnityEngine;

public class BlockController : MonoBehaviour {

	public float speed;
	public float angle;
	public float maxRadius;

	private Transform blockObject;

	private void Start() {
		blockObject = transform.Find("Block Object");
	}

	private void FixedUpdate() {
		blockObject.Translate(Vector2.up * speed * Time.fixedDeltaTime);
		blockObject.localScale = new Vector3(
			Mathf.Tan((angle / 2f) * Mathf.Deg2Rad) * blockObject.localPosition.y * 2f * 0.8f,
			blockObject.localScale.y,
			1f
		);
		if(blockObject.localPosition.y > maxRadius) {
			Destroy(gameObject);
		}
	}
}