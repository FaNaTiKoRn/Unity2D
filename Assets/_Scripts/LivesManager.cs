using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LivesManager : MonoBehaviour
{
    public static LivesManager Instance;

    [SerializeField] private GameObject lifeIconPrefab;
    [SerializeField] private int maxLives = 3;
    [SerializeField] private Vector3 startPosition = new Vector3(-7f, 4f, 0); // Arriba izquierda
    [SerializeField] private float spacing = 0.6f;

    private List<GameObject> lifeIcons = new List<GameObject>();
    private int currentLives;
    private bool isGameOver = false;
    private GameObject player;
    private AsteroidSpawner spawner;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spawner = FindObjectOfType<AsteroidSpawner>();

        currentLives = maxLives;

        for (int i = 0; i < maxLives; i++)
        {
            Vector3 position = startPosition + new Vector3(i * spacing, 0, 0);
            GameObject icon = Instantiate(lifeIconPrefab, position, Quaternion.identity, transform);
            lifeIcons.Add(icon);
        }
    }

    private void Update()
    {
        if (isGameOver && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0)))
        {
            RestartGame();
        }
    }

    public void LoseLife()
    {
        if (isGameOver || currentLives <= 0) return;

        currentLives--;

        // Sonido de colisión simple
        SoundManager.Instance?.PlaySound(SoundManager.Instance.shipCollide);

        if (currentLives >= 0 && currentLives < lifeIcons.Count)
        {
            lifeIcons[currentLives].SetActive(false);
        }

        if (currentLives == 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        isGameOver = true;

        if (player != null)
            player.SetActive(false);

        if (spawner != null)
            spawner.IncreaseSpawnSpeed(0.1f); // Asteroides más rápidos

        // Sonidos de destrucción y fin del juego
        SoundManager.Instance?.PlaySound(SoundManager.Instance.shipDestroy);
        SoundManager.Instance?.PlaySound(SoundManager.Instance.gameOver);

        UIManager.Instance?.ShowGameOver(true); // Muestra el texto GAME OVER
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
