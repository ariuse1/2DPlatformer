using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float jumpForce = 2f;

    private Animator _animator;
    private AnimationObject _animationObject;
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;    
    private bool _isGrounded = false;
    private StatesAnim _state;


    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _animationObject = GetComponentInChildren<AnimationObject>();
    }

    private void FixedUpdate()
    {
        _isGrounded = _animationObject.CheckGround( transform);
    }

    private void Update()
    {
        _state = StatesAnim.Idle;

        if (Input.GetButton("Horizontal"))
        {
            this.Run();
        }

        if (_isGrounded && Input.GetButtonDown("Jump"))
        {
            this.Jump();
        }

        if (_isGrounded == false)
        {
            _state = StatesAnim.Jump;
        }
        
        _animationObject.Run(_animator, _state);
    }

    private void Run()
    {        
        Vector3 vector = transform.right * Input.GetAxis("Horizontal");

        transform.position = Vector3.MoveTowards(transform.position, transform.position + vector, speed *Time.deltaTime);
        _spriteRenderer.flipX = vector.x < 0;
        _state = StatesAnim.Run;
    }

    private void Jump()
    {
        _rigidbody2D.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }  
}
