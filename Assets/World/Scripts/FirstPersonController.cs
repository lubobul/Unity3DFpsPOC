using UnityEngine;
using System.Collections;

[RequireComponent (typeof(RequireComponent))]
public class FirstPersonController : MonoBehaviour
{

		public float speed = 5.0f;
		public float mouse_speed = 2.0f;
		public float up_down_cap = 60.0f;
		private float velocity_y = 0.0f;
		private float jump = 5.0f;
		private float rot_up_down = 0.0f;
		private float rot_left_right = 0.0f;
		private float forward_backward = 0.0f;
		private float left_right = 0.0f;
		private CharacterController cc;
	
		// Use this for initialization
		void Start ()
		{

				Screen.lockCursor = true;
				cc = GetComponent<CharacterController> ();
		}
	
		// Update is called once per frame
		void Update ()
		{
	
				if (cc.isGrounded) {
						//rotation
						rot_left_right = Input.GetAxis ("Mouse X") * mouse_speed;
						rot_up_down -= Input.GetAxis ("Mouse Y") * mouse_speed;

						//movement

						if (cc.isGrounded && Input.GetKey (KeyCode.LeftShift)) {
			
								//sprint
								forward_backward = Input.GetAxis ("Vertical") * speed * 2f;
								left_right = Input.GetAxis ("Horizontal") * speed * 2f;
						} else {

								//normal
								forward_backward = Input.GetAxis ("Vertical") * speed;
								left_right = Input.GetAxis ("Horizontal") * speed;
						}

				} else {
						//rotation
						rot_left_right = Input.GetAxis ("Mouse X") * mouse_speed / 2;
						rot_up_down -= Input.GetAxis ("Mouse Y") * mouse_speed / 2;
			
						//movement
						forward_backward = Input.GetAxis ("Vertical") * speed / 2;
						left_right = Input.GetAxis ("Horizontal") * speed / 2;
				}

				//cap rotation
				rot_up_down = Mathf.Clamp (rot_up_down, - up_down_cap, up_down_cap);

				//apply rotation
				transform.Rotate (0, rot_left_right, 0);
				Camera.main.transform.localRotation = Quaternion.Euler (rot_up_down, 0, 0);

				//jump
				if (Input.GetButtonDown ("Jump") && cc.isGrounded)
						velocity_y = jump;
				else if (cc.isGrounded)
						velocity_y = 0;

				velocity_y += Physics.gravity.y * Time.deltaTime;

				Vector3 actual_speed = new Vector3 (left_right, velocity_y, forward_backward);

				actual_speed = transform.rotation * actual_speed;


				//apply movement
				cc.Move (actual_speed * Time.deltaTime);

		}
}
