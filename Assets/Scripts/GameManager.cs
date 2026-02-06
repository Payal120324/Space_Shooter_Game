using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Enemy")]
    public GameObject enemyPrefab;
    public float minInstantiateValue;
    public float maxInstantiateValue;
    public float enemyDestroyTime = 10f;

    [Header("Particle Effects")]
    public GameObject explosion;
    public GameObject muzzleFlash;

    [Header("Panels")]
    public GameObject startMenu;
    public GameObject pauseMenu;
    public GameObject gameOverPanel;

    [Header("Score")]
    public int score;
    public Text scoreText;
    public Text gameOverScoreText;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        startMenu.SetActive(true);
        pauseMenu.SetActive(false);
        gameOverPanel.SetActive(false);

        Time.timeScale = 0f;

        score = 0;
        scoreText.text = "Score : 0";

        InvokeRepeating(nameof(InstantiateEnemy), 1f, 1f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            PauseGame(true);
    }

    void InstantiateEnemy()
    {
        Vector3 pos = new Vector3(Random.Range(minInstantiateValue, maxInstantiateValue), 6f);
        GameObject enemy = Instantiate(enemyPrefab, pos, Quaternion.Euler(0, 0, 180));
        Destroy(enemy, enemyDestroyTime);
    }

    public void AddScore(int value)
    {
        score += value;
        scoreText.text = "Score : " + score;
    }

    public void GameOver()
    {
        Debug.Log("GAME OVER CALLED");
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
        gameOverScoreText.text = "Final Score : " + score;
    }

    public void StartGameButton()
    {
        startMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void PauseGame(bool pause)
    {
        pauseMenu.SetActive(pause);
        Time.timeScale = pause ? 0f : 1f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
