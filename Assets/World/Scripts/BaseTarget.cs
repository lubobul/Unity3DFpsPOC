using UnityEngine;
using System.Collections;

public abstract class BaseTarget : MonoBehaviour
{
		public float hit_points = 100f;
		
		public abstract void ReceiveDmg (float per_dmg);

		public abstract void Die ();
}
