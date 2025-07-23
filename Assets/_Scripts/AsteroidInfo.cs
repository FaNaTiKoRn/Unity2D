using UnityEngine;

public enum SoundType
{
    ShotCommon,
    ShotFire,
    ShotIce,
    ShotHole
}

public class AsteroidInfo : MonoBehaviour
{
    public int points = 1;
    public AudioClip impactSound;
}
