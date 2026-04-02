using UnityEngine;

public class BulletPowerUpItem : MonoBehaviour
{
    public float bonusDamage = 5f;
    public void OnTriggerEnter(Collider other)
    {
        Gun gun = other.GetComponentInChildren<Gun>();

        if (gun != null)
        {
            gun.UpDamage(bonusDamage);
            Debug.Log("데미지 상승!");
            Destroy(gameObject);
        }
    }
}
