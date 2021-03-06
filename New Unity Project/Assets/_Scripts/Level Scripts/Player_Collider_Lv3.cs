﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player_Collider_Lv3 : MonoBehaviour {

	//Public Instances
	public Text scoreLabel;
	public Text coinLabel;
	public Text lifeLabel;
	public int scoreValue = 0;
	public int coinValue = 0;
	public int lifeValue = 3;
	public Text gameOverLabel;
	public Text youWinLabel;
	public Text totalCoinLabel;
	public Text totalScoreLabel;
	public Text retry;
	public Button R_Button;
	public int minusDmg= 1;
	public int coin =1;

	public Text gemLabel;
	public int gemValue = 5;
	public int gotEm = 1;

	public SpriteRenderer spriteRenderer;
	public Rigidbody2D rigidbody2D;

	// Use this for initialization
	void Start () {
		this.gameOverLabel.enabled = false;			// this is for the GUI text making sure the gameover text is not displayed until its time
		this.youWinLabel.enabled = false;
		this.totalCoinLabel.enabled = false;
		this.totalScoreLabel.enabled = false;
		this.retry.enabled = false;
		this.R_Button.enabled = false;
		this.R_Button.image.enabled = false;
		//this._audioSources = this.GetComponents<AudioSource> ();
	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D otherGameObject) { //Pick Up Item trigger
		if (otherGameObject.tag == "Coin") {
			CoinCheck (coin); //+ 100 points for picking up 
		}

		if (otherGameObject.tag == "Enemy") { 	//Enemy Trigger
			LifeCheck (minusDmg); // minus 1 life

		}

		if (otherGameObject.tag == "Gem") {
			GemCheck (gotEm);
			Destroy (otherGameObject.gameObject);

		}

	}

	public void GemCheck(int newGemCheck)
	{
		gemValue -= newGemCheck;
		DisplayGemScore ();
	}

	public void DisplayGemScore()
	{
		gemLabel.text = "Gems Left: " + gemValue; // This displays how many Gems remaining
		if (this.gemValue <= 0) {
			this._WinGame ();
		}
	}

	public void ScoreCheck(int newScoreCheck) // This checks and updates the score
	{
		scoreValue += newScoreCheck;
		DisplayKillScore ();
	}

	public void DisplayKillScore (){
		scoreLabel.text = "Kill: " + scoreValue; // This displays the score

	}

	public void CoinCheck(int newScoreCheck) // This checks and updates the score
	{
		coinValue += newScoreCheck;
		DisplayCoinScore ();
	}

	public void DisplayCoinScore (){
		coinLabel.text = "Coin: " + coinValue; // This displays the score
	}


	public void LifeCheck (int newLifeCheck)//This checks and updates the life
	{
		lifeValue -= newLifeCheck;
		DisplayLife ();
	}

	public void DisplayLife() // This displays the Life
	{
		lifeLabel.text = "Life: " + lifeValue;
		if (this.lifeValue <= 0) {
			this._EndGame ();
		}
	}

	public void _EndGame(){
		Destroy (this.spriteRenderer);
		Destroy (this.rigidbody2D);
		this.coinLabel.enabled = false;
		this.lifeLabel.enabled = false;
		this.scoreLabel.enabled = false;
		this.gemLabel.enabled = false;
		this.gameOverLabel.enabled = true;
		this.totalCoinLabel.enabled = true;
		this.totalScoreLabel.enabled = true;
		this.R_Button.enabled = true;
		this.R_Button.image.enabled = true;
		this.retry.enabled = true;
		this.totalCoinLabel.text = "Total Gold: " + this.coinValue;
		this.totalScoreLabel.text = "Total Kills: " + this.scoreValue;
	}

	public void _WinGame(){
		Destroy (this.spriteRenderer);
		Destroy (this.rigidbody2D);
		this.coinLabel.enabled = false;
		this.lifeLabel.enabled = false;
		this.scoreLabel.enabled = false;
		this.youWinLabel.enabled = true;
		this.totalCoinLabel.enabled = true;
		this.totalScoreLabel.enabled = true;
		this.R_Button.enabled = true;
		this.R_Button.image.enabled = true;
		this.retry.enabled = true;
		this.totalCoinLabel.text = "Total Gold: " + this.coinValue;
		this.totalScoreLabel.text = "Total Kills: " + this.scoreValue;
	}


}
