using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyFiring : MonoBehaviour {

	public GameObject enemyBullet;
	private readonly int fireChance = 200;
	private readonly float bulletSpeed = 10.5f;
	public GameObject playerBullet;
	public int hits;
	public int scoreValue = 50;
	public AudioClip fireSound;
	public AudioClip deathSound;

	private ScoreKeeper scoreKeeper;

	void Start() {
		scoreKeeper = GameObject.Find ("Score").GetComponent<ScoreKeeper> ();
	}

	void OnTriggerEnter2D(Collider2D collider) {
		Destroy (collider.gameObject);
		hits--;
		if (hits <= 0) {
			Animator anim = gameObject.GetComponent<Animator> ();
			scoreKeeper.Score (scoreValue);
			anim.Play ("Death");
			AudioSource.PlayClipAtPoint (deathSound, transform.position);
			Destroy (this.gameObject, 0.6f);
			//EnemySpawner.spawnedEnemies -= 1;
		}
	}

	// Update is called once per frame
	void Update () {
		int random = Random.Range (0, fireChance);
		if (random == 0) {
			GameObject bullet = Instantiate (enemyBullet, this.transform.position, Quaternion.identity) as GameObject;
			bullet.GetComponent<Rigidbody2D> ().velocity = new Vector3 (0f, -bulletSpeed, 0f);
			AudioSource.PlayClipAtPoint (fireSound, transform.position);
		}
	}
}
