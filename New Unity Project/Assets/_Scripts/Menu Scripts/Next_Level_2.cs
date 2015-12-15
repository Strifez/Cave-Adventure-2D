using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Next_Level_2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
			SceneManager.LoadScene ("Level 1");
	}
}
