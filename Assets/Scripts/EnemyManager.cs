using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {
	public GameObject enemy;
	public float spawnTime = 3f;
	public Vector3 center = new Vector3(0,1,0);
	public float distance = 900;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
	}
	
	void Spawn ()
	{
		
		Vector3 spawnOnSphere = Random.onUnitSphere;

		if (spawnOnSphere.z < 0) {
			spawnOnSphere.z = -spawnOnSphere.z;
		}


		Instantiate (enemy, spawnOnSphere * distance, Quaternion.LookRotation(Vector3.back));
	}
}
