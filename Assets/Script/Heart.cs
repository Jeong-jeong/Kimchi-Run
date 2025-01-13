using UnityEngine;

public class Heart : MonoBehaviour
{
    public Sprite OnHeart;
    public Sprite OffHeart;
    public SpriteRenderer SpriteRenderer;
    public int LiveNumber;

    void Start()
    {
        
    }

    void Update()
    {
        if (GameManager.Instance.lives >= LiveNumber)
        {
            SpriteRenderer.sprite = OnHeart;
            return;
        } else
        {
            SpriteRenderer.sprite = OffHeart;
        }
    }
}
