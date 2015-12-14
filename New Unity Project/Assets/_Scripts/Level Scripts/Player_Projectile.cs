using UnityEngine;
using System.Collections;

public class Player_Projectile : MonoBehaviour {

	//Public Variables
		public int addPoints; // adding points
	public float speed;
	
	//private instances
	private Player_Collider playerCollider; //reference purposes
	
	// Use this for initialization
	void Start () {
		GameObject playerColliderObject = GameObject.FindWithTag("Player"); //to call another method from another script, reference is required
		if (playerColliderObject != null)
		{
			playerCollider = playerColliderObject.GetComponent<Player_Collider>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		//Get the Bullet's current position;
		Vector2 position = transform.position;
		
		//compute the bullet's new position
		position = new Vector2 (position.x + speed + Time.deltaTime, position.y );
		
		//update the bullet's position
		transform.position = position;
		
		// this is the top-right point of the screen
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));
		
		// if the bullet went outside the screen on the right, then destroy the bullet (this code is found in a Unity tutorial, please check External Document for source)
		if (transform.position.x > max.x) {
			Destroy(gameObject);
			Debug.Log ("Arrow Destroyed");
		}
	}
	
	
	/*void OnTriggerEnter2D(Collider2D otherGameObject) { //trigger to add points and destroy gameObject
		if (otherGameObject.tag == "Enemy") {
			Destroy (otherGameObject.gameObject);
			Destroy (gameObject);
			playerCollider.ScoreCheck(addPoints); //add 1 points
		}
		
	}*/

	void OnCollisionEnter2D(Collision2D otherGameObject) {
		if (otherGameObject.gameObject.tag == "Ground") {
			Destroy (gameObject);
		}
	}

}