using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 40f;
	[SerializeField] private float jumpForce = 400f;
    [SerializeField] private float slowedJumpForce = 600f;
    [SerializeField] private float slowedMoveSpeed;
	[SerializeField] private float smoothing = .05f;
	[SerializeField] private bool airControl = false;
	[SerializeField] private Transform groundChecker;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Player player;

    const float groundCheckRadius = .2f;
	private bool grounded;
	private bool facingRight = true;
	private Vector2 _velocity = Vector2.zero;
    private float horizontalMovement = 0f;
    private bool jump = false;
    private bool Slowed = false;
    private float currentJumpForce;
    private float currentMoveSpeed;

    private void Start()
    {
        currentMoveSpeed = moveSpeed;
        currentJumpForce = jumpForce;
    }

    private void Update()
    {
        //Get whether the user is pressing the A button or the D button.
        horizontalMovement = Input.GetAxisRaw("Horizontal") * currentMoveSpeed;

        if(horizontalMovement != 0)
        {
            player.animator.ChangeState(PlayerAnimator.CurrentState.Walking);
        }
        else
        {
            player.animator.ChangeState(PlayerAnimator.CurrentState.Idle);
        }

        if (Input.GetButtonDown("Jump") && grounded == true)
        {
            grounded = false;
            jump = true;
            //gameObject.layer = LayerMask.NameToLayer("Player_NoCollisions");
        }
        ApplyRotation();
    }

    private void FixedUpdate()
	{
        //Checking if the player is grounded or not.
		bool wasGrounded = grounded;
		grounded = false;

        Collider2D[] hits = Physics2D.OverlapCircleAll(groundChecker.position, groundCheckRadius);
		for (int i = 0; i < hits.Length; i++)
		{
            
            if (hits[i].gameObject != gameObject)
            {
                grounded = true;
            }
            
		}

        //Move the character.
        if (Slowed)
        {
            Move((horizontalMovement / 2) * Time.deltaTime, jump);
        }
        else
        {
            Move(horizontalMovement * Time.deltaTime, jump);
        }
        jump = false;
    }


	public void Move(float move, bool _jump)
	{
        //Checking that we are allowed to move the character.
		if (grounded == true || airControl == true)
		{
            //Need to get the goal vector that the player will move towards.
			Vector2 goal = new Vector2(move * 10f, _rigidbody.velocity.y);
            //Actually move the player using smooth damp which smoothes out movement using delta time.
			_rigidbody.velocity = Vector2.SmoothDamp(_rigidbody.velocity, goal, ref _velocity, smoothing, 500f, Time.deltaTime);
        }

        //Check if the player needs to jump.
		if (grounded && _jump)
		{
			grounded = false;
			_rigidbody.AddForce(new Vector2(0f, currentJumpForce));

        }
    }

    private void ApplyRotation()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (mousePos.x > transform.position.x)
            {
                FaceLeft();
            }
            else
            {
                FaceRight();
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                FaceRight();
            }
            if(Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
            {
                FaceLeft();
            }
        }
    }


	private void Flip()
	{
		facingRight = !facingRight;

        _renderer.flipX = !_renderer.flipX;
	}

    private void FaceLeft()
    {
        facingRight = false;
        _renderer.flipX = false;
    }

    private void FaceRight()
    {
        facingRight = true;
        _renderer.flipX = true;
    }

    public void SlowDown()
    {
        Slowed = true;
        currentMoveSpeed = slowedMoveSpeed;
        currentJumpForce = slowedJumpForce;
    }

    public void SpeedUp()
    {
        Slowed = false;
        currentMoveSpeed = moveSpeed;
        currentJumpForce = jumpForce;
    }
}
