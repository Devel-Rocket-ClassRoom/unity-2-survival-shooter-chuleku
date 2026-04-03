using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : LivingEntity
{
 
    public AudioClip deathClip;
    public AudioClip hitClip;
    public Slider hpSlider;
    public UiManager uiManager;
    public float playerHealth = 1f;
    public float fillSpeed = 5f;
    private AudioSource playerAudioSource;
    private Animator playerAnimator;
    private PlayerMovement playerMovement;


    private void Awake()
    {
        playerAudioSource = GetComponent<AudioSource>();
        playerAnimator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        playerHealth = startingHealth;
        if (hpSlider != null) hpSlider.value = 1f;
    }

    private void Update()
    {
        float healthRatio = health / startingHealth;

        if (hpSlider != null)
        {
            hpSlider.value = Mathf.Lerp(hpSlider.value, healthRatio, Time.deltaTime * fillSpeed);
        }
    }
    protected override void OnEnable()
    {
        base.OnEnable();

        playerMovement.enabled = true;
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        if (isDead) return;
        base.OnDamage(damage, hitPoint, hitNormal);
        playerAudioSource.PlayOneShot(hitClip);
        Damage(damage);
        hpSlider.value = Mathf.Lerp(hpSlider.value, playerHealth, Time.deltaTime * fillSpeed);
    }

    public override void Die()
    {
        base.Die();
        playerAudioSource.PlayOneShot(deathClip);
        playerAnimator.SetTrigger("PlayerDie");
        playerMovement.enabled = false;
      
    }
    public void Damage(float damage)
    {
        if (isDead) return;

        playerHealth -= damage/100;
        playerHealth = Mathf.Clamp(playerHealth, 0f, 1f);
    }
    public void RestartLevel()
    {
        uiManager.SetActiveGameOverUi(true);
    }

}
