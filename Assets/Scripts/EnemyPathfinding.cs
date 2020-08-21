using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

delegate void OnPathReached();

public class EnemyPathfinding : MonoBehaviour
{
    float reachDistance;
    float speed;
    bool changeAngle;
    Path path;
    int currentWaypointIdx = 0;
    OnPathReached onPathReached;
    float lastPathSetTime = 0;

    Seeker seeker;
    AIDestinationSetter destinationSetter;
    Rigidbody2D rb;

    Vector3[] lastSetPathTargets;


    public void GoTo(Vector3 target, float speed, float reachDistance, bool changeAngle)
    {
        this.changeAngle = changeAngle;

        if ((Time.time - this.lastPathSetTime < 0.5f) || 
            (Vector2.Distance(this.rb.position, target) < reachDistance))
        {
            return;
        }

        this.speed = speed;
        this.reachDistance = reachDistance;
        this.lastPathSetTime = Time.time;

        this.seeker.StartPath(this.rb.position, target, OnPathFound);

        this.onPathReached = () => 
        {
            this.Stop();
        };
    }

    public void SetPath(Vector3[] targets, float speed, float reachDistance)
    {
        if ((targets.Length == 0) || (this.lastSetPathTargets == targets) || (Time.time - this.lastPathSetTime < 0.5f))
        {
            return;
        }

        this.lastSetPathTargets = targets;
        this.speed = speed;
        this.reachDistance = reachDistance;
        this.lastPathSetTime = Time.time;
        this.changeAngle = true;

        this.onPathReached = () => 
        {
            this.Stop();
            Vector3 target = Vector3.zero;

            do
            {
                target = targets[Random.Range(0, targets.Length)];
            }
            while ((path != null) && (path.vectorPath.Last() == target));

            this.seeker.StartPath(this.rb.position, target, OnPathFound);
        };

        this.onPathReached();
    }

    public void Stop()
    {
        this.path = null;
        this.rb.velocity = Vector2.zero;
    }

    void Start()
    {
        this.seeker = GetComponent<Seeker>();
        this.rb = GetComponent<Rigidbody2D>();
        this.destinationSetter = this.GetComponent<AIDestinationSetter>();
    }

    void FixedUpdate()
    {
        if ((path != null) && (currentWaypointIdx < path.vectorPath.Count))
        {
            Vector2 nextWaypoint = path.vectorPath[currentWaypointIdx];
            float targetDistance = Vector2.Distance(this.rb.position, this.path.vectorPath.Last());
            float nextWaypointDistance = Vector2.Distance(this.rb.position, nextWaypoint);
            bool isLastWaypoint = currentWaypointIdx == path.vectorPath.Count - 1;

            if (!isLastWaypoint)
            {
                if (nextWaypointDistance < 2)
                {
                    currentWaypointIdx++;
                }
            }
            else if (targetDistance < this.reachDistance)
            {
                this.onPathReached();
                return;
            }

            this.GoToWaypoint(nextWaypoint);

            if ((this.changeAngle) && (rb.velocity.magnitude > 0))
            {
                float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg - 90f;
                rb.rotation = angle;
            }
        }
    }

    void OnPathFound(Path p)
    {
        if (!p.error)
        {
            this.path = p;
            this.currentWaypointIdx = 0;
        }
    }

    void GoToWaypoint(Vector2 waypoint)
    {
       // this.destinationSetter.target = waypoint;
        Vector2 direction = (waypoint - this.rb.position).normalized;
        this.rb.AddForce(direction * speed * Time.deltaTime);
    }
}
