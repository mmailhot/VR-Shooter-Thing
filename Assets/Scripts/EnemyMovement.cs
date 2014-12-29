using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {
	public Vector3 playerLocation;
	public float threshhold;
	public Vector3 maxDirError;

	public int targetSeekStrength = 1;
	public int collisionAvoidanceStrength = 100;
	public int avoidNegZStregth = 20;
	public int avoidHighYStregth = 2;

	public float rotationSpeed = 20f;
	public float moveSpeed = 7f;
	public float accel = 3f;

	private Vector3 velocity;
	private Vector3 target;
	// Use this for initialization
	void Start () {
		float errX = Random.Range (-maxDirError.x, maxDirError.x);
		float errY = Random.Range (-maxDirError.y, maxDirError.y);
		float errZ = Random.Range (-maxDirError.z, maxDirError.z);

		target = new Vector3 (playerLocation.x + errX,
		                      playerLocation.y + errY,
		                      playerLocation.z + errZ);

		velocity = new Vector3 (0f, 0f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
		var otherShips = GameObject.FindGameObjectsWithTag ("Enemy");

		var avoidanceVector = new Vector3 ();

		foreach(var ship in otherShips){
			if(ship != this.gameObject){
				var vector = transform.position - ship.transform.position;
				float mag = vector.magnitude;
				avoidanceVector += vector.normalized * (collisionAvoidanceStrength / (mag * mag));
			}
		}

		var targetSeperation = target - transform.position;

		Vector3 attractionVec = new Vector3 ();


		if (targetSeperation.magnitude > threshhold + 1) 
		{
			attractionVec = targetSeekStrength * (targetSeperation).normalized;
		} else if (targetSeperation.magnitude < threshhold - 1) 
		{
			attractionVec = -targetSeekStrength * (targetSeperation).normalized;
		}

		Vector3 negZVec;
		if (transform.position.z < 20) {

			negZVec = avoidNegZStregth * new Vector3(0,0,1/Mathf.Abs(transform.position.z));
		}else{
			negZVec = new Vector3(0,0,0);
		}

		Vector3 yAdjust;
		if (Mathf.Abs (transform.position.y) > 8 && targetSeperation.magnitude < 30) {
			yAdjust = avoidHighYStregth * new Vector3(0,-transform.position.y,0);
		} else {
			yAdjust = new Vector3();
		}

		var targetV = (avoidanceVector + attractionVec + negZVec + yAdjust).normalized * moveSpeed;

		velocity = Vector3.Slerp (velocity, targetV, accel * Time.deltaTime);

		transform.position += velocity * Time.deltaTime;

		var targetRot = Quaternion.LookRotation (targetSeperation.normalized);

		transform.rotation = Quaternion.Slerp (transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
	}
}
