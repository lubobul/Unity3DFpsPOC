using UnityEngine;
using System.Collections;

public class SelfDestructDebris : MonoBehaviour
{

		public float duration = 0.2f;

		// Use this for initialization
		void Start ()
		{

				Destroy (gameObject, duration);
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
}
