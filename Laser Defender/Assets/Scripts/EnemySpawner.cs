using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemyPrefab;
	public float width = 10.0f;
	public float height = 5.0f;
	public float speed = 5.0f;
	public string movement = "right"; //default moves to the right
	public float spawnDelay = 0.5f;
	private float xmax;
	private float xmin;
	private readonly float boundarySpace = 3.0f;

	//public static int spawnedEnemies = 0;
	// Use this for initialization
	void Start () {
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftBoundary = Camera.main.ViewportToWorldPoint (new Vector3 (0,0, distanceToCamera));
		Vector3 rightBoundary = Camera.main.ViewportToWorldPoint (new Vector3 (1,0, distanceToCamera));
		xmax = rightBoundary.x;
		xmin = leftBoundary.x;
		spawnUntilFull ();
	}

	void spawnEnemies() {
		foreach (Transform child in transform) {
			GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			//spawnedEnemies += 1;
			enemy.transform.parent = child;
		}
	}

	void spawnUntilFull() {
		Transform freePosition = NextFreePosition ();
		if (freePosition != null) {
			GameObject enemy = Instantiate (enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = freePosition;
		}
		if (NextFreePosition () != null) {
			Invoke ("spawnUntilFull", spawnDelay);
		}
	}

	public void OnDrawGizmos() {
		Gizmos.DrawWireCube (this.transform.position, new Vector3(width, height, 0));
	}

	Transform NextFreePosition() {
		foreach(Transform childPositionGameObject in this.transform) {
			if (childPositionGameObject.childCount == 0) {
				return childPositionGameObject;
			}
		}
		return null;
	}

	bool AllMembersDead() {
		foreach(Transform childPositionGameObject in this.transform) {
			if (childPositionGameObject.childCount > 0) {
				return false;
			}
		}
		return true;
	}

	// Update is called once per frame
	void Update () {
		if (AllMembersDead ()) {
			Debug.Log ("Empty Formation");
			spawnUntilFull ();
		}
		if (movement == "right") {
			this.transform.position += new Vector3 (speed * Time.deltaTime, 0f, 0f);
			if (this.transform.position.x >= xmax-boundarySpace) {
				movement = "left";
			}
		} else if (movement == "left") {
			this.transform.position -= new Vector3 (speed * Time.deltaTime, 0f, 0f);
			if (this.transform.position.x <= xmin+boundarySpace) {
				movement = "right";
			}
		}
	}
}
