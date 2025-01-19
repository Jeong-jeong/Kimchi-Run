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
        float gameSpeed = GameManager.Instance.CalculateGameSpeed() / 5;
        meshRenderer.material.mainTextureOffset += new Vector2(scrollSpeed * Time.deltaTime * gameSpeed, 0);
    }
}
