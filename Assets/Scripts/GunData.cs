using UnityEngine;

[CreateAssetMenu(fileName = "GunData", menuName = "Scriptable Objects/GunData")]
public class GunData : ScriptableObject
{
    public AudioClip shotClip;
    public float damage = 20f;
    public float delayShots = 0.15f;
    public float range = 100f;
}
