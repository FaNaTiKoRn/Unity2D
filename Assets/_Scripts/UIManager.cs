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

    private void Update()
    {
        // Detectar Escape para salir en cualquier momento
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape presionado, cerrando juego...");
            Application.Quit();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }

    public void ShowGameOver(bool show)
    {
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(show);
            //SoundManager.Instance.PlaySound(SoundManager.Instance.gameOver);
        }
    }
}
