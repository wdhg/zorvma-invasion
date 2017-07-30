using UnityEngine;
using UnityEngine.UI;

public class HeadController : MonoBehaviour {

	public string talking, idle;
	public bool isTalking;
	public GameObject textbox;
	public Text textboxText;
	public AudioSource talkingSound;

	private Animation anim;

	public void Say(string phrase) {
		textboxText.text = phrase;
		talkingSound.Stop();
		talkingSound.pitch = Random.Range(0.9f, 1.1f);
		talkingSound.Play();
	}

	public void ClearText() {
		textboxText.text = "";
	}

	private void Start() {
		textboxText.text = "";
		anim = GetComponent<Animation>();
	}

	private void Update() {
		isTalking = textboxText.text != "";
		textbox.SetActive(isTalking);
		if(isTalking) {
			anim.Play(talking);
		} else {
			anim.Play(idle);
		}
	}
}