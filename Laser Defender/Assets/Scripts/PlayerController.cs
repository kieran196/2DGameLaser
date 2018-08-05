using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float speed = 15.0f;
	public float padding = 1f;
	private float xmin;
	private float xmax;
	private readonly float bulletSpeed = 10.5f;
	public float firingRate = 0.2f;
	public GameObject laserPrefab;
	public GameObject projectile;
	public int playerHealth = 100;
	public LevelManager level;

	public AudioClip fireSound;

	//private GameObject[] bullets = new GameObject[5]; //Fixed size of bullets

	void OnTriggerEnter2D(Collider2D collider) {
		//if (collider.gameObject.gameObject.name == projectile.gameObject.name + "(Clone)") {
			Destroy (collider.gameObject);
			playerHealth -= 25;
			if (playerHealth == 0) {
				level.LoadLevel ("Lose");
			}
		//}
	}

	// Use this for initialization
	void Start () {
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, distance));
		xmin = leftmost.x + padding;
		xmax = rightmost.x - padding;
	}

	void Fire() {
		GameObject bullet = Instantiate (laserPrefab, this.transform.position, Quaternion.identity) as GameObject;
		bullet.GetComponent<Rigidbody2D> ().velocity = new Vector3 (0f, bulletSpeed, 0f);
		AudioSource.PlayClipAtPoint (fireSound, transform.position);
	}

	//TODO - Garbage Collection on Bullet
	void PlayerFireBullet() {
		if (Input.GetKeyDown (KeyCode.Space)) {
			InvokeRepeating ("Fire", 0.000001f, firingRate);
		}
		if (Input.GetKeyUp (KeyCode.Space)) {
			CancelInvoke ("Fire");
		}
			/*bullets [bullets.Length-1] = bullet;
			print (bullets.Length);
			print (bullets.ToString ());
		}*/
		/*foreach(GameObject laser in bullets) {
			if (laser != null) {
				laser.transform.position += new Vector3 (0f, bulletSpeed, 0f);
			}
			if (laser != null && laser.transform.position.y >= 1.5) {
				Destroy (laser);
			}
		}*/
	}

	void PlayerMovement() {
		if (Input.GetKey (KeyCode.LeftArrow)) {
			this.transform.position += new Vector3 (-speed * Time.deltaTime, 0, 0);
			// Equivalent code below
			// this.transform.position += Vector3.left * speed * Time.deltaTime;
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			this.transform.position += new Vector3 (speed * Time.deltaTime, 0, 0);
		}

		// Restrict the player to inside the map
		float newX = Mathf.Clamp (transform.position.x, xmin, xmax);
		transform.position = new Vector3 (newX, transform.position.y, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		/*if (EnemySpawner.spawnedEnemies == 0) {
			level.LoadNextLevel ();
		}*/
		PlayerMovement ();
		PlayerFireBullet ();
	}
}
