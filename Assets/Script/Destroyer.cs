using UnityEngine;

public class Destroyer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private const float DESTROYER_X_POSITION = -15f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < DESTROYER_X_POSITION)
        {
            Destroy(gameObject);
        }
    }
}
