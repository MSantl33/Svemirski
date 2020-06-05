using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KontrolaIgraca : MonoBehaviour {

	public float brzina = 15.0f;
	float xmin;
	float xmax;
	public GameObject projektil;
	public float brzinaProjektila;
	public float snaga =250;

	void OnTriggerEnter2D(Collider2D collider)
	{
		Projektil missile = collider.gameObject.GetComponent<Projektil> ();
		if (missile) {
			snaga -= missile.GetDamage ();
			missile.Hit ();
			if (snaga <= 0) {
				Die ();
			}
		}
	}

	void Die(){
		LevelMenager man = GameObject.Find ("LevelMenager").GetComponent<LevelMenager> ();
		man.LoadLevel ("Win");
		Destroy (gameObject);
	}

	// Use this for initialization
	void Start () {
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 krajnjelijevo = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distance));
		Vector3 krajnjedesno = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, distance));
		xmin = krajnjelijevo.x;
		xmax = krajnjedesno.x;

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.LeftArrow)) {
			transform.position += Vector3.left * brzina * Time.deltaTime;
		} else if (Input.GetKey (KeyCode.RightArrow)) 
		{
			transform.position += Vector3.right * brzina * Time.deltaTime;
		}
		float newX = Mathf.Clamp (transform.position.x, xmin, xmax);
		transform.position = new Vector3 (newX, transform.position.y, transform.position.z);
			
		if (Input.GetKeyDown (KeyCode.Space)) {
			GameObject laser = Instantiate (projektil, transform.position, Quaternion.identity) as GameObject;
			laser.GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, brzinaProjektila, 0);
		}
	}
}
