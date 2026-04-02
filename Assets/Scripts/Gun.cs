using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform firePoint;
    public GunData gunData;
    public PlayerInput input;
    public AudioSource gunAudioPlayer;
    private LineRenderer bulletLineEffect;
    private float fireDistance = 50f;
    private float lastFireTime;
    public LayerMask noHitLayer;
    public ParticleSystem gunEffect;

    private Coroutine coShot;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        bulletLineEffect = GetComponent<LineRenderer>();
        bulletLineEffect.positionCount = 2;
        bulletLineEffect.enabled = false;
    }

    private void OnEnable()
    {
        lastFireTime = 0f;
    }
    public void Fire()
    {
        if (Time.time - lastFireTime >= gunData.delayShots)
        {
            lastFireTime = Time.time;
            Shoot();
        }
    }
    public void Shoot()
    {
        if (firePoint == null) return;
        Vector3 hitPosition = Vector3.zero;
        RaycastHit hit;
        if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, fireDistance,noHitLayer))
        {
            hitPosition = hit.point;

            var damageAble = hit.collider.GetComponentInParent<IDamageAble>();
            if (damageAble != null)
            {
                damageAble.OnDamage(gunData.damage, hit.point, hit.normal);
            }
        }
        else
        {
            hitPosition = firePoint.position + firePoint.forward * fireDistance;
        }
        if (coShot != null)
        {
            StopCoroutine(coShot);
            coShot = null;
        }

        coShot = StartCoroutine(CoShotEffect(hitPosition));  
    }
    private IEnumerator CoShotEffect(Vector3 hitPosition)
    {
        gunEffect.Play();

        gunAudioPlayer.PlayOneShot(gunData.shotClip);

        bulletLineEffect.SetPosition(0, firePoint.position);
        bulletLineEffect.SetPosition(1, hitPosition);
        bulletLineEffect.enabled = true;

        yield return new WaitForSeconds(0.05f);

        bulletLineEffect.enabled = false;
    }

}
