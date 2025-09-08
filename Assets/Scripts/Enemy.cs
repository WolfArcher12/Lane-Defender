using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameManager gameManager;
    Rigidbody2D rigidbody;
    BoxCollider2D boxCollider2D;
    Animator animator;


    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float health = 1f;
    [SerializeField] float pointValue = 100f;

    [SerializeField] GameObject hitVFX;

    [SerializeField] AudioClip hitSFX;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] AudioClip lifeLostSFX;

    bool isHit = false;
    float lives;


    void Start()
    {
        GameObject GameManager = GameObject.Find("GameManager");
        if (GameManager != null)
        {
            gameManager = GameManager.GetComponent<GameManager>();
        }
        rigidbody = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isHit == true) return;
        Vector2 enemyVelocity = new Vector2(-moveSpeed, rigidbody.linearVelocity.y);
        rigidbody.linearVelocity = enemyVelocity;
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            health -= 1f;
            animator.SetBool("isHit", true);
            isHit = true;
            rigidbody.linearVelocity = Vector2.zero;
            Instantiate(hitVFX, transform.position, transform.rotation);
            StartCoroutine(ResetHit());
            AudioSource.PlayClipAtPoint(hitSFX, Camera.main.transform.position);
            if (health <= 0f)
            {
                AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position);
                Destroy(gameObject);
                gameManager.score += pointValue;
            }
        }
        if (other.gameObject.CompareTag("Enemy Goal"))
        {
            lives-= 1f;
            gameManager.lives = lives;
            AudioSource.PlayClipAtPoint(lifeLostSFX, Camera.main.transform.position);
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Player"))
        {
            lives -= 1f;
            gameManager.lives = lives;
            AudioSource.PlayClipAtPoint(lifeLostSFX, Camera.main.transform.position);
            Destroy(gameObject);

        }
    }

    IEnumerator ResetHit()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("isHit", false);
        isHit = false;
    }
}
