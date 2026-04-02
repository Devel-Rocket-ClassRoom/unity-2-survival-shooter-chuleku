using UnityEngine;

public interface IDamageAble
{
    void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal);
}
