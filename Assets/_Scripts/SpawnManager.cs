using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] cometasPrefabs;
    [SerializeField] private float spawnInterval = 2f;

    private float timer = 0f;

    private float leftLimitX;
    private float rightLimitX;
    private float spawnY;

    void Start()
    {
        Camera cam = Camera.main;
        float camHeight = cam.orthographicSize * 2f;
        float camWidth = camHeight * cam.aspect;

        leftLimitX = cam.transform.position.x - camWidth / 2f + 0.5f;  // pequeño margen para no salir del borde
        rightLimitX = cam.transform.position.x + camWidth / 2f - 0.5f;

        spawnY = cam.transform.position.y + cam.orthographicSize + 1f; // justo arriba del view
    }

    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= spawnInterval)
        {
            timer = 0f;
            SpawnCometa();
        }
    }

    void SpawnCometa()
    {
        // Posición aleatoria en X dentro de límites
        float spawnX = Random.Range(leftLimitX, rightLimitX);

        // Prefab aleatorio
        int index = Random.Range(0, cometasPrefabs.Length);

        Vector3 spawnPos = new Vector3(spawnX, spawnY, 0);

        Instantiate(cometasPrefabs[index], spawnPos, Quaternion.identity);
    }
}
