using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    private float playerSpeed;
    [SerializeField]
    private float jumpHeight;
    [SerializeField]
    private int maxJumpNo;

    private int jumpsLeft;
    private Vector2 playerVelocity;

    private Animator playerAnimator;
    private float movementSpeed = 0;
    private bool isJumping = false;
    private bool facingRight = true;

    // Use this for initialization
    void Start()
    {
        jumpsLeft = maxJumpNo;
        playerVelocity = GetComponent<Rigidbody2D>().velocity;
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        playerVelocity = GetComponent<Rigidbody2D>().velocity;
        movementSpeed = 0;
        if (playerVelocity.y == 0)
        {
            jumpsLeft = maxJumpNo;
            isJumping = false;
        }

        if (Input.GetButtonDown("Jump") && jumpsLeft > 0)
        {
            playerVelocity.y = jumpHeight * jumpsLeft;
            jumpsLeft--;
            isJumping = true;
        }
        if (Input.GetAxis("Horizontal") != 0)
        {
            playerVelocity.x = playerSpeed * Input.GetAxis("Horizontal");
            movementSpeed = Mathf.Abs(Input.GetAxis("Horizontal"));
            if (Input.GetAxis("Horizontal") > 0 && !facingRight)
            {
                Flip();
            }
            else if (Input.GetAxis("Horizontal") < 0 && facingRight)
            {
                Flip();
            }
        }

        GetComponent<Rigidbody2D>().velocity = playerVelocity;
        playerAnimator.SetBool("Jumping", isJumping);
        playerAnimator.SetFloat("Speed", movementSpeed);
        if (Input.GetButtonDown("Fire1"))
        {
            FireOneShot();
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void FireOneShot()
    {
        var direction = transform.TransformDirection(Vector2.right);
        Rigidbody2D pc = GetComponent<Rigidbody2D>();
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition) - pc.transform.position;
        Debug.Log("Detect Stuff: " + pc.transform.position + " / " + mouse);
        RaycastHit2D hit = Physics2D.Raycast(pc.transform.position, mouse, 5);
        var damage = 10;
        Debug.DrawLine(pc.transform.position, mouse, Color.red);
        if (hit.collider != null)
        {
            Debug.DrawLine(pc.transform.position, hit.point, Color.cyan);
            Debug.Log("Detect Stuff: " + hit.collider.name);
            // - send damage to object we hit - \\
            hit.collider.SendMessageUpwards("ApplyDamage", damage, SendMessageOptions.DontRequireReceiver);
        }
    }
}