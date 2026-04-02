using UnityEngine;

public class HealItem : MonoBehaviour
{

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerHealth health = other.GetComponent<PlayerHealth>();
            health.Heal(20);
            Debug.Log("회복!");
            Destroy(gameObject);
        }
    }
}
