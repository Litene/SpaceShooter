using Components;
using Unity.Entities;

namespace Aspects {
    public readonly partial struct PlayerAspect : IAspect {
        public readonly Entity Entity;
        public readonly RefRO<PlayerProperties.MoveSpeed> Move;
    }
}