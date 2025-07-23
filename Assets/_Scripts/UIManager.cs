using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private TextMeshProUGUI gameOverText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        ShowGameOver(false); // Ocultar texto al inicio
    }

    public void ShowGameOver(bool show)
    {
        if (gameOverText != null)
            gameOverText.gameObject.SetActive(show);
    }
}
