using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	public float bulletSpeed;
	private Vector3 currentPosition;
	private Vector3 target;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		currentPosition = transform.position;
		target = currentPosition;
		target.y = currentPosition.y++;
		transform.position = Vector3.Lerp (currentPosition, target, bulletSpeed);
		EnforceBounds ();
	}

	private void EnforceBounds()
	{
		Vector3 tempPosition = transform.position; //copies the ship's current position, ensuring the ship maintains its z position
		// since the camera is at the center of the screen, the yMin and yMax is just the negative and positive values of the same value
		// therefore we can condense this to just one line
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

		//checks if the other object is the player's ship, this is to avert self-destruction since the bullet spawns 'under' the player's ship
		if (other.gameObject != playerShip.gameObject) {
				Debug.Log ("Hit " + other.gameObject);
				DestroyObject (other.gameObject);
				DestroyObject (gameObject);
		}


	}
}
