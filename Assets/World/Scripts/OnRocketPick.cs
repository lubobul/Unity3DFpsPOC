using UnityEngine;
using System.Collections;

public class OnRocketPick : MonoBehaviour
{

		private PerformAttack attack;

		// Use this for initialization
		void Start ()
		{
	
		}

		void Update ()
		{

				transform.RotateAround (transform.position, Vector3.up, Time.deltaTime * 100f);
		}

		void OnTriggerEnter (Collider col)
		{
				if (col.gameObject.name.Equals ("Player")) {
						attack = (PerformAttack)col.gameObject.GetComponent ("PerformAttack");
						attack.RocketsUp ();
						Destroy (gameObject);
				}
		}
}

