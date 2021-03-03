using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   


    [SerializeField] GameObject skillBar;
    [SerializeField] private List<GameObject> hearts = new List<GameObject>();
    [Header("Values")]
    [SerializeField] private float _speedMult = 200;
    [SerializeField] private float _jumpMult = 200;
    [SerializeField] private float _dashMult = 200;
    [SerializeField] private float _dashDistance = 10f;

    [Header("Check")]
    [SerializeField] private bool _isJumping = false;
    [SerializeField] private bool _isRunning = false;
    [SerializeField] private bool _isDashing = false;
    [SerializeField] private bool _canDash = true;
    [SerializeField] private float _horizontalValue;

    [Header("LayerMask")]
    [SerializeField] private LayerMask _targetLayer;

    [Header("Dash")]
    [SerializeField] private GameObject _dashEffect;

    private BoxCollider2D _boxCollider2D;
    private CapsuleCollider2D _capsuleCollider2D;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rb;
    private Animator _anim;

    private bool _isDamaged = false;
    AudioListener audio;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(hearts.Count == 0)
        {
            GameManager.Instance.LostFight();
        }
        if(!_isDamaged)
        {
            Run();
            Jump();
        }


        HandleAnimation();
    }

    private void HandleAnimation()
    {
        if(GroundCheck())
        {
            _anim.SetBool("isJumping", false);
        }
        else
        {
            _anim.SetBool("isJumping", true);
        }
        _anim.SetBool("isRunning", _isRunning);
    }

    #region Ground Check

    private bool GroundCheck()
    {
        Bounds bounds = _boxCollider2D.bounds;
        Vector2 boundsMid = new Vector2(bounds.center.x, bounds.min.y);
        Vector2 boundsLeft = new Vector2(bounds.min.x, bounds.min.y);
        Vector2 boundsRight = new Vector2(bounds.max.x, bounds.min.y);

        float extraHeight = 0.05f;

        Color rayColor;

        RaycastHit2D raycastHitMiddle = Physics2D.Raycast(boundsMid,
                               Vector2.down, extraHeight, _targetLayer);

        RaycastHit2D raycastHitLeft = Physics2D.Raycast(boundsLeft,
                               Vector2.down, extraHeight, _targetLayer);

        RaycastHit2D raycastHitRight = Physics2D.Raycast(boundsRight,
                       Vector2.down, extraHeight, _targetLayer);

        
        if(raycastHitMiddle.collider != null || raycastHitLeft.collider != null || raycastHitRight.collider != null)
        {
            rayColor = Color.green;
        }
        else rayColor = Color.red;

        Debug.DrawRay(boundsMid,
                               Vector2.down * extraHeight, rayColor);
        Debug.DrawRay(boundsLeft,
                               Vector2.down * extraHeight, rayColor);
        Debug.DrawRay(boundsRight,
                               Vector2.down * extraHeight, rayColor);

        return raycastHitMiddle.collider != null || raycastHitLeft.collider != null || raycastHitRight.collider != null;


    }

    #endregion


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Letter")
        {
            GetDamage();
            Destroy(collision.gameObject);
        }

        if(collision.gameObject.tag == "Shard")
        {
            RiseHope();
            Destroy(collision.gameObject);
        }
    }


    #region Movement

    private void Run()
    {
        if(Input.GetButton("Horizontal") && GroundCheck())
            _isRunning = true;
        else _isRunning = false;

        float myHorizontal = Input.GetAxis("Horizontal");



        _rb.velocity = new Vector2(myHorizontal * _speedMult * Time.deltaTime, _rb.velocity.y);

        if(Input.GetKeyDown(KeyCode.A) || myHorizontal < 0)
            _spriteRenderer.flipX = true;
        else if(Input.GetKeyDown(KeyCode.D) || myHorizontal > 0)
            _spriteRenderer.flipX = false;

        _horizontalValue = myHorizontal;
    }

    private void Jump()
    {
        if(Input.GetButtonDown("Jump") && GroundCheck())
        {
            _rb.AddForce(Vector2.up * _jumpMult, ForceMode2D.Impulse);
            SFXChanger.Instance.PlayJump();
        }
    }

    #endregion

    private void GetDamage()
    {
        hearts[0].SetActive(false);
        hearts.RemoveAt(0);
        SFXChanger.Instance.PlayHurt();
    }

    private void RiseHope()
    {
        int i = Random.Range(0, 4);
        skillBar.GetComponent<SkillBar>().ChangeColorBlue(i);
        SFXChanger.Instance.PlayPowerUp();
    }

}
