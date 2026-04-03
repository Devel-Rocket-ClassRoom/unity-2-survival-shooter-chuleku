using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UiManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gunPowerText;
    public TextMeshProUGUI spawnTimeText;
    public GameObject gameOverUi;
    public GameObject pauseMenuUi;
    private bool isPaused;
    public static UiManager instance;
    public int currentScore;
    public bool enemySounds;
    public GameObject buttonToggleOn;
    public GameObject buttonToggleOff;

    public void Awake()
    {
        instance = this;
    }
    public void Start()
    {
       currentScore = 0;
       gameOverUi.SetActive(false);
        isPaused = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) Resume();
            else Pause();
        }
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
    public void SetGunPowerText(float power)
    {
        gunPowerText.text = $"GunPower : {power}";
    }

    public void SetSpawnTimeText(float  spawnTime)
    {
        spawnTimeText.text = $"SpawnTime : {spawnTime:F1}";
    }

    public void OnClickRestart()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
    public void Resume()
    {
        pauseMenuUi.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

    }

    public void Pause()
    {
        pauseMenuUi.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

    }
    public void EnemySoundOn()
    {
        enemySounds = true;
        Debug.Log("사운드 킴");
        buttonToggleOn.SetActive(true);
        buttonToggleOff.SetActive(false);
    }
    public void EnemySoundOff()
    {
        enemySounds = false;
        Debug.Log("사운드 끔");
        buttonToggleOn.SetActive(false);
        buttonToggleOff.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit(); //실제게임에서 나가짐

        UnityEditor.EditorApplication.isPlaying = false; // 유니티에서만 멈춤
    }
    
}
