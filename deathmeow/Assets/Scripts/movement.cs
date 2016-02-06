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
	
		checkInput ();
		move(horizontal);
		reset ();
	}
	private void move(float horizontal)
	{
		// checks if the player isnt already jumping
		if (isGrounded && jump) {
			isGrounded = false;
			myRigidbody.AddForce(new Vector2 (0, jumpSpeed));
		}
		myRigidbody.velocity = new Vector2 (horizontal * movementSpeed, myRigidbody.velocity.y);
	}
	private void checkInput()
	{
		if(Input.GetKeyDown(KeyCode.Joystick1Button0)) // A on the xbox controller
				    {
		jump = true;
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
		jump = false; // resets the jump 
	}
}

