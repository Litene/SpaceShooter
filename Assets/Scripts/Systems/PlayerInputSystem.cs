using Components;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Systems {
    [UpdateInGroup(typeof(InitializationSystemGroup), OrderLast = true)]
    public partial class PlayerInputSystem : SystemBase {
        private PlayerInputActions _actions;
        private Entity _playerEntity;

        protected override void OnCreate() {
            RequireForUpdate<PlayerProperties.Tag>();
            RequireForUpdate<PlayerProperties.MoveInput>();

            _actions = new PlayerInputActions();
        }

        protected override void OnStartRunning() {
            _actions.Enable(); 
            
            _playerEntity = SystemAPI.GetSingletonEntity<PlayerProperties.Tag>();
        }

        protected override void OnUpdate() {
            var currMoveInput = _actions.PlayerInputActionsMap.PlayerMovement.ReadValue<Vector2>();
            
            SystemAPI.SetSingleton(new PlayerProperties.MoveInput() {
                Value = currMoveInput
            });
        }

        protected override void OnStopRunning() {
            _playerEntity = Entity.Null;
            _actions.Disable();
        }
    }
}