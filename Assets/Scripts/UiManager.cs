using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UiManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public GameObject gameOverUi;
    public static UiManager instance;
    public int currentScore;

    public void Awake()
    {
        instance = this;
    }
    public void Start()
    {
       currentScore = 0;
       gameOverUi.SetActive(false);
    }

    public void SetScoreText(int score)
    {
        currentScore+= score;
        scoreText.text = $"Score: {currentScore}";
    }
    public void SetActiveGameOverUi(bool active)
    {
        gameOverUi.SetActive(active);
    }

    public void OnClickRestart()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

}
