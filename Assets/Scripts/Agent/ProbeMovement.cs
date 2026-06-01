using UnityEngine;

public class ProbeMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
