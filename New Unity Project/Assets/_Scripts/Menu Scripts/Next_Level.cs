using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Next_Level : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		SceneManager.LoadScene ("Level 2");
	}

	public void OnStartButtonClick() {
		SceneManager.LoadScene ("Level 2");
	}
}
