using TMPro;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    public static UIScript Instance;
    private int killCount = 0;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI killCountText;
    [Space]
    [SerializeField] private GameObject gameOverPanel;

    void Awake()
    {
        Instance = this;
        Time.timeScale = 1f;
    }

    void Update()
    {
        timerText.text = Time.time.ToString("F2") + "s";
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
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
