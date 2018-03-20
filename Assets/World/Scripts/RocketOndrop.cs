using UnityEngine;
using System.Collections;

public class RocketOndrop : MonoBehaviour
{

		public GameObject rocket;

		// Use this for initialization
		void Start ()
		{
	
		}
	
		void OnCollisionEnter ()
		{
				Vector3 position = gameObject.transform.position;
				position.y += 0.3f;
				Instantiate (rocket, position, Quaternion.identity);
				Destroy (gameObject);
		}
}
