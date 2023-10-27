using Unity.Burst;
using UnityEngine;

namespace Utility {
	public struct DebugHelper {
		[BurstDiscard] public static void Log(string message) {
			Debug.Log(message);
		}
	}
}