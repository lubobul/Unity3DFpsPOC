using UnityEngine;
using System.Collections.Generic;

public class Target : BaseTarget
{

		public float hit_points_left = 0;
		public float duration = 4.0f;
		private World world;
		private UpdateScore score;
		private float factor = 0.0f;
		private float time_multiplier = 10f;
		private TextMesh tm;
		private LineRenderer hp;
		private List<Vector3> hp_pos;

		// Use this for initialization
		void Start ()
		{

				world = (World)GameObject.Find ("world").GetComponent ("World");

				score = UpdateScore.GetInstance ();
				tm = gameObject.GetComponentInChildren<TextMesh> ();

				hp = GetComponent<LineRenderer> ();

				//gameObject.transform.localRotation = Quaternion.Euler(new Vector3 (90f, 90f, 0f));

				hit_points_left = hit_points;

				Vector3 pos0 = gameObject.transform.position;
				Vector3 pos1 = gameObject.transform.position;

				pos0.y -= (gameObject.transform.localScale.x / 2f) + 0.1f;
				pos0.z += gameObject.transform.localScale.x / 2f;

				pos1.y -= (gameObject.transform.localScale.x / 2f) + 0.1f;
				pos1.z -= gameObject.transform.localScale.x / 2f;
				;

				hp.SetPosition (0, pos0);
				hp.SetPosition (1, pos1);

				hp_pos = new List<Vector3> ();

				hp_pos.Add (pos0);
				hp_pos.Add (pos1);

				//Debug.Log ("Target: " + gameObject.transform.position);

		}
	
		// Update is called once per frame
		void Update ()
		{

				duration -= Time.deltaTime;

				if (duration <= 0) {

						gameObject.collider.enabled = false;

						Vector3 ov = gameObject.transform.localScale;

						factor += Time.deltaTime * time_multiplier;

						float factor_percent = factor / 100;

						ov.x -= ov.x * factor_percent;
						ov.y -= ov.y * factor_percent;
						ov.z -= ov.z * factor_percent;

						gameObject.transform.localScale = ov;

						Destroy (hp);

						if (gameObject.transform.localScale.y < 0.001f) {
				
								Destroy (gameObject.transform.parent.gameObject);

								if (gameObject.name.Equals ("MainTarget")) {


										score.score_down (hit_points_left / 5);
										world.NormalTargetDied ();
								} else if (gameObject.name.Equals ("Target3hit")) {

										score.score_down (1);
										world.ShootingTargetDied ();
								}
						}
				} else {
						tm.text = duration.ToString ("0.#");
				}

		}

		public override void ReceiveDmg (float per_dmg)
		{

				float dmg = hit_points * per_dmg;

				hit_points_left -= dmg;

				Vector3 pos1 = hp_pos [1];

				pos1.z += per_dmg * gameObject.transform.localScale.z;

				hp_pos [1] = pos1;

				hp.SetPosition (1, pos1);

				if (hit_points_left <= 0)
						Die ();
		}
	
		public override void Die ()
		{

				if (gameObject.name.Equals ("MainTarget")) {

						score.score_up (10);
						world.NormalTargetDied ();

				} else if (gameObject.name.Equals ("Target3hit")) {

						score.score_up (5);
						world.ShootingTargetDied ();
				}

				Destroy (gameObject.transform.parent.gameObject);
		}
	
}
