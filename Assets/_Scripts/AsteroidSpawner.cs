using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private float spawnRate = 1.5f;               // Tiempo entre spawns
    [SerializeField] private float spawnYOffset = 1f;              // Altura extra para spawn arriba del viewport
    [SerializeField] private Vector2 speedRange = new Vector2(1f, 3f);  // Rango de velocidad

    private float timer = 0f;
    private float minX, maxX, spawnY;

    private void Start()
    {
        Camera cam = Camera.main;
        float camHeight = cam.orthographicSize * 2f;
        float camWidth = camHeight * cam.aspect;

        minX = cam.transform.position.x - camWidth / 2f + 0.5f;
        maxX = cam.transform.position.x + camWidth / 2f - 0.5f;
        spawnY = cam.transform.position.y + cam.orthographicSize + spawnYOffset;

        SpawnAsteroid();  // Spawn inicial para que no espere el primer intervalo
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnRate)
        {
            timer = 0f;
            SpawnAsteroid();
        }
    }

    private void SpawnAsteroid()
    {
        // Array con los tipos de asteroides que tienes definidos en tu ObjectPoolManager.PoolNames
        ObjectPoolManager.PoolNames[] tipos = {
            ObjectPoolManager.PoolNames.AsteroidePequeño,
            ObjectPoolManager.PoolNames.AsteroideMediano,
            ObjectPoolManager.PoolNames.AsteroideGrande,
            ObjectPoolManager.PoolNames.AsteroideGigante,
        };

        // Elige aleatoriamente un tipo de asteroide
        ObjectPoolManager.PoolNames tipoElegido = tipos[Random.Range(0, tipos.Length)];

        // Obtiene el objeto desde la pool según el tipo
        GameObject asteroid = ObjectPoolManager.m_instance.GetPooledObject(tipoElegido);

        if (asteroid != null)
        {
            float spawnX = Random.Range(minX, maxX);
            Vector3 spawnPos = new Vector3(spawnX, spawnY, 0);
            asteroid.transform.position = spawnPos;
            asteroid.transform.rotation = Quaternion.Euler(0, 0, Random.Range(-25f, 25f));
            asteroid.SetActive(true);

            // Configura la velocidad con el script Asteroid
            Asteroid script = asteroid.GetComponent<Asteroid>();
            if (script != null)
            {
                float randomSpeed = Random.Range(speedRange.x, speedRange.y);
                script.SetSpeed(randomSpeed);
            }
        }
        else
        {
            Debug.LogWarning("No hay asteroides disponibles en la pool para el tipo " + tipoElegido);
        }
    }

    public void IncreaseSpawnSpeed(float newRate)
    {
        spawnRate = newRate;
    }

}
