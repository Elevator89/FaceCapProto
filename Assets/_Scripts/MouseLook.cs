using System;
using UnityEngine;

namespace Assets.Scripts.Control
{
	[Serializable]
	public class MouseLook
	{
		public float XSensitivity = 2f;
		public float YSensitivity = 2f;
		public float MinimumX = -85f;
		public float MaximumX = 85f;
		public bool LockCursor = true;

		private bool m_cursorIsLocked = true;

		public void LookRotation(Transform camera)
		{
			float deltaY = Input.GetAxis("Mouse X") * XSensitivity;
			float deltaX = Input.GetAxis("Mouse Y") * YSensitivity;

			Vector3 eulerAngles = camera.localRotation.eulerAngles;

			if (deltaY != 0)
			{
				eulerAngles = camera.localRotation.eulerAngles;
			}

			eulerAngles.x = Mathf.Clamp(GetIsotropicAngle(eulerAngles.x - deltaX), MinimumX, MaximumX);
			eulerAngles.y += deltaY;
			camera.localRotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y, 0.0f);

			UpdateCursorLock();
		}

		public void SetCursorLock(bool value)
		{
			LockCursor = value;
			if (!LockCursor)
			{//we force unlock the cursor if the user disable the cursor locking helper
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			}
		}

		public void UpdateCursorLock()
		{
			//if the user set "lockCursor" we check & properly lock the cursos
			if (LockCursor)
				InternalLockUpdate();
		}

		private void InternalLockUpdate()
		{
			if (Input.GetKeyUp(KeyCode.Escape))
			{
				m_cursorIsLocked = false;
			}
			else if (Input.GetMouseButtonUp(0))
			{
				m_cursorIsLocked = true;
			}

			if (m_cursorIsLocked)
			{
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
			}
			else if (!m_cursorIsLocked)
			{
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			}
		}

		float GetIsotropicAngle(float angle)
		{
			return angle > 180.0f ? angle - 360.0f : angle;
		}
	}
}
