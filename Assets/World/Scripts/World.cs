using UnityEngine;
using System.Collections;

public class World : MonoBehaviour
{

		public float spawn_time_normal_target_a = 1f;
		public float spawn_time_normal_target_b = 1f;
		private float time_to_spawn_norm_target = 0.0f;
		public float spawn_time_shooting_target_a = 1f;
		public float spawn_time_shooting_target_b = 1f;
		private float time_to_spawn_shooting_target = 0.0f;
		public float spawn_time_bot_a = 1f;
		public float spawn_time_bot_b = 1f;
		private float time_to_spawn_bot = 0.0f;
		public float range_a_x = 0.0f;
		public float range_b_x = 0.0f;
		public float range_a_y = 0.0f;
		public float range_b_y = 0.0f;
		public float range_a_z = 0.0f;
		public float range_b_z = 0.0f;
		public float time_elapsed = 0.0f;
		public GameObject NormalTarget;
		public GameObject Target3Hit;
		public GameObject Bot;
		public GameObject gui_time;
		private bool is_shooting_target_spawned = false;
		private bool is_normal_target_spawned = false;
		private bool is_bot_spawned = false;

		// Use this for initialization
		void Start ()
		{

				time_to_spawn_shooting_target = Random.Range (spawn_time_shooting_target_a, spawn_time_shooting_target_b);
				time_to_spawn_norm_target = Random.Range (spawn_time_normal_target_a, spawn_time_normal_target_b);
				time_to_spawn_bot = Random.Range (spawn_time_bot_a, spawn_time_bot_b);
		}
	
		// Update is called once per frame
		void Update ()
		{

				int minutes = Mathf.FloorToInt (Time.realtimeSinceStartup / 60F);
				int seconds = Mathf.FloorToInt (Time.realtimeSinceStartup - minutes * 60);
		
				string niceTime = string.Format ("{0:0}:{1:00}", minutes, seconds);

				gui_time.guiText.text = niceTime;

				if (time_to_spawn_norm_target > 0)
						time_to_spawn_norm_target -= Time.deltaTime;

				if (time_to_spawn_shooting_target > 0)
						time_to_spawn_shooting_target -= Time.deltaTime;

				if (time_to_spawn_bot > 0)
						time_to_spawn_bot -= Time.deltaTime;

				if (time_to_spawn_shooting_target <= 0 && !is_shooting_target_spawned)
						this.SpawnTarget3hit ();	

				if (time_to_spawn_norm_target <= 0 && !is_normal_target_spawned) 
						this.SpawnNormalTarget ();

				if (time_to_spawn_bot <= 0 && !is_bot_spawned) 
						this.SpawnBot ();
		
		}

		void SpawnNormalTarget ()
		{
		
				if (NormalTarget != null) {
			
			
						Vector3 normTargetPos = new Vector3 (Random.Range (range_a_x, range_b_x),
			                                     Random.Range (range_a_y, range_b_y), Random.Range (range_a_z, range_b_z));
			
						Collider[] checkResult = Physics.OverlapSphere (normTargetPos, 1f);
			
						bool hasnt_colided = true;
			
						foreach (Collider col in checkResult) {
				
								if (col.gameObject.name.Equals ("MainTarget") || col.gameObject.name.Equals ("Player") || col.gameObject.name.Equals ("Target3hit"))
										hasnt_colided = false;
						}
			
						if (hasnt_colided) {
				
								Instantiate (NormalTarget, normTargetPos, Quaternion.identity);
								is_normal_target_spawned = true;
						}
				}
		}

		void SpawnTarget3hit ()
		{
		
				if (Target3Hit != null) {
			
			
						Vector3 Target3Pos = new Vector3 (Random.Range (range_a_x, range_b_x),
			                                     Random.Range (range_a_y, range_b_y), Random.Range (range_a_z, range_b_z));
			
						Collider[] checkResult = Physics.OverlapSphere (Target3Pos, 1f);
			
						bool hasnt_colided = true;
			
						foreach (Collider col in checkResult) {
				
								if (col.gameObject.name.Equals ("MainTarget") || col.gameObject.name.Equals ("Player") || col.gameObject.name.Equals ("Target3hit"))
										hasnt_colided = false;
						}
			
						if (hasnt_colided) {
				
								Instantiate (Target3Hit, Target3Pos, Quaternion.identity);
								is_shooting_target_spawned = true;
						}
				}
		}

		void SpawnBot ()
		{

				if (Bot != null) {

						Vector3 BotPos = new Vector3 (Random.Range (13f, 13f),
			                                  6f, Random.Range (-9f, 9f));
			
						Collider[] checkResult = Physics.OverlapSphere (BotPos, 1f);
			
						bool hasnt_colided = true;
			
						foreach (Collider col in checkResult) {
				
								if (col.gameObject.name.Equals ("MainTarget") || col.gameObject.name.Equals ("Player") || col.gameObject.name.Equals ("Target3hit"))
										hasnt_colided = false;
						}
			
						if (hasnt_colided) {
				
								Instantiate (Bot, BotPos, Quaternion.identity);
								is_bot_spawned = true;
						}
				}
		}

		public void ShootingTargetDied ()
		{

				time_to_spawn_shooting_target = Random.Range (spawn_time_shooting_target_a, spawn_time_shooting_target_b);
				is_shooting_target_spawned = false;
		}

		public void NormalTargetDied ()
		{

				time_to_spawn_norm_target = Random.Range (spawn_time_normal_target_a, spawn_time_normal_target_b);
				is_normal_target_spawned = false;
		}

		public void BotDied ()
		{
		
				time_to_spawn_bot = Random.Range (spawn_time_bot_a, spawn_time_bot_b);
				is_bot_spawned = false;
		}
}
