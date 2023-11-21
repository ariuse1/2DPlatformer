using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jump;

    private bool _isExandSprite;
    private Animator _animator;
    private AnimationObject _animationObject = new AnimationObject();
    private SpriteRenderer _spriteRenderer;
    private bool _isRunAnimation;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        Movement();        
    }

    public void Movement()
    {
        _animator = GetComponent<Animator>();
        _isRunAnimation = false;

        if (Input.GetKey(KeyCode.D))
        {
            _isExandSprite = true;
            _isRunAnimation = true;
            transform.Translate(_speed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            _isExandSprite = false;
            _isRunAnimation = true;
            transform.Translate(_speed * Time.deltaTime * -1, 0, 0);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            transform.Translate(0, _jump * Time.deltaTime, 0);
        }

       // _animationObject.Run(_animator, _spriteRenderer, _isRunAnimation, _isExandSprite);
    }       
}
