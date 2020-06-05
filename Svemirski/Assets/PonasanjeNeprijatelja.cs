using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PonasanjeNeprijatelja : MonoBehaviour {

	public GameObject projektil;
	public float snaga = 150;
	public float PucanjuSekundi=4f;
	public float brzinaProjektila=10;
	public AudioClip zvukPucnja;
	public AudioClip zvukUnistenja;

	// Update is called once per frame
	void Update () {
		float vjerojatnos = PucanjuSekundi * Time.deltaTime;
		if (Random.value < vjerojatnos) {
			Fire ();
		}
	}

	void Fire()
	{
		Vector3 offset = new Vector3(0, -1.0f, 0);
		Vector3 polozajPucanja = transform.position + offset;
		GameObject missile = Instantiate(projektil, polozajPucanja, Quaternion.identity);
		missile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -brzinaProjektila);
		AudioSource.PlayClipAtPoint(zvukPucnja, transform.position);
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		Projektil missile = collider.gameObject.GetComponent<Projektil> ();
		if (missile) {
			missile.Hit ();
			snaga -= missile.GetDamage ();
			if (snaga <= 0) {
				AudioSource.PlayClipAtPoint(zvukUnistenja, transform.position);
				Destroy (gameObject);
			}
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	

}
