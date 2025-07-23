using UnityEngine;

public class InstructionHUD : MonoBehaviour
{
    [SerializeField] private GameObject hudPrefab; // Prefab a instanciar
    [SerializeField] private float displayTime = 5f;

    private GameObject hudInstance;

    private void Start()
    {
        if (hudPrefab != null)
        {
            hudInstance = Instantiate(hudPrefab, transform);
            hudInstance.SetActive(true);
            Invoke(nameof(HideHUD), displayTime);
        }
        else
        {
            Debug.LogWarning("⚠️ No se asignó el HUD (awds.prefab) en el Inspector.");
        }
    }

    private void HideHUD()
    {
        if (hudInstance != null)
            hudInstance.SetActive(false);
    }
}
