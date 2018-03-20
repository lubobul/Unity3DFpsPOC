using UnityEngine;
using System.Collections;

public class TargetAttack : MonoBehaviour
{
	
		public float cooldown = 1f;
		public float gun_range = 100f;
		private float cooldown_remaining = 0f;
		public GameObject debrisPrefab;
		public GameObject trace;
		private GameObject char_control;
		private UpdateScore score;

		// Use this for initialization
		void Start ()
		{

				score = UpdateScore.GetInstance ();
		}
	
		// Update is called once per frame
		void Update ()
		{
		
				if (cooldown_remaining > 0)
						cooldown_remaining -= Time.deltaTime;
		

				GameObject go;
				string target_name;
		
				if (cooldown_remaining <= 0) {
			
						cooldown_remaining = cooldown;
						char_control = GameObject.Find ("Player/player_heart");
			
						RaycastHit hitInfo;
						Ray ray = new Ray (gameObject.transform.position, char_control.transform.position - gameObject.transform.position);
			
						if (Physics.Raycast (ray, out hitInfo, gun_range)) {
				
								Vector3 hitPoint = hitInfo.point;
								go = hitInfo.collider.gameObject;

								target_name = go.name;
				
				
								if (debrisPrefab != null && trace != null) {
					
										LineRenderer lr = trace.GetComponent<LineRenderer> ();
					
										Vector3 trace_pos = gameObject.transform.position;
					
										lr.SetPosition (0, trace_pos);
										lr.SetPosition (1, hitPoint);
					
										Instantiate (trace);
					
										if (target_name.Equals ("Player")) {
												Instantiate (debrisPrefab, hitPoint, Quaternion.identity);
												score.score_down (1);
										}
								}	
						}				
				}
		}
}