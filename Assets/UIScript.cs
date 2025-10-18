using TMPro;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    public static UIScript Instance;
    private int killCount = 0;
    private float startTime;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI killCountText;
    [Space]
    [SerializeField] private GameObject gameOverPanel;

    void Awake()
    {
        Instance = this;
        Time.timeScale = 1f;
        // initialize timer start so we can reset it on restart
        startTime = Time.time;
    }

    void Update()
    {
        float elapsed = Time.time - startTime;
        timerText.text = elapsed.ToString("F2") + "s";
    }

    public void UpdateKillCount()
    {
        killCount++;
        killCountText.text = killCount.ToString();
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0.3f;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        // reset timer start so the timer shows 0 immediately (useful if Restart is called without a full app restart)
        startTime = Time.time;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
