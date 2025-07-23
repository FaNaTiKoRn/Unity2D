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
