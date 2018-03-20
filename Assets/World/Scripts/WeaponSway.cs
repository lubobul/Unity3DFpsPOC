using UnityEngine;
using System.Collections;

public class WeaponSway : MonoBehaviour
{
	
		public float mouseSensitivity = 10f;
		public float maxMoveAmount = 0.5f;
		public float smoothSpeed = 3f;
		private Vector3 startPos;
		private float factorX;
		private float factorY;
		public bool bRotate;
		public float smoothRotationSpeed = 2f;
		public float rotateAmount = 45f;
		public float recoil = 0f;
		private float tiltAroundZ;
		private float tiltAroundX;
	
		void Start ()
		{

				startPos = transform.localPosition;
		}
	
		void Update ()
		{
				//get store axis and interpolate them them
				factorX = Mathf.Lerp (factorX, -Input.GetAxis ("Mouse X") * mouseSensitivity, Time.deltaTime * 10f);
				factorY = Mathf.Lerp (factorY, -Input.GetAxis ("Mouse Y") * mouseSensitivity, Time.deltaTime * 10f);

				// capping
				if (factorX > maxMoveAmount)
						factorX = maxMoveAmount;
		
				if (factorX < -maxMoveAmount)
						factorX = -maxMoveAmount;
		
				if (factorY > maxMoveAmount)
						factorY = maxMoveAmount;
		
				if (factorY < -maxMoveAmount)
						factorY = -maxMoveAmount;
		
				// set new position
				Vector3 targetPos = new Vector3 (startPos.x + factorX, startPos.y + factorY, startPos.z);
		
				// apply position and interpolate it
				transform.localPosition = Vector3.Lerp (transform.localPosition, targetPos, smoothSpeed * Time.deltaTime);
		
				// rotation
				if (bRotate) {

						tiltAroundZ = Input.GetAxis ("Mouse X") * rotateAmount;
						tiltAroundX = Input.GetAxis ("Mouse Y") * rotateAmount;
						Vector3 target = new Vector3 (tiltAroundX, 0f, tiltAroundZ);
						transform.localRotation = Quaternion.Slerp (transform.localRotation, Quaternion.Euler (target), Time.deltaTime * smoothRotationSpeed);
				}
		}

		public void Recoil ()
		{
				factorY = recoil;
		}
}