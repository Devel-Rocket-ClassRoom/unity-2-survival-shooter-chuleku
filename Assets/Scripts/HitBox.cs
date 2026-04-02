using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    private List<Collider> hitColliders = new List<Collider>();

    public List<Collider> Colliders
    {
        get { return hitColliders; }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hitColliders.Contains(other))
        {
            hitColliders.Add(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (hitColliders.Contains(other))
        {
            hitColliders.Remove(other);
        }
    }
}
