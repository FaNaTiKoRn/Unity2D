using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance;

    [SerializeField] private GameObject gameOverText;

    private bool isGameOver = false;

    private void Awake()
    {
        Instance = this;
        gameOverText.SetActive(false);
    }

    public void ShowGameOver()
    {
        if (isGameOver) return; // Evitar múltiples llamadas

        isGameOver = true;
        gameOverText.SetActive(true);

    }

    private void Update()
    {
        if (!isGameOver) return;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
