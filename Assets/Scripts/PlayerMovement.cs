using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveForce = 10f;

    [SerializeField]
    private float jumpForce = 11f;

    private float movementX;

    private Animator anim;
    private string walkAnimation = "Walk";
    
    private bool isGrounded = true;
    private string groundTag = "Ground";

    private SpriteRenderer sr;
    private Rigidbody2D myBody;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    /*void Start()
    {
        
    }*/

    // Update is called once per frame
    void Update()
    {
        movePlayerKeyboard();
        playerAnimation();
        playerJump();
    }

    void movePlayerKeyboard()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(movementX, 0f, 0f) * moveForce * Time.deltaTime;
    }

    void playerAnimation()
    {
        if(movementX > 0)
        {
            anim.SetBool(walkAnimation, true);
            sr.flipX = false;
        }
        else if(movementX < 0)
        {
            anim.SetBool(walkAnimation, true);
            sr.flipX = true;
        }
        else
        {
            anim.SetBool(walkAnimation, false);
        }
    }

    void playerJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isGrounded = false;
            myBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(groundTag))
        {
            isGrounded = true;
        }
    }
}
