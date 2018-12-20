using UnityEngine;

namespace Assets.Scripts.Control
{
	public class SpectatorController : MonoBehaviour
	{
		[SerializeField]
		public MouseLook MouseLook;

		public float MinWalkSpeed = 0.5f;
		public float TargetWalkSpeed = 5.0f;
		public float CurrentWalkSpeed = 5.0f;
		public float MaxWalkSpeed = 100.0f;

		public float MinStrafeSpeed = 0.25f;
		public float TargetStrafeSpeed = 2.5f;
		public float CurrentStrafeSpeed = 2.5f;
		public float MaxStrafeSpeed = 50.0f;

		public float MinScroll = 0.0f;
		public float Scroll = 5;
		public float MaxScroll = 10.0f;
		public float ScrollModifier = 1.5f;

		private void FixedUpdate()
		{
			MouseLook.UpdateCursorLock();
		}

		private void Update()
		{
			MouseLook.LookRotation(transform);

			Scroll = Mathf.Clamp(Scroll + Input.mouseScrollDelta.y, MinScroll, MaxScroll);
			TargetWalkSpeed = Mathf.Clamp(Mathf.Pow(ScrollModifier, Scroll), MinWalkSpeed, MaxWalkSpeed);
			TargetStrafeSpeed = Mathf.Clamp(Mathf.Pow(ScrollModifier, Scroll), MinStrafeSpeed, MaxStrafeSpeed);

			float verticalAxisValue = Input.GetAxis("Vertical");
			float horizontalAxisValue = Input.GetAxis("Horizontal");

			CurrentWalkSpeed = Mathf.LerpUnclamped(0.0f, TargetWalkSpeed, verticalAxisValue);
			CurrentStrafeSpeed = Mathf.LerpUnclamped(0.0f, TargetStrafeSpeed, horizontalAxisValue);

			Vector3 velocity = new Vector3(CurrentStrafeSpeed, 0.0f, CurrentWalkSpeed);

			velocity = transform.rotation * velocity;
			velocity.y = 0.0f;

			transform.position += velocity * Time.fixedDeltaTime;
		}

		private static float CutFloatPart(float value)
		{
			if (value > 0f) return 1f;
			if (value < 0f) return -1f;
			return 0f;
		}
	}
}