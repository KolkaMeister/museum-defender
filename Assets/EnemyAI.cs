using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof (Rigidbody2D))]
[RequireComponent(typeof (Seeker))]

public class EnemyAI : MonoBehaviour
{
    public Transform target;

    public float updateRate = 1f;
    private Seeker seeker;
    private Rigidbody2D rb;
    private Character Char;

    public Path path;

    public bool pathIsEnded = false;

    public float nextWaypointDistance = 1;
    public float stopDistance = 1;
    public float pogr = 0.5f;

    private int currentWaypoint = 0;
    private Vector3 dir;
    // Start is called before the first frame update
    IEnumerator Starter() {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        Char = GetComponent<Character>();

        if (target == null)
        {
            Debug.Log("No Target found");
            yield return new WaitForSeconds(1f / updateRate);
            StartCoroutine(Starter());
        }
        seeker.StartPath(transform.position, target.position, OnPathComplete);
        //Debug.Log(this);
        StartCoroutine(UpdatePath());
    }
    void Start()
    {
        StartCoroutine(Starter());
    }
    IEnumerator UpdatePath()
    {
        if (target == null) {
            yield return false;
        }
        seeker.StartPath(transform.position, target.position, OnPathComplete);
        yield return new WaitForSeconds(1f / updateRate);
        StartCoroutine(UpdatePath());
    }
    public void OnPathComplete(Path p) {
        //Debug.Log("We got a path. Error:? " + p.error);
        if (!p.error) {
            path = p;
            currentWaypoint = 0;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (target == null) {
            return;
        }
        if (path == null) {
            return;
        }
        float distStop = Vector3.Distance(transform.position, target.position);
        if (currentWaypoint >= path.vectorPath.Count)
        {
            Char.MoveDirection = new Vector2(0, 0);
            if (pathIsEnded)
            {
                return;
            }
            pathIsEnded = true;
            return;
        }
        pathIsEnded = false;

        if (distStop >= stopDistance+pogr )
        {
            dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
            Char.MoveDirection = new Vector2(dir.x, dir.y);
        }
        else if (distStop <= stopDistance-pogr)
        {
            dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
            Char.MoveDirection = new Vector2(-dir.x, -dir.y);
        }
        else {
            Char.MoveDirection = new Vector2(0, 0);
        }
        Char.AimPos = new Vector2(target.position.x, target.position.y);

        float dist = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
        
        if (dist < nextWaypointDistance)
        {
            currentWaypoint++;
            return;
        }

    }

}
