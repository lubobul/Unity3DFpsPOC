using UnityEngine;
using System.Collections;

public class DetonateOnHit : MonoBehaviour
{

		public float damage = 15f;
		public float explosion_radius = 2f;
		private Target target;
		private UpdateScore score;
		public GameObject explosion;

		void Start ()
		{

				score = UpdateScore.GetInstance ();
		}

		void OnCollisionEnter ()
		{
			//TODO (if needed)
		}

		void OnTriggerEnter ()
		{
				Detonate ();
		}

		void Detonate ()
		{

				if (explosion != null)
						Instantiate (explosion, gameObject.transform.position, Quaternion.identity);
			
				Collider[] coll = Physics.OverlapSphere (gameObject.transform.position, explosion_radius);

				foreach (Collider col in coll) {
						if (col.gameObject.name.Equals ("Player")) {

								float distance = Vector3.Distance (gameObject.transform.position, col.gameObject.transform.position);

								float ratio = 1f - (distance / explosion_radius);

								score.score_down (damage * ratio);
						} else if (col.gameObject.name.Equals ("MainTarget") || col.gameObject.name.Equals ("Target3hit")) {
				
								float distance = Vector3.Distance (gameObject.transform.position, col.gameObject.transform.position);
				
								float ratio = 1f - (distance / (explosion_radius + 1f));

								target = (Target)col.gameObject.GetComponent ("Target");

								target.ReceiveDmg (ratio * 2f);
						}
				}

				Destroy (gameObject);
		}
}