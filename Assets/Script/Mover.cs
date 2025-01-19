using UnityEngine;

public class Mover : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] public float Speed;

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * Time.deltaTime * GameManager.Instance.CalculateGameSpeed();
    }
}
