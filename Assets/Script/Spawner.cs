using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Spawner Settings")]
    public GameObject[] prefabs;
    void Start()
    {
        InvokeSpawn();
    }

    void Spawn() {
        CreateObject();
        InvokeSpawn();
    }

    void CreateObject()
    {
        GameObject randomObject = prefabs[Random.Range(0, prefabs.Length)];
        Instantiate(randomObject, transform.position, Quaternion.identity);
    }

    void InvokeSpawn()
    {
        Invoke("Spawn", 5f);
    }
}
