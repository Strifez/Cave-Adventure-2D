using UnityEngine;
using System.Collections;

[System.Serializable]
public class VelocityRange {
	public float vMin, vMax;
	
	public VelocityRange(float vMin, float vMax){
		this.vMin=vMin;
		this.vMax = vMax;
	}
}

public class Player_Controller : MonoBehaviour {

	//Public INSTANCES
	public float speed = 50f;
	public float jump = 500f;
	public Transform sightStart;	//LINECASTING start
	public Transform sightEnd;		//LINECASTING end
	
	public VelocityRange velocityRange = new VelocityRange (300f, 1000f);
	
	public GameObject rightArrow;			//GAMEOBJECTS for Firing Arrow
	public GameObject leftArrow;
	public GameObject arrowPosition;
	
	public bool onLadder;					//Ladder variables
	public float climbspeed;

	//Private INSTANCES
	private Rigidbody2D _rigidbody2D; 		//REFERENCES
	private Transform _transform;
	private Animator _animator; 
	
	private float _movingValue = 0;			//LINECASTING Checks and movement
	private bool _isFacingRight =true;
	private bool _isGrounded = true;
	private bool _isGroundBelow = false;	
	
	public float fireRate;					//Firing arrow configurations
	private float nextFire;					
	
	private AudioSource[] _audioSource; //array of many sounds
	private AudioSource _coinSound; // one sound
	private AudioSource _bkgSound;
	private AudioSource _jumpSound;

	private float _climbVelocity;
	private float _gravityStore;
	
	// Use this for initialization
	void Start () {
		this._rigidbody2D = gameObject.GetComponent<Rigidbody2D> (); //referencing the rigidbody 2d and transform
		this._transform = gameObject.GetComponent<Transform> ();
		this._animator = gameObject.GetComponent<Animator> ();
		
		this._audioSource = gameObject.GetComponents<AudioSource> ();
		this._bkgSound = this._audioSource [0]; //assigning this to array element 0
		this._coinSound = this._audioSource [1];
		this._jumpSound = this._audioSource [2];

		_gravityStore = _rigidbody2D.gravityScale;
	}
	
	void Update (){
		if (Input.GetKeyDown ("space") && Time.time > nextFire) { //the fire button will be the spacebar
			
			nextFire = Time.time + fireRate;
			GameObject arrow;		//arrow gameobject created
			
			if (this._isFacingRight){
				arrow= (GameObject) Instantiate (rightArrow); //If character is facing right, Instantiate the right bullet
			}else {
				arrow= (GameObject) Instantiate (leftArrow); //If character is facing left, Instantiate the left bullet
			}
			arrow.transform.position = arrowPosition.transform.position; //set initial bullet position
		}
		
		if (Input.GetKeyDown (KeyCode.R)){  //press R to reload the level main.
			Application.LoadLevel("main");

		}
	}
	
	// Using Physics motion
	void FixedUpdate () {
		float forceX = 0f;
		float forceY = 0f;
		
		float absVelX = Mathf.Abs (this._rigidbody2D.velocity.x); 
		float absVelY = Mathf.Abs (this._rigidbody2D.velocity.y);
		
		this._movingValue = Input.GetAxis ("Horizontal"); // gives moving variable a value of -1 or 1
		
		//check player is moving
		if (this._movingValue != 0) {
			//check if player is grounded
			if (this._isGrounded) {
				//change animation to 1, 1 was set to Player Run Animation
				this._animator.SetInteger ("AnimState", 1);
				if (this._movingValue > 0) {
					//move right
					if (absVelX < this.velocityRange.vMax) {
						forceX = this.speed;
						this._isFacingRight = true;
						this._flip ();
					}
				} else if (this._movingValue < 0) { // if the character is moving left do this.
					// move left
					if (absVelX < this.velocityRange.vMax) {
						forceX = -this.speed;
						this._isFacingRight = false;
						this._flip ();
					}
				}
			}
		} else {
			// 0 was set to Player Idle Animation
			this._animator.SetInteger ("AnimState", 0);
			
		}
		
		//check if player is jumping 
		if ((Input.GetKey ("up") || Input.GetKey (KeyCode.W))) { 
			if (this._isGrounded) {
				// 2 was set to Player Jump Animation
				this._animator.SetInteger ("AnimState", 2);
				
				//line cast added so the player does not shoot up into the air when its touching a wall.
				this._isGroundBelow = Physics2D.Linecast(this.sightStart.position, this.sightEnd.position, 1 << LayerMask.NameToLayer ("Ground")); 
				Debug.DrawLine(this.sightStart.position,this.sightEnd.position); //draws a line in debug mode to see if linecasting works
				
				//if there is ground below then allow the jump and make the player not grounded anymore.
				if(_isGroundBelow == true){ 
					this._jumpSound.Play();
					if (absVelY < this.velocityRange.vMax) {
						forceY = this.jump;
						this._isGrounded = false;
					}
				}
				
			}
		}
		
		if (Input.GetKey ("space")){ // animation for player shooting
			if (this._isGrounded){
				this._animator.SetInteger ("AnimState", 3);
			}
		}
		//add force to push the player
		this._rigidbody2D.AddForce (new Vector2 (forceX, forceY));

		//Ladder Interactions
		if (onLadder) {
			_rigidbody2D.gravityScale = 0f;
			_climbVelocity = climbspeed * Input.GetAxisRaw ("Vertical");
			_rigidbody2D.velocity = new Vector2 (_rigidbody2D.velocity.x, _climbVelocity);
		}

		if (!onLadder) {
			_rigidbody2D.gravityScale = _gravityStore;
		}
	}
	
	
	void OnCollisionStay2D(Collision2D otherCollider){				// check if grounded CollisionStay
		if (otherCollider.gameObject.CompareTag ("Ground")) {
			this._isGrounded = true;
		}
	}
	
	void OnTriggerEnter2D(Collider2D otherCollider){				// check if player hits coin if so play sound 
		if (otherCollider.gameObject.CompareTag ("Coin")) {
			this._coinSound.Play ();
		}
	}
	
	//private methods
	//flips the character when you face the other way.
	private void _flip(){
		if (this._isFacingRight) {
			this._transform.localScale = new Vector3 (1f, 1f, 1f); 
			
		} else {
			this._transform.localScale = new Vector3 (-1f, 1f, 1f);
		}
	}
	
}
