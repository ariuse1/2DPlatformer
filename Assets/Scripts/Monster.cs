using UnityEngine;

abstract public class Monster : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected float jumpForce;
      
    protected Point[] movementPoints;    
    protected SpriteRenderer spriteRenderer;
    protected AnimationObject animationObject;
    protected Animator animator;
    protected StatesAnim state;
    protected bool isGrounded = false;    
    protected int direction;
    protected int currentPoint;
    protected int nextPoint;
    protected int flipX;

    public Monster Spawn(Point[] pointsPath, int numberStartPosition)
    {
        Transform transform = pointsPath[numberStartPosition].GetComponent<Transform>();
        Monster monster = Instantiate(gameObject, transform.position, Quaternion.identity).GetComponent<Monster>();
        monster.GetMotionParameters(pointsPath, numberStartPosition);

        return monster;
    }

    protected void Move()
    {
        float positionYShift = 1;
        float accuracy = 0.7f;
        state = StatesAnim.Idle;
        Transform target = movementPoints[nextPoint].transform;

        if (Mathf.Abs(transform.position.x - target.position.x) < accuracy)
        {
            currentPoint = nextPoint;
            nextPoint = GetNextPoint();
            target = movementPoints[nextPoint].transform;
        }

        spriteRenderer.flipX = flipX * (transform.position.x - target.position.x) < 0;        

        if (target.position.y - transform.position.y > positionYShift)
        {
            Jump(target);            
        }            
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            state = StatesAnim.Run;
        }
        
        animationObject.Run(animator, state);
    }

    private void GetMotionParameters(Point[] pointsPath, int numberStartPosition)
    {
        movementPoints = pointsPath;
        currentPoint = numberStartPosition;
        direction = ChooseRandomDirection();
        nextPoint = GetNextPoint();                
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        animationObject = GetComponent<AnimationObject>(); 
    }

    private int GetNextPoint()
    {
        int nextPointPath = currentPoint + direction;

        if (nextPointPath < 0 || nextPointPath >= movementPoints.Length)
            DhangeDirection();

        return currentPoint + direction;
    }

    private void Jump(Transform target)
    {
        transform.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce,ForceMode2D.Force);
        transform.position = Vector2.MoveTowards(transform.position, target.position, (speed + 5) * Time.deltaTime);
    }

    private int ChooseRandomDirection()
    {
        int countDirection = 2;

        if (Random.Range(0, countDirection) == 0)
            return 1;
        else return -1;
    }

    private void DhangeDirection()
    {
        direction *= -1;
    }
}

