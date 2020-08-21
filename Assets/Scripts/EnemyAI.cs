using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public enum State
{
    Patrol,
    AfterPlayer,
    Attacking
}

public class EnemyAI : HitAwareness
{
    public Transform player;
    public float fieldOfView = 100f;
    public float patrolSpeed = 1f;
    public float afterPlayerSpeed = 2f;
    public float discoverPlayerDist = 7;
    public float attackPlayerDist = 5;
    public float stopWalkDist = 3;

    [SerializeField] private LayerMask playerObstacleLayerMask = new LayerMask();
    public State state = State.Patrol;
    Patrol patrol;
    AIDestinationSetter destinationSetter;
    AIPath aiPath;
    WeaponAPI weapon;
    Animator animator;
    Rigidbody2D rb;

    void Awake()
    {
        this.weapon = this.GetComponent<WeaponAPI>();
        this.animator = this.GetComponent<Animator>();
        this.rb = this.GetComponent<Rigidbody2D>();
        this.patrol = this.GetComponent<Patrol>();
        this.destinationSetter = this.GetComponent<AIDestinationSetter>();
        this.aiPath = this.GetComponent<AIPath>();
    }

    void Start()
    {
        this.patrol.enabled = false;
        this.destinationSetter.enabled = false;

        this.patrol.targets = this.patrol.targets.OrderBy(x => Random.value).ToArray();
    }

    void FixedUpdate()
    {
        float distToPlayer = Vector2.Distance(this.transform.position, this.player.transform.position);
        bool canSeePlayer = this.CanSeePlayer();
        bool shouldAttack = (canSeePlayer) && (distToPlayer < this.attackPlayerDist);

        switch (this.state)
        {
            case State.Patrol:
                this.patrol.enabled = true;
                this.aiPath.maxSpeed = this.patrolSpeed;

                if ((distToPlayer < this.discoverPlayerDist) && (canSeePlayer))
                {
                    this.patrol.enabled = false;
                    this.state = State.AfterPlayer;
                }
                break;

            case State.AfterPlayer:
                this.destinationSetter.enabled = true;
                this.aiPath.maxSpeed = this.afterPlayerSpeed;

                if (shouldAttack)
                {
                    this.destinationSetter.enabled = false;
                    this.state = State.Attacking;
                }
                break;
            case State.Attacking:
                this.Attack();

                if (!shouldAttack)
                {
                    this.state = State.AfterPlayer;
                }

                break;
        }

        this.animator.SetFloat("Speed", this.rb.velocity.magnitude);
    }

    bool CanSeePlayer()
    {
        Vector2 dir = this.player.transform.position - this.transform.position;
        float angle = Vector2.Angle(this.transform.up, dir);

        if (angle > this.fieldOfView / 2)
        {
            return false;
        }
        

        RaycastHit2D raycast = Physics2D.Raycast(this.transform.position, dir, float.PositiveInfinity, this.playerObstacleLayerMask);

        if (raycast.collider == null)
        {
            return false;
        }

        return raycast.collider.gameObject == this.player.gameObject;
    }

    void Attack()
    {
        Vector3 dir = this.player.transform.position - this.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        this.weapon.Attack();
    }

    override public void NotifyHit()
    {
        if (this.state == State.Patrol)
        {
            this.patrol.enabled = false;
            this.state = State.AfterPlayer;
        }
    }
}
