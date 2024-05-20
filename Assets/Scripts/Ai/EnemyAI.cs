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

    public bool stop = false;

    public bool pathIsEnded = false;
    public bool isWeaponed = false;
    [SerializeField] private bool _PVE = false;
    Bounds bounds;
    public bool PVE {
        get { 
            return _PVE;
        }
        set {
            _PVE = value;
            if (_PVE == true)
            {
                StartCoroutine(PveTimer());
            }
            else {
                StopCoroutine(PveTimer());
            }
        }
    }
    public bool Patroul;
    public float nextWaypointDistance = 1;
    public float stopDistance = 1;
    public float DistToTarget;
    public float pogr = 0.7f;

    public int rand = 0;

    public int currentWaypoint = 0;
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
        Ftarget = GetComponent<FindTarget>().enabled ? GetComponent<FindTarget>() : null;
        bounds = GetComponent<Collider2D>().bounds;
    }
    void Start()
    {
        Starter();
        StartCoroutine(Founder());
        PickWeapon();
        if(PVE) StopCoroutine(PveTimer());
        StartCoroutine(UpdatePathColider());
    }
    void PickWeapon() {
        if (isWeaponed == false)
        {
            //Debug.Log("PickWeapon");
            Char.Interact(InteractionType.Weapon);
            if (transform.Find("HoldPoint").transform.childCount != 0) {
                isWeaponed = true;
                target = null;
                stopDistance = transform.Find("HoldPoint").transform.GetChild(0).GetComponent<Weapon>()._shootRange;
            }
        }
    }
    IEnumerator UpdatePath()
    {
        if (target == null) {
            Char.MoveDirection = new Vector2(0, 0);
            if (Ftarget !=null) {
                Ftarget.FindTargets();
            }
            yield return false;
        }
        if (Ftarget !=null && !Ftarget.CheckTag()) {
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
            if (Ftarget != null) {
                Ftarget.FindTargets();
            }
            Char.MoveDirection = new Vector2(0, 0);
        }
        PickWeapon();
        yield return new WaitForSeconds(1f / updateRate);
        StartCoroutine(UpdatePath());
    }
    IEnumerator UpdatePathColider() {
        
        var guo = new GraphUpdateObject(bounds);
        // Set some settings
        guo.updatePhysics = true;
        AstarPath.active.UpdateGraphs(guo);
        bounds = GetComponent<Collider2D>().bounds;
        bounds.size += new Vector3(1,1,0);
        guo = new GraphUpdateObject(bounds);
        // Set some settings
        guo.updatePhysics = true;
        AstarPath.active.UpdateGraphs(guo);
        yield return new WaitForSeconds(0.5f / updateRate);
        StartCoroutine(UpdatePathColider());
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
        try
        {
            DistToTarget = Vector3.Distance(target.GetComponent<Collider2D>().ClosestPoint(transform.position), transform.position);
            if (DistToTarget == 0) {
                DistToTarget = Vector3.Distance(target.transform.position, transform.position);
            }
        }
        catch {
            DistToTarget = Vector3.Distance(target.transform.position, transform.position);
        }
        if (currentWaypoint >= path.vectorPath.Count)
        {
            Char.MoveDirection = new Vector2(0, 0);
            if (pathIsEnded)
            {
                return;
            }
            pathIsEnded = true;
            if (Patroul)
            {
                Debug.Log("Patroul Go" + this.name);
                Ftarget.FindTargets();
            }
            return;
        }
        pathIsEnded = false;

        if ((DistToTarget >= stopDistance + pogr) && !stop)
        {
            dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
            Char.MoveDirection = new Vector2(dir.x, dir.y);
        }
        else if ((DistToTarget <= stopDistance - pogr) && !stop)
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
        rand = Random.Range(7, 28);
        Char.Interact(InteractionType.Dialog);
        yield return new WaitForSeconds(rand);
        Ftarget.FindTargets();
        StartCoroutine(PveTimer());
    }
    void FixedUpdate()
    {
        MoveControl();
        if (!PVE && !stop) FireControl();
        else
        {
            //PVE Controll
        };
    }

}
