using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	public static int health = 100;

	private Slider slider;

	// Use this for initialization
	void Start () {
		slider = GetComponent<Slider> ();
	}
	
	void Update () {
		slider.value = health;
	}

	public static void TakeDamage(int damage){
		health -= damage;
	}
}
