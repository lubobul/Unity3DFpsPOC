using UnityEngine;
using System.Collections;

public class UpdateScore
{

		private float score = 100f;
		private GUIText score_text;
		private GUIText rocket_count;
		private static UpdateScore instance = null;

		public UpdateScore ()
		{

				score_text = GameObject.Find ("gui_score").guiText;
				rocket_count = GameObject.Find ("rockets_count").guiText;
		}

		public static UpdateScore GetInstance ()
		{

				if (instance != null)
						return instance;
				else {
				
						instance = new UpdateScore ();
						return instance;
				}
		}

		public void RocketCount (int cnt)
		{
				rocket_count.text = "Rockets: " + cnt.ToString ();
		}

		public void score_up (float points)
		{
	
				score += points;
				score_text.text = score.ToString ();
		}

		public void score_down (float points)
		{

				score -= points;
				score_text.text = score.ToString ();
		}

		public int GetScore{ get; set; }
}
