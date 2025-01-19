using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Movement Settings")]
    [SerializeField] public float JumpForce;
    private const int MAX_JUMP_COUNT = 3;
    private const float GRAVITY_SCALE = 3f;
    [SerializeField] public bool isGodMode = false;

    [Header("Components")]
    [SerializeField] private Rigidbody2D PlayerRigidBody;
    [SerializeField] private Animator PlayerAnimator;
    [SerializeField] private BoxCollider2D PlayerCollider;
    private int JumpCount = 0;
    void Start()
    {
        PlayerRigidBody.gravityScale = GRAVITY_SCALE;
        JumpForce = 14f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && JumpCount < MAX_JUMP_COUNT)
        {
            Jump();
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.name == "Platform")
        {
            JumpCount = 0;
            PlayerAnimator.SetInteger("JumpCountState", 0);
        }
    }

    private void Jump()
    {
        JumpCount++;
        PlayerRigidBody.AddForceY(JumpForce, ForceMode2D.Impulse);
        PlayerAnimator.SetInteger("JumpCountState", JumpCount);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            if (isGodMode)
            {
                return;
            }

            Hit(collider.gameObject);
            return;
        }

        if (collider.gameObject.tag == "Food")
        {
            Heal(collider.gameObject);
        }
        if (collider.gameObject.tag == "GoldenFood")
        {
            GodMode(collider.gameObject);
        }
    }

    private void Hit(GameObject enemy)
    {
        if (GameManager.Instance.lives > 0)
        {
            GameManager.Instance.lives--;
            Destroy(enemy);
        }

        if (GameManager.Instance.lives == 0)
        {
            KillPlayer();
        }
    }

    private void Heal(GameObject food)
    {
        if (GameManager.Instance.lives < GameManager.MAX_LIVES)
        {
            GameManager.Instance.lives++;
            Destroy(food);
        }
    }

    private void GodMode(GameObject goldenFood)
    {
        isGodMode = true;
        Destroy(goldenFood);
        Invoke("EndGodMode", 5f);
    }

    private void EndGodMode()
    {
        isGodMode = false;
    }

    public void KillPlayer()
    {
        PlayerCollider.enabled = false;
        PlayerAnimator.enabled = false;
        Jump();
    }
}
