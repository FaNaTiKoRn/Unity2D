using UnityEngine;

public class ParallaxScroller : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 2f;
    private Camera mainCam;
    private Vector2 startPos;
    private float spriteHeight;

    private GameObject clone;

    void Start()
    {
        mainCam = Camera.main;

        // Escalar el fondo al tamaño del viewport
        ScaleToScreen();

        // Guardar posición original
        startPos = transform.position;

        // Obtener alto del sprite
        spriteHeight = GetComponent<SpriteRenderer>().bounds.size.y;

        // Duplicar el fondo
        clone = Instantiate(gameObject, transform.position + Vector3.up * spriteHeight, Quaternion.identity, transform.parent);
        Destroy(clone.GetComponent<ParallaxScroller>()); // Para evitar scroll recursivo
    }

    void Update()
    {
        float movement = scrollSpeed * Time.deltaTime;
        transform.Translate(Vector3.down * movement);
        clone.transform.Translate(Vector3.down * movement);

        // Si el fondo original bajó del todo, lo reubicamos arriba del clon
        if (transform.position.y < mainCam.transform.position.y - spriteHeight)
        {
            transform.position = clone.transform.position + Vector3.up * spriteHeight;
        }

        // Si el clon bajó del todo, lo reubicamos arriba del original
        if (clone.transform.position.y < mainCam.transform.position.y - spriteHeight)
        {
            clone.transform.position = transform.position + Vector3.up * spriteHeight;
        }
    }

    private void ScaleToScreen()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr == null || sr.sprite == null) return;

        float screenHeight = mainCam.orthographicSize * 2f;
        float screenWidth = screenHeight * mainCam.aspect;

        Vector2 spriteSize = sr.bounds.size;
        Vector3 newScale = transform.localScale;

        newScale.x = screenWidth / spriteSize.x;
        newScale.y = screenHeight / spriteSize.y;

        transform.localScale = newScale;
    }
}
