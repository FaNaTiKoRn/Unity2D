using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Disparos")]
    public AudioClip gunShot;

    [Header("Impactos")]
    public AudioClip shotCommon;
    public AudioClip shotFire;
    public AudioClip shotIce;
    public AudioClip shotHole;
    public AudioClip shipCollide;
    public AudioClip shipDestroy;

    [Header("Otros")]
    public AudioClip gameOver;
    public AudioClip backgroundMusic;

    private AudioSource fxSource;
    private AudioSource musicSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            fxSource = gameObject.AddComponent<AudioSource>();
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.loop = true;
            musicSource.volume = 0.4f; // ajustá volumen de fondo si querés

            musicSource.clip = backgroundMusic;
            musicSource.Play();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip != null)
            fxSource.PlayOneShot(clip);
    }
}
