using UnityEngine;
using System.Collections;

public class PerformAttackProjectile : MonoBehaviour
{

		public float cooldown = 1f;
		private float cooldown_remaining = 0f;
		public GameObject projectile;

		// Use this for initialization
		void Start ()
		{
				cooldown_remaining = cooldown;
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (cooldown_remaining > 0)
						cooldown_remaining -= Time.deltaTime;

				if (cooldown_remaining <= 0) {
						cooldown_remaining = cooldown;

						Instantiate (projectile, gameObject.transform.position + gameObject.transform.forward, gameObject.transform.rotation);
				}

		}
}
