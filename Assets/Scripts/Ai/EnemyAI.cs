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
    FindTarget Ftarget;

    public Path path;

    public bool pathIsEnded = false;
    public bool isWeaponed = false;
    public bool PVE = false;
    public float nextWaypointDistance = 1;
    public float stopDistance = 1;
    public float DistToTarget;
    public float pogr = 0.7f;

    private int currentWaypoint = 0;
    private Vector3 dir;
    // Start is called before the first frame update
    IEnumerator Founder() {
    
        if (target == null)
        {
            // Debug.Log("No Target found");
            yield return new WaitForSeconds(1f / updateRate);
            StartCoroutine(Founder());
        }
        try
        {
            seeker.StartPath(transform.position, target.position, OnPathComplete);
        }
        catch {
            // Debug.Log("Цель пропала");
        }
            
        StartCoroutine(UpdatePath());
    }
    void Starter() {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        Char = GetComponent<Character>();
        Ftarget = Char.GetComponent<FindTarget>();
    }
    void Start()
    {
        Starter();
        StartCoroutine(Founder());
        PickWeapon();
        if (PVE) StartCoroutine(PveTimer());
    }
    void PickWeapon() {
        if (isWeaponed == false)
        {
            //Debug.Log("PickWeapon");
            Char.Interact();
            if (transform.Find("HoldPoint").transform.childCount != 0) {
                isWeaponed = true;
                transform.Find("HoldPoint").transform.GetChild(0).tag = "Untagged";
                target = null;
                stopDistance = transform.Find("HoldPoint").transform.GetChild(0).GetComponent<Weapon>()._shootRange;
            }
        }
    }
    IEnumerator UpdatePath()
    {
        if (target == null) {
            Char.MoveDirection = new Vector2(0, 0);
            Ftarget.FindTargets();
            yield return false;
        }
        if (!GetComponent<FindTarget>().CheckTag()) {
            //Debug.Log(GetComponent<FindTarget>().CheckTag()) ;
            target = null;
            Ftarget.FindTargets();
            yield return false;
        }
        
        try
        {
            seeker.StartPath(transform.position, target.position, OnPathComplete);
        }
        catch {
            Ftarget.FindTargets();
            Char.MoveDirection = new Vector2(0, 0);
        }
        PickWeapon();
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
        DistToTarget = Vector3.Distance(transform.position, target.position);
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

        if (DistToTarget >= stopDistance + pogr)
        {
            dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
            Char.MoveDirection = new Vector2(dir.x, dir.y);
        }
        else if (DistToTarget <= stopDistance - pogr)
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
        if (DistToTarget <= stopDistance + 1 && target != null) {
            //Debug.Log("atk");
            Char.Attack();
        }
    }
    IEnumerator PveTimer()
    {
        yield return new WaitForSeconds(Random.Range(7, 13));
        Ftarget.FindTargets();
        StartCoroutine(PveTimer());
    }
        void FixedUpdate()
    {
        MoveControl();
        if (!PVE) FireControl();
        else
        {
            //PVE controll
        };
    }

}
