using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IzoPlayer : MonoBehaviour
{

    [SerializeField] private float _speedMult = 200;
    [SerializeField] private bool _isRunning = false;
    [SerializeField] private bool _isUp = false;
    [SerializeField] private bool _isDown = false;
    private BoxCollider2D _boxCollider2D;
    private CapsuleCollider2D _capsuleCollider2D;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rb;
    private Animator _anim;
    [SerializeField] private float _horizontalValue;


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        RunSide();
        RunVertical();
        HandleAnimation();
    }

    private void HandleAnimation()
    {
        _anim.SetBool("isRunning", _isRunning);
        _anim.SetBool("isUp", _isUp);
        _anim.SetBool("isDown", _isDown);
        GameManager.Instance.FollowPlayer();
    }

    private void RunSide()
    {

        float myHorizontal = Input.GetAxis("Horizontal");



        _rb.velocity = new Vector2(myHorizontal * _speedMult * Time.deltaTime, _rb.velocity.y);

        if(Input.GetKeyDown(KeyCode.A) || myHorizontal < 0)
            _spriteRenderer.flipX = true;
        else if(Input.GetKeyDown(KeyCode.D) || myHorizontal > 0)
            _spriteRenderer.flipX = false;

        _horizontalValue = myHorizontal;
    }

    private void RunVertical()
    {
        float myVertical = Input.GetAxis("Vertical");

        _rb.velocity = new Vector2(_rb.velocity.x, myVertical * _speedMult * Time.deltaTime);

        if(Input.GetKeyDown(KeyCode.S) || myVertical < 0)
        {

            _isUp = false;
            _isDown = true;
        }
            
        else if(Input.GetKeyDown(KeyCode.W) || myVertical > 0)
        {
            _isUp = true;
            _isDown = false;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "NPC")
        {
            GameManager.Instance.ChangeFightArea(GameManager.Instance.GetNPCIndex());
        }
    }
}
