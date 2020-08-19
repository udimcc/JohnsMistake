using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Patrol,
    AfterPlayer,
    Attacking
}

public class EnemyAI : HitAwareness
{
    public GameObject[] patrolWaypointsObjects;
    public Transform player;
    public float fieldOfView = 100f;
    public float patrolSpeed = 100f;
    public float afterPlayerSpeed = 200f;
    public float discoverPlayerDist = 7;
    public float attackPlayerDist = 5;
    public float stopWalkDist = 3;

    [SerializeField] private LayerMask playerObstacleLayerMask = new LayerMask();
    State state = State.Patrol;
    Vector3[] patrolWaypoints;
    EnemyPathfinding enemyPathfinding;
    WeaponAPI weapon;
    Animator animator;
    Rigidbody2D rb;

    void Awake()
    {
        this.weapon = this.GetComponent<WeaponAPI>();
        this.enemyPathfinding = GetComponent<EnemyPathfinding>();
        this.animator = this.GetComponent<Animator>();
        this.rb = this.GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        patrolWaypoints = patrolWaypointsObjects.Select(x => x.transform.position).ToArray();
    }

    void FixedUpdate()
    {
        float distToPlayer = Vector2.Distance(this.transform.position, this.player.transform.position);
        bool canSeePlayer = this.CanSeePlayer();
        bool shouldAttack = (canSeePlayer) && (distToPlayer < this.attackPlayerDist);

        switch (this.state)
        {
            case State.Patrol:
                enemyPathfinding.SetPath(patrolWaypoints, patrolSpeed, 2);

                if ((distToPlayer < this.discoverPlayerDist) && (canSeePlayer))
                {
                    enemyPathfinding.Stop();
                    this.state = State.AfterPlayer;
                }
                break;

            case State.AfterPlayer:
                enemyPathfinding.GoTo(player.position, afterPlayerSpeed, this.stopWalkDist, !shouldAttack);

                if (shouldAttack)
                {
                    this.state = State.Attacking;
                }
                break;
            case State.Attacking:
                if (shouldAttack)
                {
                    this.Attack();
                }
                else
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
            enemyPathfinding.Stop();
            this.state = State.AfterPlayer;
        }
    }
}
