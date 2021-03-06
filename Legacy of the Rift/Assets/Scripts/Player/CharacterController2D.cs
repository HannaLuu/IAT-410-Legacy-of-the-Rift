using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    [SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.

    const float k_GroundedRadius = .05f; // Radius of the overlap circle to determine if grounded
    private bool m_Grounded;            // Whether or not the player is grounded.
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private Vector3 m_Velocity = Vector3.zero;

    public PlayerSwitching playerSwitchingScript;
    public LokirAttacks lokir;
    public HalvarAttacks halvar;
    public UrsaAttacks ursa;
    public float knockbackX, knockbackY, bigKnockX, bigKnockY, knockbackLength, knockbackCount;
    public bool knockFromRight, bigKnock;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    private void Awake()
    {
        playerSwitchingScript = FindObjectOfType<PlayerSwitching>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

    }

    private void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }
    }

    public void Move(float move, bool crouch, bool jump)
    {
        if (playerSwitchingScript.currHero == PlayerSwitching.Hero.Lokir)
        {
            lokir = GameObject.FindObjectOfType<LokirAttacks>();
            //only control the player if grounded or airControl is turned on
            if (m_Grounded || m_AirControl)
            {
                if (lokir.dontMove == false && knockbackCount <= 0)
                {
                    // Move the character by finding the target velocity
                    Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
                    // And then smoothing it out and applying it to the character
                    m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

                    // If the input is moving the player right and the player is facing left...
                    if (move > 0 && !m_FacingRight)
                    {
                        // ... flip the player.
                        Flip();
                    }
                    // Otherwise if the input is moving the player left and the player is facing right...
                    else if (move < 0 && m_FacingRight)
                    {
                        // ... flip the player.
                        Flip();
                    }
                }
                else if (knockbackCount > 0)
                {
                    if (!bigKnock)
                    {
                        if (knockFromRight)
                        {
                            m_Rigidbody2D.velocity = new Vector2(-knockbackX, knockbackY);
                        }
                        if (!knockFromRight)
                        {
                            m_Rigidbody2D.velocity = new Vector2(knockbackX, knockbackY);
                        }
                        knockbackCount -= Time.deltaTime;
                    }
                    if (bigKnock)
                    {
                        if (knockFromRight)
                        {
                            m_Rigidbody2D.velocity = new Vector2(-bigKnockX, bigKnockY);
                        }
                        if (!knockFromRight)
                        {
                            m_Rigidbody2D.velocity = new Vector2(bigKnockX, bigKnockY);
                        }
                        knockbackCount -= Time.deltaTime;
                    }
                }
            }

            // If the player should jump...
            if (m_Grounded && jump && m_Rigidbody2D.velocity.y <= 0f && lokir.dontMove == false)
            {
                // Add a vertical force to the player.
                m_Grounded = false;
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            }
        }

        if (playerSwitchingScript.currHero == PlayerSwitching.Hero.Halvar)
        {
            halvar = GameObject.FindObjectOfType<HalvarAttacks>();
            //only control the player if grounded or airControl is turned on
            if (m_Grounded || m_AirControl)
            {
                if (halvar.dontMove == false && knockbackCount <= 0)
                {
                    // Move the character by finding the target velocity
                    Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
                    // And then smoothing it out and applying it to the character
                    m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

                    // If the input is moving the player right and the player is facing left...
                    if (move > 0 && !m_FacingRight)
                    {
                        // ... flip the player.
                        Flip();
                    }
                    // Otherwise if the input is moving the player left and the player is facing right...
                    else if (move < 0 && m_FacingRight)
                    {
                        // ... flip the player.
                        Flip();
                    }
                }
                else if (knockbackCount > 0)
                {
                    if (!bigKnock)
                    {
                        if (knockFromRight)
                        {
                            m_Rigidbody2D.velocity = new Vector2(-knockbackX, knockbackY);
                        }
                        if (!knockFromRight)
                        {
                            m_Rigidbody2D.velocity = new Vector2(knockbackX, knockbackY);
                        }
                        knockbackCount -= Time.deltaTime;
                    }
                    if (bigKnock)
                    {
                        if (knockFromRight)
                        {
                            m_Rigidbody2D.velocity = new Vector2(-bigKnockX, bigKnockY);
                        }
                        if (!knockFromRight)
                        {
                            m_Rigidbody2D.velocity = new Vector2(bigKnockX, bigKnockY);
                        }
                        knockbackCount -= Time.deltaTime;
                    }
                }
            }

            // If the player should jump...
            if (m_Grounded && jump && m_Rigidbody2D.velocity.y <= 0f && halvar.dontMove == false)
            {
                // Add a vertical force to the player.
                m_Grounded = false;
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            }
        }

        if (playerSwitchingScript.currHero == PlayerSwitching.Hero.Ursa)
        {
            ursa = GameObject.FindObjectOfType<UrsaAttacks>();
            //only control the player if grounded or airControl is turned on
            if (m_Grounded || m_AirControl)
            {
                if (ursa.dontMove == false && knockbackCount <= 0)
                {
                    // Move the character by finding the target velocity
                    Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
                    // And then smoothing it out and applying it to the character
                    m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

                    // If the input is moving the player right and the player is facing left...
                    if (move > 0 && !m_FacingRight)
                    {
                        // ... flip the player.
                        Flip();
                    }
                    // Otherwise if the input is moving the player left and the player is facing right...
                    else if (move < 0 && m_FacingRight)
                    {
                        // ... flip the player.
                        Flip();
                    }
                }
                else if (knockbackCount > 0)
                {
                    if (!bigKnock)
                    {
                        if (knockFromRight)
                        {
                            m_Rigidbody2D.velocity = new Vector2(-knockbackX, knockbackY);
                        }
                        if (!knockFromRight)
                        {
                            m_Rigidbody2D.velocity = new Vector2(knockbackX, knockbackY);
                        }
                        knockbackCount -= Time.deltaTime;
                    }
                    if (bigKnock)
                    {
                        if (knockFromRight)
                        {
                            m_Rigidbody2D.velocity = new Vector2(-bigKnockX, bigKnockY);
                        }
                        if (!knockFromRight)
                        {
                            m_Rigidbody2D.velocity = new Vector2(bigKnockX, bigKnockY);
                        }
                        knockbackCount -= Time.deltaTime;
                    }
                }
            }

            // If the player should jump...
            if (m_Grounded && jump && m_Rigidbody2D.velocity.y <= 0f && ursa.dontMove == false)
            {
                // Add a vertical force to the player.
                m_Grounded = false;
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            }
        }
    }


    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        transform.Rotate(0f, 180f, 0f);
    }

    // private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (collision.name == "Ground") // your ground gameobject must be obviously called Ground
    //         grounded = true;
    // }

    // private void OnCollisionExit2D(Collision2D collision)
    // {
    //     if (collision.name == "Ground")
    //         grounded = false;
    // }


}
