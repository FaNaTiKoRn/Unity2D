using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [SerializeField] private TextMeshProUGUI scoreText;
    private int score = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddPoints(int amount)
    {
        score += amount;
        Debug.Log("Sumando puntos: " + amount + " | Total: " + score);

        UpdateUI();

        // Aumentar dificultad: cada punto es un 1% más
        AsteroidSpawner spawner = FindObjectOfType<AsteroidSpawner>();
        if (spawner != null)
        {
            float difficultyMultiplier = 1f + (amount / 100f);  // ej. 3 puntos → x1.03
            spawner.IncreaseDifficulty(difficultyMultiplier);
            Debug.Log($"📈 Aumento de dificultad en {amount}% → x{difficultyMultiplier}");
        }
    }

    private void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Puntos: " + score;
        }
        else
        {
            Debug.LogWarning("⚠️ ¡No se asignó el TextMeshProUGUI al ScoreManager!");
        }
    }
}
