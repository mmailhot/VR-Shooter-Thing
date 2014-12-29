using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour {
	public float timeBetweenBullets = 0.3f;
	public float range = 300f;
	float effectsDisplayTime = 0.2f;

	Ray shootRay;
	RaycastHit shootHit;
	int shootableMask;
	float timer;

	LineRenderer gunLine;
	AudioSource gunAudio;
	Cardboard cardboardController;

	// Use this for initialization
	void Start () {
		shootableMask = LayerMask.GetMask ("Enemy");
		gunLine = GetComponent <LineRenderer> ();
		gunAudio = GetComponent<AudioSource> ();
		cardboardController = GetComponent<Cardboard> ();
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		
		if((cardboardController.CardboardTriggered || Input.GetButton("Fire1")) && timer >= timeBetweenBullets && Time.timeScale != 0)
		{
			Shoot ();
		}
		
		if(timer >= timeBetweenBullets * effectsDisplayTime)
		{
			DisableEffects ();
		}
	}

	public void DisableEffects ()
	{
		gunLine.enabled = false;
	}
	
	
	void Shoot ()
	{
		timer = 0f;
		
		gunAudio.Play ();
		
		gunLine.enabled = true;
		gunLine.SetPosition (0, transform.position);
		
		shootRay.origin = transform.position;
		shootRay.direction = transform.forward;
		
		if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))
		{
			EnemyAttack enemyHealth = shootHit.collider.GetComponent <EnemyAttack> ();
			if(enemyHealth != null)
			{
				enemyHealth.Kill();
			}
			gunLine.SetPosition (1, shootHit.point);
		}
		else
		{
			gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
		}
	}
}
