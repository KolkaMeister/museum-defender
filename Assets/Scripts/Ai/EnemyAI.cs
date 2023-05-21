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
    public float rangeAIM = 100f;
    private Seeker seeker;
    private Rigidbody2D rb;
    private Character Char;

    public Path path;

    public bool pathIsEnded = false;
    public bool isWeaponed = false;

    public float nextWaypointDistance = 1;
    public float stopDistance = 1;
    public float distStop;
    public float pogr = 0.5f;

    private int currentWaypoint = 0;
    private Vector3 dir;
    // Start is called before the first frame update
    IEnumerator Founder() {
    
        if (target == null)
        {
            //Debug.Log("No Target found");
            yield return new WaitForSeconds(1f / updateRate);
            StartCoroutine(Founder());
        }
        try
        {
            seeker.StartPath(transform.position, target.position, OnPathComplete);
        }
        catch {
            Debug.Log("Цель пропала");
            target = null;
        }
            
        StartCoroutine(UpdatePath());
    }
    void Starter() {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        Char = GetComponent<Character>();
    }
    void Start()
    {
        Starter();
        StartCoroutine(Founder());
        if (isWeaponed == false)
        {
            Char.Interact();
        }
    }
    IEnumerator UpdatePath()
    {
        if (target == null) {
            Char.MoveDirection = new Vector2(0, 0);
            Char.GetComponent<FindTarget>().FindTargets();
            yield return false;
        }
        try
        {
            seeker.StartPath(transform.position, target.position, OnPathComplete);
        }
        catch {
            Char.GetComponent<FindTarget>().FindTargets();
            Char.MoveDirection = new Vector2(0, 0);
        }
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
    public void MoveControl() {
        if (target == null)
        {
            return;
        }
        if (path == null)
        {
            return;
        }
        distStop = Vector3.Distance(transform.position, target.position);
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

        if (distStop >= stopDistance + pogr)
        {
            dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
            Char.MoveDirection = new Vector2(dir.x, dir.y);
        }
        else if (distStop <= stopDistance - pogr)
        {
            dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
            Char.MoveDirection = new Vector2(-dir.x, -dir.y);
        }
        else
        {
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
    public void FireControl() {
        if (distStop <= stopDistance + 1 && target != null) {
            //Debug.Log("atk");
            //Char.AimPos = new Vector2(target.position.x + Random.Range(-rangeAIM, rangeAIM), target.position.y + Random.Range(-rangeAIM, rangeAIM));
            Char.Attack();
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        MoveControl();
        FireControl();
    }

}
