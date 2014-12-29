using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {
	public float timeBetweenAttacks;
	public int damage;

	public GameObject explosion;
	GameObject player;
	private AudioSource sound;
	float timer;
	bool dead = false;

	// Use this for initialization
	void Start () {
		sound = GetComponent<AudioSource>();
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		bool playerInRange = (transform.position - player.transform.position).magnitude < 40;
		
		if(timer >= timeBetweenAttacks && playerInRange && !dead)
		{
			Attack ();
		}
	}

	void Attack ()
	{
		timer = 0f;
		PlayerHealth.TakeDamage (damage);
		sound.Play ();
	}

	public void Kill ()
	{
		Instantiate (explosion, transform.position, transform.rotation);
		this.gameObject.SetActive (false);
	}
}
