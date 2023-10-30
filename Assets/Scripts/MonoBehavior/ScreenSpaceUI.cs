using System;
using System.Collections;
using Components;
using TMPro;
using Unity.Entities;
using UnityEngine;
using UnityEngine.Serialization;

namespace MonoBehavior {
	public class ScreenSpaceUI : MonoBehaviour {

		[SerializeField] private TextMeshProUGUI _totalEntitiesText;
		[SerializeField] private TextMeshProUGUI _totalAsteroidText;
		[SerializeField] private TextMeshProUGUI _totalBulletsText;
		[SerializeField] private TextMeshProUGUI _fpsText;
		[SerializeField] private TextMeshProUGUI _currentDifficultyText;
		[SerializeField] private TextMeshProUGUI _TimeTillNextDifficultyText;
		[SerializeField] private TextMeshProUGUI _CurrentSpawnRateText;

		[SerializeField] private float _hudRefreshRate = 1f;

		private bool _initialized;

		private float _timer;

		private EntityManager _manager;

		private Entity _statsEntity;
		private Entity _spaceEntity;

		private IEnumerator Start() {
			_manager = World.DefaultGameObjectInjectionWorld.EntityManager;
			yield return new WaitForSeconds(0.2f);
			EntityQuery statsEntity = _manager.CreateEntityQuery(typeof(StatsProperties.Tag));
			EntityQuery spaceEntity = _manager.CreateEntityQuery(typeof(DifficultyComponent));
			statsEntity.TryGetSingletonEntity<StatsProperties.Tag>(out Entity stat);
			spaceEntity.TryGetSingletonEntity<DifficultyComponent>(out Entity difi);
			_statsEntity = stat;
			_spaceEntity = difi;

			Application.targetFrameRate = 20000;
			_initialized = true;
		}

		private void Update() {
			if (!_initialized) return;
			
			if (Time.unscaledTime > _timer) {
				int fps = (int)(1f / Time.unscaledDeltaTime);
				_fpsText.text = "FPS: " + fps;
				_timer = Time.unscaledTime + _hudRefreshRate;
			}

			int asteroidCount = _manager.GetComponentData<StatsProperties.AsteroidCount>(_statsEntity).value;
			int bulletCount = _manager.GetComponentData<StatsProperties.BulletCount>(_statsEntity).value;
			int totalCount = asteroidCount + bulletCount;
			
			

			int currentDifficulty = _manager.GetComponentData<DifficultyComponent>(_spaceEntity).CurrentDifficulty;
			float timeTillNextDifficulty = _manager.GetComponentData<DifficultyComponent>(_spaceEntity).DifficultyTimer;
			float currentSpawnsPerSecond = _manager.GetComponentData<DifficultyComponent>(_spaceEntity).SpawnsPerSecond;

			//Debug.Log($"statsRef: {_statsEntity}, spaceRef: {_spaceEntity}, asteroidCount: {asteroidCount}");
			_currentDifficultyText.text = "Current difficulty: " + currentDifficulty.ToString();
			_TimeTillNextDifficultyText.text = "Difficulty increase in: " + $"{timeTillNextDifficulty:F1}s";
			_CurrentSpawnRateText.text = "Current spawn rate: " + $"{currentSpawnsPerSecond:F1}/s";
			_totalEntitiesText.text = "Total entity count: " + totalCount.ToString();
			_totalAsteroidText.text = "Total asteroid count: " + asteroidCount.ToString();
			_totalBulletsText.text = "Total bullets count: " + bulletCount.ToString();
		}

	}
}