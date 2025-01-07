using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public float scrollSpeed;
    
    [Header("References")]
    public MeshRenderer meshRenderer;
    void Start()
    {
    }

    void Update()
    {
        meshRenderer.material.mainTextureOffset += new Vector2(scrollSpeed * Time.deltaTime, 0);
    }
}
