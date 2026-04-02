using UnityEngine;

[CreateAssetMenu(fileName = "ZombieData", menuName = "Scriptable Objects/ZombieData")]
public class ZombieData : ScriptableObject
{
    public float health = 100f;
    public float moveSpeed = 2f;
    public float attackDamage = 10f;
    public Color skinColor = Color.white;
}
