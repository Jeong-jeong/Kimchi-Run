using UnityEngine;

public class Mover : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] public float Speed;

    void Start()
    {
        Speed = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * Time.deltaTime * Speed;
    }
}
