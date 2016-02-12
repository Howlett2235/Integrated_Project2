using UnityEngine;
using System.Collections;

public class movement : MonoBehaviour {
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
	// Use this for initialization
	void Start () {
		myRigidbody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		float horizontal = Input.GetAxis ("Horizontal"); // uses input manager
		isGrounded = IsGrounded();
		if(Input.GetButtonDown("Jump") && isGrounded ) 
		{
			//rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpHeight);
			Jump();
		}
		
		if(Input.GetButtonDown("Jump") && !doubleJumped && !isGrounded ) 
		{
			//rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpHeight);
			Jump();
			doubleJumped = true;
		}

		move(horizontal);
		reset ();
	}
	private void move(float horizontal)
	{
	
		myRigidbody.velocity = new Vector2 (horizontal * movementSpeed, myRigidbody.velocity.y);
	}
	public void Jump()
	{
		//myrigidbody2D.velocity = new Vector2(myrigidbody2D.velocity.x, jumpHeight);
		
		if(isGrounded) 
		{
			//rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpHeight);
			//Jump();
			myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpSpeed);
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
	private void reset()
	{
		doubleJumped = false; // resets the jump 
	}
}

