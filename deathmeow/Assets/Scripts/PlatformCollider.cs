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
		playerCollider = GameObject.Find ("WeeBlockGuy").GetComponent <BoxCollider2D> ();
		Physics2D.IgnoreCollision(playerCollider, GetComponent <BoxCollider2D> (), true);

	}
	

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.name == "WeeBlockGuy") {
			Physics2D.IgnoreCollision (platformCollider, playerCollider, true);	
		}
	}
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.name == "WeeBlockGuy") {
			Physics2D.IgnoreCollision (platformCollider, playerCollider, false);	
		}
	}
}
