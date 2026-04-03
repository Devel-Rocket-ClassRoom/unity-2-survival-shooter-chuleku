using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : LivingEntity
{
    public enum Status
    {
        Idle,
        Trace,
        Attack,
        Die,
    }
    public AudioClip deathClip;
    public AudioClip hitClip;
    private AudioSource audioSource;
    public Transform target;
    public HitBox hitBox;
    public LayerMask targetLayer;
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    public Renderer zombieRenderer;
    public ParticleSystem hitEffect;
    public ZombieData zombieData;
    public int score;
    public static readonly int HashMove = Animator.StringToHash("Target");

    public float attackDamage = 10f;
    public float moveSpeed = 2f;
    public float attackDilay = 1f;
    public float attackedTime;
    public float traceDistance = 10f;

    public bool isAttacked = false;
    public bool isDieed = false;

    private Status currentStatus;
    public Status CurrentStatus
    {
        get { return currentStatus; }
        set
        {
            var prevStatus = currentStatus;
            currentStatus = value;

            switch (currentStatus)
            {
                case Status.Idle:
                    navMeshAgent.isStopped = true;
                    break;
                case Status.Trace:
                    navMeshAgent.isStopped = false;
                    break;
                case Status.Attack:
                    navMeshAgent.isStopped = true;
                    break;
                case Status.Die:
                    if (prevStatus != Status.Die)
                    {
                        UpdateDie();
                    }
                    break;
            }


        }
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
    }
    protected override void OnEnable()
    {
        base.OnEnable();

        currentStatus = Status.Idle;
        navMeshAgent.enabled = true;
        navMeshAgent.isStopped = false;
        navMeshAgent.ResetPath();

        if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 10f, NavMesh.AllAreas))
        {
            navMeshAgent.Warp(hit.position);
        }

        attackedTime = 0f;
    }
    private void Update()
    {
        switch (CurrentStatus)
        {
            case Status.Idle:
                UpdateIdle();
                break;
            case Status.Trace:
                UpdateTrace();
                break;
            case Status.Attack:
                UpdateAttack();
                break;
            case Status.Die:
                UpdateDie();
                break;
        }
        if(isDead)
        {
            StartSinking(Time.deltaTime);
        }
    }

    private void UpdateIdle()
    {
        if (target == null) return;
        if (Vector3.Distance(target.position, transform.position) <= traceDistance)
        {
            CurrentStatus = Status.Trace;
        }
    }

    private void UpdateTrace()
    {
        var distance = Vector3.Distance(target.position, transform.position) > traceDistance;
        target = FindTarget(traceDistance);
        if (target == null || distance)
        {
            target = null;
            CurrentStatus = Status.Idle;
            animator.SetBool(HashMove, false);
            return;
        }
        navMeshAgent.SetDestination(target.position);
        animator.SetBool(HashMove, true);
        if (target != null)
        {
            var find = hitBox.Colliders.Find(x => x.transform == target);
            if (find != null)
            {
                CurrentStatus = Status.Attack;
                animator.SetBool(HashMove, false);
                return;
            }
        }

    }

    private void UpdateAttack()
    {
        if (target == null)
        {
            CurrentStatus = Status.Idle;
            return;
        }
        var find = hitBox.Colliders.Find(x => x.transform == target);
        if (find == null && target != null)
        {

            CurrentStatus = Status.Trace;
            return;
        }
        var damageable = target.GetComponent<LivingEntity>();
        if (damageable != null && damageable.isDead)
        {
            target = null;
            CurrentStatus = Status.Idle;
            return;
        }

        var lookAt = target.position;
        lookAt.y = transform.position.y;
        transform.LookAt(lookAt);
        if (Time.time > attackedTime + attackDilay)
        {
            attackedTime = Time.time;
            if (damageable != null)
            {
                if (damageable.health > 0)
                {

                    damageable.OnDamage(attackDamage, transform.position, -transform.forward);

                }
            }

        }
    }

    private void UpdateDie()
    {
        if (isDieed) return;

        isDieed = true;
        navMeshAgent.isStopped = true;
        navMeshAgent.enabled = false;
        audioSource.PlayOneShot(deathClip);
        hitBox.Colliders.Clear();
        hitBox.gameObject.SetActive(false);
        animator.SetTrigger("Die");
        var col = GetComponent<Collider>();
        if (col != null) col.enabled = false;
        UiManager.instance.SetScoreText(score);
        Destroy(gameObject, 3f);
    }
    private Transform FindTarget(float radius)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, targetLayer);
        if (colliders.Length == 0)
            return null;

        var target = colliders.OrderBy(x => Vector3.Distance(x.transform.position, transform.position)).First();
        return target.transform;
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        if (isDead) return;
        base.OnDamage(damage, hitPoint, hitNormal);;
        hitEffect.transform.position = hitPoint;
        hitEffect.transform.forward = hitNormal;
        hitEffect.Play();
        audioSource.PlayOneShot(hitClip);
    }

    public override void Die()
    {
        base.Die();
        DropItem();
        CurrentStatus = Status.Die;
    }

    public void SetTarget(Transform newtarget)
    {
        target = newtarget;
    }
    public void StartSinking(float time)
    {
        transform.up *= -time;
    }

    public void DropItem()
    {
        var percentageItem = Random.Range(0, 100);
        var pos = transform.position;
        var randomItem = Random.Range(0,zombieData.items.Length);
        if(percentageItem <30)
        {
            Instantiate(zombieData.items[randomItem],pos,Quaternion.identity);
        }
    }
}
