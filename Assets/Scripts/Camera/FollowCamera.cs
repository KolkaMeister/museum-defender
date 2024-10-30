using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private GameObject _target;

    private void Awake()
    {
        _target = FindObjectOfType<PlayerInputController>().gameObject;
    }

    private void Update()
    {
        if (_target)
            transform.position = new Vector3(_target.transform.position.x, _target.transform.position.y, transform.position.z);
    }
}