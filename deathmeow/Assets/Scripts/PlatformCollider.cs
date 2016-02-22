using UnityEngine;
using System.Collections;

public class PlatformCollider : MonoBehaviour {
	private BoxCollider2D playerCollider;
	[SerializeField]
	private BoxCollider2D platformCollider;
	[SerializeField]
	private BoxCollider2D platformTrigger;
	// Use this for initialization
	void Start () {
		playerCollider = GameObject.Find ("Cat").GetComponent <BoxCollider2D> ();
		Physics2D.IgnoreCollision(playerCollider, GetComponent <BoxCollider2D> (), false);

	}
	

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.name == "Cat") {
			Physics2D.IgnoreCollision (platformCollider, playerCollider, true);	
		}
	}
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.name == "Cat") {
			Physics2D.IgnoreCollision (platformCollider, playerCollider, false);	
		}
	}
}
