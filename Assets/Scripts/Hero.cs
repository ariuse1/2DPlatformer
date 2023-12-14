using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private float _speed ;
    [SerializeField] private float _jumpForce ;

    private Animator _animator;
    private AnimationObject _animationObject; 
      
    private bool _isGrounded = false;
    private StatesAnim _stateAnim;

    private void Awake()
    {
        _animationObject = new();
        _animator = GetComponent<Animator>();                     
    }

    private void FixedUpdate()
    {
        _isGrounded = _animationObject.CheckGround(transform);
    }

    private void Update()
    {
        _stateAnim = StatesAnim.Idle;

        if (Input.GetButton("Horizontal"))
        {
            Run();
        }

        if (_isGrounded && Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if (_isGrounded == false)
        {
            _stateAnim = StatesAnim.Jump;
        }
        
        _animationObject.Run(_animator, _stateAnim);
    } 

    private void Run()
    {        
        Vector3 vector = transform.right * Input.GetAxisRaw("Horizontal");

        transform.position = Vector3.MoveTowards(transform.position, transform.position + vector, _speed * Time.deltaTime);

        GetComponent<SpriteRenderer>().flipX = vector.x < 0;
        _stateAnim = StatesAnim.Run;
    }

    private void Jump()
    {
        transform.GetComponent<Rigidbody2D>().AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);        
    }  
}
