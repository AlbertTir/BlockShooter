using UnityEngine;
using System.Collections;

public class EnemyBulletController : MonoBehaviour {

	public float bulletSpeed;
	private Vector3 currentPosition;
	private Vector3 target;
	private Vector3 playerShipTarget;
	private Vector3 moveDirection;

	private GameObject playerShip;

	// Use this for initialization
	void Start () {

		playerShip = GameObject.Find ("ship");
		if(playerShip != null)
		{
			playerShipTarget = playerShip.transform.position; //just get this once so it doesn't keep tracking the player's ship
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (playerShip == null) {
			Debug.Log ("player is dead");
			DestroyObject (gameObject);
		} else {
			currentPosition = transform.position;
			
			moveDirection = playerShipTarget - currentPosition;
			moveDirection.z = 0; 
			moveDirection.Normalize();

			// for some reason changing this to target instead breaks it
			playerShipTarget = moveDirection * bulletSpeed + currentPosition;

			//target = playerShipTarget;
		}
		transform.position = Vector3.Lerp (currentPosition, playerShipTarget, Time.deltaTime);
		EnforceBounds ();
	}
	
	private void EnforceBounds()
	{
		Vector3 tempPosition = transform.position; //copies the ship's current position, ensuring the ship maintains its z position
		Vector3 cameraPosition = Camera.main.transform.position;

		float xDist = Camera.main.aspect * Camera.main.orthographicSize; // calculates distance from the center of the screen to one of its edges
		float xMax = cameraPosition.x + xDist;						     // adds the distance to the camera's x position
		float xMin = cameraPosition.x - xDist;

		if(tempPosition.x < xMin || tempPosition.x > xMax)
		{
			DestroyObject (gameObject);
		}

		// since the camera is at the center of the screen, the yMin and yMax is just the negative and positive values of the same value
		float yMax = Camera.main.orthographicSize + ((float)3); // added the 3 just so it can go slightly off screen before disappearing
		
		if(tempPosition.y < -yMax || tempPosition.y > yMax)
		{
			DestroyObject (gameObject); // destroys the bullet when it hits the top of the screen...or bottom
		}
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{

		GameObject playerShip;
		
		playerShip = GameObject.Find ("ship");
		
		//checks if the other object is the player's ship, this is to avert self-destruction since the bullet spawns 'under' the enemy's ship
		if (playerShip != null) {
			if (other.gameObject == playerShip.gameObject) {
				Debug.Log ("Hit " + other.gameObject);
				DestroyObject (other.gameObject);
				DestroyObject (gameObject);
			}
		}
	}

}
