using System.Collections;
using UnityEngine;

public class MoveFoward : MonoBehaviour
{
    [SerializeField, Range(1, 20)]
    private float m_speed = 1f;

    [SerializeField]
    private float totalAnimationDuration = 3f;

    private Vector3 originalScale;
    private Vector3 targetScale;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private Color targetColor;

    private bool isAnimating = false;

    private void Start()
    {
        originalScale = transform.localScale;
        targetScale = originalScale * 0.5f;

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
            targetColor = new Color(
                originalColor.r * 0.5f,
                originalColor.g * 0.5f,
                originalColor.b * 0.5f,
                originalColor.a * 0.7f
            );
        }
        else
        {
            Debug.LogWarning("No SpriteRenderer found on this object. Color change will not work.");
        }
    }

    private void Update()
    {
        transform.Translate(Vector2.up * m_speed * Time.deltaTime, Space.Self);

        if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) && !isAnimating)
        {
            Debug.Log("Pirueta");
            StartCoroutine(ShiftAnimationSequence());
        }
    }

    private IEnumerator ShiftAnimationSequence()
    {
        isAnimating = true;
        float halfDuration = totalAnimationDuration / 2f;
        float t = 0f;

        // Escalar y oscurecer
        while (t < halfDuration)
        {
            float progress = t / halfDuration;
            Vector3 newScale = Vector3.Lerp(originalScale, targetScale, progress);
            transform.localScale = newScale;

            if (spriteRenderer != null)
            {
                Color newColor = Color.Lerp(originalColor, targetColor, progress);
                spriteRenderer.color = newColor;
            }

            t += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;
        if (spriteRenderer != null)
            spriteRenderer.color = targetColor;

        // Volver a normal
        t = 0f;
        while (t < halfDuration)
        {
            float progress = t / halfDuration;
            Vector3 newScale = Vector3.Lerp(targetScale, originalScale, progress);
            transform.localScale = newScale;

            if (spriteRenderer != null)
            {
                Color newColor = Color.Lerp(targetColor, originalColor, progress);
                spriteRenderer.color = newColor;
            }

            t += Time.deltaTime;
            yield return null;
        }

        transform.localScale = originalScale;
        if (spriteRenderer != null)
            spriteRenderer.color = originalColor;

        isAnimating = false;
    }
}
