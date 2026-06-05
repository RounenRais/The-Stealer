using UnityEngine;

public class PlayerMovement : MonoBehaviour
{   public Animator animator;
    public float moveSpeed = 5f;
    public Rigidbody2D rb;         // 2D olmalý
    Vector2 movement;
    public SpriteRenderer spriteRenderer;
    public Sprite upSprite;
    public Sprite downSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement != Vector2.zero)
        {
            Vector2 normalized = movement.normalized;
            animator.SetFloat("Last_X", normalized.x);
            animator.SetFloat("Last_Y", normalized.y);
            animator.SetFloat("Speed", movement.magnitude);
        }
        else
        {
            animator.SetFloat("Speed", 0); // idle'a geç ama Last_X/Y deðiþmez
        }

    }
    private void FixedUpdate()
    {
        Vector2 konum = movement.normalized * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + konum);  
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {

            WorldEnemy worldEnemy = other.GetComponent<WorldEnemy>();

            if (worldEnemy == null || worldEnemy.isDefeated) return;

            GameManager.instance.EnterCombat(worldEnemy);
        }
    }
}