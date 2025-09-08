using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    Vector2 moveInput;
    GameManager gameManager;
    Rigidbody2D rigidbody;
    BoxCollider2D boxCollider2D;

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float shootInterval = 0.5f;

    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform projectileSpawn;
    [SerializeField] Transform explosionSpawn;

    [SerializeField] GameObject shootFX;

    [SerializeField] AudioClip shootSound;
    [SerializeField] AudioClip lifeLostSound;

    bool isFiring = false;
    float shootTimer = 0f;
    float lives;

    void Start()
    {
        GameObject GameManager = GameObject.Find("GameManager");
        gameManager = GameManager.GetComponent<GameManager>();
        rigidbody = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        Vector2 playerVelocity = new Vector2(rigidbody.linearVelocity.x, moveInput.y * moveSpeed);
        rigidbody.linearVelocity = playerVelocity;
        ProcessShooting();
        shootTimer += Time.deltaTime;
        lives = gameManager.lives;
        if (lives <= 0f)
            Destroy(gameObject);

    }

    private void ProcessShooting()
    {
        if (isFiring)
        {
            if (shootTimer < shootInterval) return;
            Instantiate(projectilePrefab, projectileSpawn.position, projectileSpawn.rotation);
            Instantiate(shootFX, explosionSpawn.position, projectileSpawn.rotation);
            AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position);
            shootTimer = 0f;
        }
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        
    }

    void OnFire(InputValue value)
    {
        isFiring = value.isPressed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            lives -= 1f;
            gameManager.lives = lives;
            AudioSource.PlayClipAtPoint(lifeLostSound, Camera.main.transform.position);
        }
    }
}
