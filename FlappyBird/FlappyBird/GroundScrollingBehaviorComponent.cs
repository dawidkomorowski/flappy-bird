using Geisha.Common.Math;
using Geisha.Engine.Core.Components;

namespace FlappyBird
{
    public sealed class GroundScrollingBehaviorComponent : BehaviorComponent
    {
        private const double Speed = 5;

        public override void OnFixedUpdate()
        {
            var transformComponent = Entity.GetComponent<TransformComponent>();
            transformComponent.Translation -= new Vector3(Speed, 0, 0);

            if (transformComponent.Translation.X < -1000)
            {
                const double groundWidth = 336;
                transformComponent.Translation += new Vector3(groundWidth * 10, 0, 0);
            }
        }
    }
}