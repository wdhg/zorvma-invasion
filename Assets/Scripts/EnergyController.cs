using UnityEngine;

public class EnergyController : MonoBehaviour {

	public Transform EnergyMeter;
	// Between 0 and 100
	public float energy;
	[Range(1, 100)]
	public float energyProduction;
	public Transform planet, sun;

	private void Update() {
		energy = Mathf.Clamp(energy, 0f, 100f);
		if(!Physics2D.Linecast(planet.position, sun.position, 1 << 8)) {
			// We are in LOS of the sun!
			energy += energyProduction * Time.deltaTime;
		}
		EnergyMeter.localScale = new Vector3(1f, energy / 100f, 1f);
	}
}