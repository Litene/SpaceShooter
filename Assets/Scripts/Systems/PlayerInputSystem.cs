using Aspects;
using Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine.InputSystem;
using UnityEngine;

namespace Systems {
    [UpdateInGroup(typeof(InitializationSystemGroup), OrderLast = true)]
    [BurstCompile] public partial class PlayerInputSystem : SystemBase {
        private PlayerInputActions _actions;
        private Entity _playerEntity;
        private float3 _playerPos;

        [BurstCompile] protected override void OnCreate() {
            RequireForUpdate<PlayerProperties.Tag>();
            RequireForUpdate<PlayerProperties.MousePosition>();

            _actions = new PlayerInputActions();
        }

        [BurstCompile] protected override void OnStartRunning() {
            _actions.Enable(); 
            
            _playerEntity = SystemAPI.GetSingletonEntity<PlayerProperties.Tag>();
        }

        [BurstCompile] protected override void OnUpdate() {
            var mousePositionX = Mouse.current.position.x.ReadValue();
            var mousePositionY = Mouse.current.position.y.ReadValue();

            var cameraOffset = Camera.main.ScreenToWorldPoint(new Vector3(mousePositionX, mousePositionY, 0));
            var shoot = _actions.PlayerInputActionsMap.Shoot.IsPressed();
            
            SystemAPI.SetSingleton(new PlayerProperties.MousePosition() {
                Value = new float2(cameraOffset.x, cameraOffset.y)
            });
            SystemAPI.SetSingleton(new PlayerProperties.ShootInput() {
                Value = shoot
            });
        }

        [BurstCompile] protected override void OnStopRunning() {
            _playerEntity = Entity.Null;
            _actions.Disable();
        }
    }
}