using UnityEngine;

public class MoveSpeedUpItem : MonoBehaviour
{

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerMovement playerMove = other.GetComponentInChildren<PlayerMovement>();
            playerMove.MoveSpeedUp(10f);
    
            Debug.Log("넌 강해졌다 돌격해!");
            Destroy(gameObject);
        }
    }
}
