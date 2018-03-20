using UnityEngine;
using System.Collections;

public class PerformAttack : MonoBehaviour
{

		public float cooldown = 0.2f;
		public float gun_range = 100f;
		private float cooldown_remaining = 0f;
		public GameObject debrisPrefab;
		public GameObject trace;
		public GameObject shot_spawn;
		public WeaponSway ws;
		public LayerMask player_mask;
		private int rockets = 0;
		public GameObject projectile;
		private UpdateScore score;

		// Use this for initialization
		void Start ()
		{
				score = UpdateScore.GetInstance ();
		}

		public void RocketsUp ()
		{
				rockets++;
				score.RocketCount (rockets);
		}

		public void RocketsDown ()
		{
				rockets--;
				score.RocketCount (rockets);
		}
	
		// Update is called once per frame
		void Update ()
		{
	
				if (cooldown_remaining > 0)
						cooldown_remaining -= Time.deltaTime;

				BaseTarget target;
				GameObject go;
				string target_name;

				if (Input.GetMouseButtonDown (1) && cooldown_remaining <= 0 && rockets > 0) {

						cooldown_remaining = cooldown;
	
						Instantiate (projectile, Camera.main.transform.position + Camera.main.transform.forward, Camera.main.transform.rotation);
						RocketsDown ();
				}

				if (Input.GetMouseButton (0) && cooldown_remaining <= 0) {

						cooldown_remaining = cooldown;

						RaycastHit hitInfo;
						Ray ray = new Ray (Camera.main.transform.position, Camera.main.transform.forward);

						if (Physics.Raycast (ray, out hitInfo, gun_range, player_mask)) {

								Vector3 hitPoint = hitInfo.point;
								go = hitInfo.collider.gameObject;

								target = go.GetComponent<BaseTarget> ();
								target_name = go.name;

			
								if (debrisPrefab != null && trace != null) {
					
										LineRenderer lr = trace.GetComponent<LineRenderer> ();
					
										Vector3 trace_pos = shot_spawn.transform.position;

										lr.SetPosition (0, trace_pos);
										lr.SetPosition (1, hitPoint);

				
										Instantiate (trace);
										ws.Recoil ();

										Instantiate (debrisPrefab, hitPoint, Quaternion.identity);
								}

								if (target != null) {

										if (target_name.Equals ("MainTarget")) {
												//have I told you the definition of insanity ?
												float d = Vector2.Distance (new Vector2 (hitPoint.y, hitPoint.z),
						                           new Vector2 (go.transform.position.y, go.transform.position.z));

												float percent_hp = 1f - (d / (go.transform.localScale.x / 2f));

												if (percent_hp > 0 && percent_hp < 1f)
														target.ReceiveDmg (percent_hp / 3f);

										} else if (target_name.Equals ("Target3hit")) {

												target.ReceiveDmg (0.34f);
										} else if (target_name.Equals ("bot")) {
					
												target.ReceiveDmg (0.25f);
										}
								}
						}
				}
		}
}
