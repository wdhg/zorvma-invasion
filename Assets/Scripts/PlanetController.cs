using UnityEngine;

public class PlanetController : MonoBehaviour {

	[Header("Movement")]
	public float minSpeed;
	public float maxSpeed;
	public float acceleration;
	public float deceleration;
	public float rocketCost;
	[Header("Combat")]
	public GameObject projectile;
	public float fireRate;
	public float shootCost;
	[Header("Other")]
	public EnergyController ec;
	public Renderer fire1, fire2;
	public AudioSource earthRocket;
	public RingGenerator rg;
	public DifficultyIncrementor di;
	public ScoreController sc;
	[HideInInspector()]
	public bool alive;

	private float currentSpeed;
	private float nextFireTime;
	private Transform planetObject;
	private AudioSource explosion;

	public void Die() {
		alive = false;
		explosion.Play();
	}

	private void Start() {
		alive = true;
		explosion = GetComponent<AudioSource>();
		planetObject = transform.Find("Planet Object");
	}

	private void Awake() {
		nextFireTime = Time.time;
	}

	private void Move() {
		// v = u + at
		if(Input.GetButton("Jump") && ec.energy > 0f) {
			currentSpeed += acceleration * Time.deltaTime;
			ec.energy -= rocketCost * Time.deltaTime;
		} else {
			currentSpeed -= deceleration * Time.deltaTime;
		}
		if(currentSpeed > minSpeed) {
			fire1.enabled = Input.GetButton("Jump");
			fire2.enabled = !Input.GetButton("Jump");
			if(!earthRocket.isPlaying) {
				earthRocket.Play();
			}
		} else {
			fire1.enabled = false;
			fire2.enabled = false;
			earthRocket.Stop();
		}
		currentSpeed = Mathf.Clamp(currentSpeed, minSpeed, maxSpeed);
		transform.Rotate(0f, 0f, currentSpeed * Time.deltaTime);
	}

	private void Shoot() {
		Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 targetVector = mousePos - (Vector2) planetObject.position;
		float angle = Mathf.Atan2(targetVector.y, targetVector.x) * Mathf.Rad2Deg;
		Instantiate(projectile, planetObject.position, Quaternion.Euler(0f, 0f, angle));
		ec.energy -= shootCost;
		nextFireTime = Time.time + fireRate;
	}

	private void Update() {
		if(alive) {
			Move();
			if(Input.GetButton("Fire1") && (Time.time >= nextFireTime && ec.energy >= shootCost)) {
				Shoot();
			}
		} else if(!alive && Input.anyKeyDown) {
			rg.ResetEnemies();
			di.ResetDifficulty();
			sc.ResetScore();
			alive = true;
			ec.energy = 50f;
		}
		planetObject.gameObject.SetActive(alive);
	}
}