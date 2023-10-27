using Unity.Burst;
using UnityEngine;

namespace Utility {
	public struct DebugHelper {
		public static void Log(string message) {
			Debug.Log(message);
		}
	}
}