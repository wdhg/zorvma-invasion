using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour {

	private void Update() {
		if(Input.GetKeyDown(KeyCode.Space)) {
			SceneManager.LoadScene(1);
		} else if(Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}
	}
}