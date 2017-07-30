using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

	public int score;
	public int scoreIncrease;
	public Text scoreText;
	public Text highScoreText;

	private int highScore;

	private void Start() {
		highScore = PlayerPrefs.GetInt("High Score");
	}

	public void ResetScore() {
		score = 0;
		scoreText.text = "Score: " + score.ToString();
	}

	public void IncreaseScore() {
		score += scoreIncrease;
		if(score > highScore) {
			highScore = score;
			PlayerPrefs.SetInt("High Score", highScore);
			highScoreText.text = "High Score: " + highScore.ToString();
		}
		scoreText.text = "Score: " + score.ToString();
	}
}