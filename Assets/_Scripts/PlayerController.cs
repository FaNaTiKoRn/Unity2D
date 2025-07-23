using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField, Range(1, 20)]
    float m_speed = 9f;

    [SerializeField]
    Vector2 m_VerticalLimitConf = new Vector2(0.1f, 0.8f);

    [SerializeField, Header("Projectile")]
    Vector3 m_projectileOffset = new Vector3(0, 0.5f, 0);

    float m_horizontalLimit = 8f;
    Vector2 m_verticalLimit;

    Camera cam;

    void Start()
    {
        cam = Camera.main;
        SetupLimits();
    }

    void Update()
    {
        MovePlayer();
        CheckLimits();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootProjectile();
            SoundManager.Instance.PlaySound(SoundManager.Instance.gunShot);
        }
    }

    void MovePlayer()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        transform.Translate(move * m_speed * Time.deltaTime);
    }

    void SetupLimits()
    {
        float min = (cam.orthographicSize * 2 * m_VerticalLimitConf.x) - cam.orthographicSize;
        float max = (cam.orthographicSize * 2 * m_VerticalLimitConf.y) - cam.orthographicSize;
        m_verticalLimit = new Vector2(min, max);
    }

    void CheckLimits()
    {
        Vector3 pos = cam.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp(pos.x, 0.05f, 0.95f); // Ajuste fino
        pos.y = Mathf.Clamp(pos.y, 0.05f, 0.95f);
        transform.position = cam.ViewportToWorldPoint(pos);
    }

    void ShootProjectile()
    {
        GameObject projectile = ObjectPoolManager.m_instance.GetPooledObject(ObjectPoolManager.PoolNames.Projectiles);
        if (projectile != null)
        {
            projectile.transform.position = transform.position + m_projectileOffset;
            projectile.transform.rotation = transform.rotation;
            projectile.SetActive(true);

            // Resetea estado si el proyectil tiene script propio
            var projScript = projectile.GetComponent<Projectile>();
            if (projScript != null)
                projScript.ResetProjectile();
        }
        else
        {
            Debug.LogWarning("No hay proyectiles disponibles en la pool.");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Asteroid"))
        {
            Debug.Log("¡Colisión con asteroide!");

            // 🔊 Reproducir sonido de colisión
            SoundManager.Instance?.PlaySound(SoundManager.Instance.shipCollide);

            LivesManager.Instance?.LoseLife();
            other.gameObject.SetActive(false);
        }
    }

}
