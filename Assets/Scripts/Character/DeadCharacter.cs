using UnityEngine;

public class DeadCharacter : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;

    public void SetImpulse(Vector3 impulse)
    {
        _rb.AddForce(impulse, ForceMode2D.Impulse);
    }
}