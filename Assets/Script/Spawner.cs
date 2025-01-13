using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Spawner Settings")]
    public float minSpawnDelay;
    public float maxSpawnDelay;

    
    [Header("Spawner References")]
    public GameObject[] prefabs;
    void OnEnable()
    {
        InvokeSpawn();
    }

    void OnDisable()
    {
        CancelInvoke();
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
        Invoke("Spawn", Random.Range(minSpawnDelay, maxSpawnDelay));
    }
}
