using UnityEngine;
using System.Collections;

public class Ladder : MonoBehaviour {

	private Player_Controller player;

	// Use this for initialization
	void Start () {
		player = FindObjectOfType<Player_Controller>(); //shortform of reference 
	}
	
	void OnTriggerEnter2D(Collider2D other)	{
		if (other.gameObject.tag == "Player") {
			player.onLadder = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)	{
		if (other.gameObject.tag == "Player") {
			player.onLadder = false;
		}
	}


}
