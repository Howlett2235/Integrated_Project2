using UnityEngine;
using System.Collections;

public class movement : MonoBehaviour {
	public string playerPrefix = "P1";
	private Rigidbody2D myRigidbody;
	[SerializeField]
	private float movementSpeed;
	[SerializeField]
	private Transform[] groundPoints; // Put these on the bottom of the player 
	[SerializeField]
	private float groundRadius; // set to like 0.2
	[SerializeField]
	private LayerMask whatIsGround;
	private bool isGrounded;
	private bool jump;
	[SerializeField]
	private float jumpSpeed;
	private bool doubleJumped;
	private bool facingRight;
	private Animator myAnimator;
	private float jumpTime;
	private float jumpDelay = 0.5f;
	private bool jumped;
	// Use this for initialization
	void Start () {
		myRigidbody = GetComponent<Rigidbody2D> ();
		myAnimator = GetComponent<Animator> ();
		facingRight = true;

	}
	
	// Update is called once per frame
	void Update () 
	{

		float horizontal = Input.GetAxis (playerPrefix+"Horizontal"); // uses input manager
		isGrounded = IsGrounded();
		if(Input.GetButtonDown(playerPrefix+"Jump") && isGrounded ) 
		{
			//rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpHeight);
			Jump();
			jumpTime = jumpDelay;
			jumped = true;
			myAnimator.SetTrigger("jump");
		}
		
		if(Input.GetButtonDown(playerPrefix+"Jump") && !doubleJumped && !isGrounded ) 
		{
			//rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpHeight);
			Jump();

			doubleJumped = true;
		}
		jumpTime -= Time.deltaTime;
		if (jumpTime <= 0 && isGrounded && jumped) {
			myAnimator.SetTrigger("land");
			jumped = false;
		}
		move(horizontal);
		Flip (horizontal);
	}
	private void move(float horizontal)
	{
	
		myRigidbody.velocity = new Vector2 (horizontal * movementSpeed, myRigidbody.velocity.y);
		myAnimator.SetFloat ("speed", Mathf.Abs (horizontal));

	}
	public void Jump()
	{
		//myrigidbody2D.velocity = new Vector2(myrigidbody2D.velocity.x, jumpHeight);
		
		if(isGrounded) 
		{
			//rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpHeight);
			//Jump();
			myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpSpeed);
			doubleJumped = false;

		}
		
		if(!doubleJumped && !isGrounded) 
		{
			//rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpHeight);
			//Jump();
			myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpSpeed);
			doubleJumped = true;

		}

	}

	private bool IsGrounded(){ 
		if (myRigidbody.velocity.y <= 0) 
		{
			foreach(Transform point in groundPoints)
			{
				Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);
				for (int i = 0; i < colliders.Length; i++)
				{
				if (colliders[i].gameObject !=gameObject)
				{
						return true;

				}
				}
			}
		}
		return false;
	}
	private void Flip(float horizontal)
	{
		if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight ) 
		{
			facingRight = !facingRight;
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
		}
	}
}

