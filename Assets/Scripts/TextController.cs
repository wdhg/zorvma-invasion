using UnityEngine;

public class TextController : MonoBehaviour {

	public HeadController mayor, alien;
	public string[] mayorPhrases, alienPhrases;
	public string deathPhrase;
	public float minPhraseRate, maxPhraseRate;
	public float nextPhraseTime;
	public PlanetController pc;
	public float timePerCharacter;
	public float textBoxLifetime;

	private string phraseToSay;
	private string saidPhrase;
	private float nextCharTime;
	private float closeTextboxTime;
	private HeadController currentlySpeaking;

	private void Start() {
		saidPhrase = "";
		phraseToSay = "";
		closeTextboxTime = 0f;
	}

	private void TurnOffText() {
		mayor.ClearText();
		alien.ClearText();
	}

	private void SayPhrase() {
		TurnOffText();
		saidPhrase = "";
		if(Random.value >= 0.5f) {
			currentlySpeaking = mayor;
			phraseToSay = mayorPhrases[Random.Range(0, mayorPhrases.Length)];
		} else {
			currentlySpeaking = alien;
			phraseToSay = alienPhrases[Random.Range(0, alienPhrases.Length)];
		}
		closeTextboxTime = Time.time + (timePerCharacter * phraseToSay.Length) + textBoxLifetime;
		nextPhraseTime = closeTextboxTime + Random.Range(minPhraseRate, maxPhraseRate);
	}

	private void UpdatePhrase() {
		saidPhrase += phraseToSay[saidPhrase.Length];
		currentlySpeaking.Say(saidPhrase);
		nextCharTime = Time.time + timePerCharacter;
	}

	private void Update() {
		if(pc.alive) { // Only speak when the play is alive
			if(phraseToSay == deathPhrase) {
				TurnOffText();
				phraseToSay = "";
				saidPhrase = "";
			}
			if(phraseToSay == saidPhrase) { // If we have finished speaking
				if(Time.time >= nextPhraseTime) { // If it is time to speak again
					SayPhrase();
				} else if(Time.time >= closeTextboxTime) { // If it is time to close the textbox
					TurnOffText();
				}
			} else if(phraseToSay != saidPhrase && Time.time > nextCharTime) { // If it is time to add another character
				UpdatePhrase();
			}
		} else {
			alien.ClearText();
			if(phraseToSay != deathPhrase) {
				saidPhrase = "";
				phraseToSay = deathPhrase;
				currentlySpeaking = mayor;
			}
			if(saidPhrase != phraseToSay && Time.time > nextCharTime) {
				UpdatePhrase();
			}
		}
	}
}