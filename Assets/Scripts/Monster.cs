using UnityEngine;

[RequireComponent(typeof(Animator))]

abstract public class Monster : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
      
    private Point[] _movementPoints;
    private AnimationObject _animationObject = new();
    private StatesAnim _state;  
    private int _direction;
    private int _currentPoint;
    private int _nextPoint;     

    public void SetStartParameters(Path path, int numberStartPosition, int direction)
    {
        _movementPoints = path.GetComponentsInChildren<Point>();
        _currentPoint = numberStartPosition;
        _direction = direction;
        _nextPoint = GetNextPoint();  
    }

    protected void Move()
    {
        float positionYShift = 1;
        float accuracy = 0.7f;

        _state = StatesAnim.Idle;

        Transform target = _movementPoints[_nextPoint].transform;

        if (Mathf.Abs(transform.position.x - target.position.x) < accuracy)
        {
            _currentPoint = _nextPoint;
            _nextPoint = GetNextPoint();
            target = _movementPoints[_nextPoint].transform;
        }

        GetComponentInChildren<SpriteRenderer>().flipX = (transform.position.x - target.position.x) < 0;        

        if (target.position.y - transform.position.y > positionYShift)
        {
            Jump(target);            
        }            
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);
            _state = StatesAnim.Run;
        }

        bool isGrounded = _animationObject.CheckGround(transform);

        if (isGrounded == false)
        {
            _state = StatesAnim.Jump;
        }

        _animationObject.Run(GetComponentInChildren<Animator>(), _state);
    }   

    private int GetNextPoint()
    {
        int nextPointPath = _currentPoint + _direction;

        if (nextPointPath < 0 || nextPointPath >= _movementPoints.Length)
            DhangeDirection();

        return _currentPoint + _direction;
    }

    private void Jump(Transform target)
    {
        transform.GetComponent<Rigidbody2D>().AddForce(Vector2.up * _jumpForce,ForceMode2D.Force);
        transform.position = Vector2.MoveTowards(transform.position, target.position, (_speed + 5) * Time.deltaTime);
    }

    private void DhangeDirection()
    {
        _direction *= -1;
    }
}

