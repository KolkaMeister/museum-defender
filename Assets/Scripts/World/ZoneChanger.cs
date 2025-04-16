using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;

public class ZoneChanger : MonoBehaviour
{
    [SerializeField] private string sceneName;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerInputController _))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
