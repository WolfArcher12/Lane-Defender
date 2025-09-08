using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject gameCanvas;
    [SerializeField] TMPro.TextMeshProUGUI scoreText;
    [SerializeField] TMPro.TextMeshProUGUI highscoreText;
    [SerializeField] TMPro.TextMeshProUGUI livesText;
    [SerializeField] GameObject gameOverCanvas;
    [SerializeField] TMPro.TextMeshProUGUI gameOverScoreText;
    [SerializeField] TMPro.TextMeshProUGUI gameOverHighscoreText;
    [SerializeField] GameObject restartButton;
    [SerializeField] GameObject quitButton;


    public float lives = 3f;
    public float score = 0f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
        UpdateUI();
        GameOver();
    }

    private void UpdateUI()
    {
        scoreText.text = "Score: " + score.ToString();
        PlayerPrefs.SetFloat("Highscore", Mathf.Max(score, PlayerPrefs.GetFloat("Highscore", 0f)));
        highscoreText.text = "Highscore: " + PlayerPrefs.GetFloat("Highscore", 0f).ToString();
        livesText.text = "Lives: " + lives.ToString();
    }

    private void GameOver()
    {
        if (lives <= 0f)
        {
            gameCanvas.SetActive(false);
            gameOverCanvas.SetActive(true);
            gameOverScoreText.text = "Score: " + score.ToString();
            gameOverHighscoreText.text = "Highscore: " + PlayerPrefs.GetFloat("Highscore", 0f).ToString();
            restartButton.SetActive(true);
            quitButton.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }

}
