using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumpking : MonoBehaviour
{
    [Header("Ground Checks")]
    public bool isGrounded;
    public float checkRadius = 0.2f;
    public Transform groundCheck;
    public LayerMask groundMask;

    [Header("Mat")]
    public PhysicsMaterial2D bounceMat, normalMat;

    [Header("Attritubes")]
    public float activeMoveSpeed;
    public float jumpYMultiplier;
    public float jumpXMultiplier;

    public float maxXStrenth, maxYStrenth;
    
    public float horizontalBounceForce = 10.0f;
    public float verticalBounceForce = 10.0f;
    public string bounceTag = "Thorn";
    private float jumpStrength;
    private float jumpX;
    private int dir = 1;
    private bool isJumping = false;

    private Rigidbody2D rb;
    private Animator anim;

    

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        dir = 1;
    }

    // Update is called once per frame
    void Update()
    {   
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            checkRadius,
            groundMask
        );
        
        // ChangeMat();

        if(isGrounded)
        {
            float input = Input.GetAxisRaw("Horizontal");

            anim.SetFloat("walk",Mathf.Abs(input));
            
            if(input > 0 ) dir = 1;
            else if(input < 0) dir = -1;

            transform.localScale = new Vector3(dir, 1f, 1f);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                anim.SetTrigger("squat");
            }


            if (Input.GetKey(KeyCode.Space))
            {
                rb.velocity = new Vector3(0f, 0f, 0f);
                jumpStrength += (Time.deltaTime * jumpYMultiplier * 2);
                jumpX += (Time.deltaTime * jumpXMultiplier * 2);
            }
            else if (Input.GetButtonUp("Jump"))
            {
                anim.SetTrigger("jump");
                isJumping = true;

                if (jumpStrength > maxYStrenth)
                    jumpStrength = maxYStrenth;
                if (jumpStrength < 100)
                    jumpStrength = 100;
                if (jumpX > maxXStrenth)
                    jumpX = maxXStrenth;
                
                rb.AddForce(
                    new Vector2(dir*jumpX, jumpStrength), 
                    ForceMode2D.Impulse
                );
                // rb.AddForce(Vector2.up * jumpStrength, ForceMode2D.Impulse);
                
                Debug.Log("X velocity : " + rb.velocity.x);
                Debug.Log(jumpStrength);
                
                jumpX = 0f;
                jumpStrength = 0f;
            }
            else if(isJumping == false)
            {
                rb.velocity = new Vector2(activeMoveSpeed * input,rb.velocity.y);
            }

            if(rb.velocity.y <= 0 && isJumping)
                isJumping = false;
        }else
        {
            if(rb.velocity.y < 0)
            {
                anim.SetTrigger("fall");
            }
            
        }
    }

    public void ChangeMat()
    {
        if(isGrounded)
        {
            rb.sharedMaterial = normalMat;
        }
        else
        {   
            rb.sharedMaterial = bounceMat;
        }
    }

    void OnDrawGizmoSelected(){
        Gizmos.color = Color.green;
        Gizmos.DrawCube(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.5f), new Vector2(0.9f, 0.2f));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(bounceTag))
        {
            Vector2 collisionObjectDirection = other.transform.position - transform.position;
            Vector2 horizontalBounceDirection = -collisionObjectDirection.normalized;
            horizontalBounceDirection.y = 0; // Remove vertical component
            Vector2 verticalBounceDirection = Vector2.up;

            Vector2 finalBounceDirection = (horizontalBounceDirection * horizontalBounceForce) + (verticalBounceDirection * verticalBounceForce);
            rb.AddForce(finalBounceDirection, ForceMode2D.Impulse);
        }
    }

    

    
}
