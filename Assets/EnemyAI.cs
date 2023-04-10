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

    private int currentWaypoint = 0;
    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        Char = GetComponent<Character>();

        if (target == null) {
            Debug.LogError("No Target found");
            return;
        }
        seeker.StartPath(transform.position, target.position, OnPathComplete);

        StartCoroutine(UpdatePath());
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
        if (currentWaypoint + stopDistance >= path.vectorPath.Count) {
            Char.MoveDirection = new Vector2(0, 0);
            if (pathIsEnded) {
                return;
            }
            Debug.Log("End of path reached.");
            pathIsEnded = true;
            return;
        }
        pathIsEnded = false;

        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        Char.MoveDirection = new Vector2(dir.x,dir.y);
        Char.AimPos = new Vector2(target.position.x, target.position.y);

        float dist = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
        
        if (dist < nextWaypointDistance)
        {
            currentWaypoint++;
            return;
        }

    }

}
