using System;
using Components;
using TMPro;
using Unity.Entities;
using UnityEngine;

namespace MonoBehavior {
	public class ScreenSpaceUI : MonoBehaviour {

		[SerializeField] private TextMeshProUGUI _text;

		private EntityManager _manager;

		private Entity _statsEntity;

		private void Start() {
			_manager = World.DefaultGameObjectInjectionWorld.EntityManager;
			_statsEntity = _manager.CreateEntityQuery(typeof(StatsProperties.Tag)).GetSingletonEntity();
		}

		private void Update() {
			int totalCount = _manager.GetComponentData<StatsProperties.AsteroidCount>(_statsEntity).value +
			                 _manager.GetComponentData<StatsProperties.BulletCount>(_statsEntity).value;

			_text.text = totalCount.ToString();
		}

	}
}