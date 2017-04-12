using Improbable.General;
using Improbable.Math;
using UnityEngine;

namespace Assets.Gamelogic.Extensions
{
	public static class RotationExtensions {
		public static Quaternion ToQuaternion(this Rotation rotation) {
			return new Quaternion(rotation.x, rotation.y, rotation.z, rotation.w);
		}
	}

	public static class QuaternionExtensions {
		public static Rotation ToRotation(this Quaternion q) {
			return new Rotation(q.x, q.y, q.z, q.w);
		}
	}

	public static class CoordinatesExtensions {
		public static Vector3 ToVector3(this Coordinates coordinates) {
			return new Vector3((float)coordinates.X, (float)coordinates.Y, (float)coordinates.Z);
		}
	}

	public static class Vector3Extensions {
		public static Coordinates ToCoordinates(this Vector3 vector3) {
			return new Coordinates(vector3.x, vector3.y, vector3.z);
		}
	}
}