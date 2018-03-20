using UnityEngine;
using System.Collections.Generic;

public class Bot : BaseTarget
{

		private World world;
		private Vector3 new_pos;
		private Vector3 rand;
		private bool retur = false;
		private bool finished_last_move = true;
		private Vector3 retur_position;
		private Vector3 last_pos;
		private UpdateScore score;
		public float hit_points_left = 0;
		public GameObject hp;
		public GameObject player;
		public GameObject projectile_drop;
		public float random_move_from = 0f;
		public float random_move_to = 0f;

		// Use this for initialization
		void Start ()
		{

				hit_points_left = hit_points;
				score = UpdateScore.GetInstance ();
				player = GameObject.Find ("Player");
				world = (World)GameObject.Find ("world").GetComponent ("World");

		}
	
		// Update is called once per frame
		void Update ()
		{

				float distance = Vector3.Distance (gameObject.transform.position, player.transform.position);

				if (distance > 12f && finished_last_move && !retur) {

						new_pos = Vector3.MoveTowards (transform.position, player.transform.position, 0.05f);	
						new_pos.y = 7f;
						last_pos = new_pos;
		
				} else {

						if (finished_last_move && !retur) {

								rand = (transform.position + Random.onUnitSphere) * Random.Range (random_move_from, random_move_to);
								retur = true;

								finished_last_move = false;

						} else if (finished_last_move && retur) {

								retur = false;
								rand = last_pos;

								finished_last_move = false;
						}

						new_pos = Vector3.Slerp (transform.position, rand, 0.2f);

						if (Vector3.Distance (transform.position, rand) < 0.01f)
								finished_last_move = true;

				}

				gameObject.transform.position = new_pos;

				transform.LookAt (player.transform);
		}

		public override void ReceiveDmg (float per_dmg)
		{
		
				float dmg = hit_points * per_dmg;
		
				hit_points_left -= dmg;

				Vector3 hp_scale = hp.transform.localScale;
				Vector3 hp_pos = hp.transform.position;

				hp_scale.x -= per_dmg * gameObject.transform.localScale.z;
				hp_pos.z += (per_dmg * gameObject.transform.localScale.z) / 2f;
		
				hp.transform.localScale = hp_scale;
				hp.transform.position = hp_pos;

		
				if (hit_points_left <= 0)
						Die ();
		}
	
		public override void Die ()
		{
		
				score.score_up (5);
				world.BotDied ();
				Destroy (gameObject.transform.parent.gameObject);
				Instantiate (projectile_drop, gameObject.transform.position, Quaternion.identity);
		}
}
