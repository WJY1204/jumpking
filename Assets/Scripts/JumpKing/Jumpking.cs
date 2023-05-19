using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumpking : MonoBehaviour
{
    [Header("Ground Checks")]
    public bool isGrounded;
    public bool isFronted;
    public float checkRadius = 0.2f;
    public Transform[] groundCheck;
    public Transform frontCheck;
    public LayerMask groundMask;

    [Header("Mat")]
    public PhysicsMaterial2D bounceMat, normalMat;

    [Header("Attritubes")]
    public float activeMoveSpeed;
    public float jumpYMultiplier;
    public float jumpXMultiplier;

    public float maxXStrenth, maxYStrenth;

    private int dir = 1;
    private float jumpStrength;
    private float jumpX;
    public bool isJumping = false;
    public bool isFalling = false;
    public bool isBouncing = false;

    public string respawnTag = "Respawn";
    public string bounceTag = "Thorn";
    public string bugTag = "Bug";
    public Vector2 spawnPonit = Vector2.zero;
    private float jumpAmount;

    private Rigidbody2D rb;
    private Animator anim;

    private Vector2 worldSpawnPoint;

    private bool isDelaying = false;
    private bool forceStopped = false;

    [SerializeField]private EnergyUI energyUI;

    private void Awake() 
    {
        worldSpawnPoint = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        dir = 1;
        isBouncing = false;
    }

    // Update is called once per frame
    void Update()
    {   
        if(forceStopped)
            return;

        if(energyUI.InTransition)
            return;

       isGrounded = Physics2D.OverlapCircle(
            groundCheck[0].position,
            checkRadius,
            groundMask
        ) || Physics2D.OverlapCircle(
            groundCheck[1].position,
            checkRadius,
            groundMask
        ) || Physics2D.OverlapCircle(
            groundCheck[2].position,
            checkRadius,
            groundMask
        ) ;

        isFronted = Physics2D.OverlapCircle(
            frontCheck.position,
            0.1f,
            groundMask
        );

        if(isGrounded)
        {
            float input = Input.GetAxisRaw("Horizontal");
            
            if(input > 0 ) dir = 1;
            else if(input < 0) dir = -1;

            transform.localScale = new Vector3(dir, 1f, 1f);

            if(Input.GetKeyDown(KeyCode.Space))
            {
                jumpAmount = 0;
                anim.SetTrigger("squat");
                rb.velocity = Vector2.zero;
            }
            else if (Input.GetKey(KeyCode.Space))
            {
                jumpStrength += (Time.deltaTime * jumpYMultiplier * 2);
                jumpX += (Time.deltaTime * jumpXMultiplier * 2);

                jumpAmount = jumpStrength;
                energyUI.UpdateEnergybar(jumpAmount/maxYStrenth) ;
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
                StartCoroutine(DelaySec());
            }
            else if(isJumping == false)
            {
                anim.SetFloat("walk",Mathf.Abs(input));
                rb.velocity = new Vector2(activeMoveSpeed * input,rb.velocity.y);
            }
        }
        else
        {
            if(rb.velocity.y < 0 && isFalling == false)
            {
                isFalling = true;
                anim.SetTrigger("fall");
            }   
        }
    }

    IEnumerator DelaySec()
    {
        isDelaying = true;
        yield return new WaitForSeconds(0.1f);
        isDelaying = false;
    }

    private void FixedUpdate() 
    {
        if(forceStopped)
            return;
            
        if(isDelaying)
            return;
        
        if(energyUI.InTransition)
            return;
        
        if(isJumping)
        {
            if(isFronted)
            {
                isBouncing = true;
                rb.AddForce(
                    new Vector2(-rb.velocity.x*(jumpX/2), rb.velocity.y),  
                    ForceMode2D.Impulse
                );
            }
            if(isGrounded)
            {
                isJumping = false;
                isBouncing = false;

                jumpStrength = 0;
                jumpX = 0;
                jumpAmount = 0;
                energyUI.UpdateEnergybar(0);
            }
        } 
        if(isGrounded)
        {
            isFalling = false;
            anim.SetTrigger("exit");
            jumpAmount = 0;
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
        if (other.gameObject.CompareTag("Finish"))
        {
            forceStopped = true;
        }
        if (other.gameObject.CompareTag(respawnTag))
        {
            spawnPonit = other.GetComponent<RespawnObject>().GetSpawnPoint;
        }
        if (other.gameObject.CompareTag(bounceTag))
        {
            energyUI.UpdateTransiton();
            rb.velocity = Vector2.zero;
            transform.position = spawnPonit;
        }
        if(other.gameObject.CompareTag(bugTag))
        {
            energyUI.UpdateTransiton();
            rb.velocity = Vector2.zero;
            transform.position = worldSpawnPoint;
        }
    }
}

