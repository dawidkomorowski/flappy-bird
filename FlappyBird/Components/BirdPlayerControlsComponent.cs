using Geisha.Engine.Core.Components;
using Geisha.Engine.Input.Components;

namespace FlappyBird.Components
{
    public sealed class BirdPlayerControlsComponent : BehaviorComponent
    {
        private InputComponent _inputComponent;
        private BirdPhysicsComponent _birdPhysicsComponent;

        public override void OnStart()
        {
            _inputComponent = Entity.GetComponent<InputComponent>();
            _inputComponent.BindAction("Flap", Flap);

            _birdPhysicsComponent = Entity.GetComponent<BirdPhysicsComponent>();
            _birdPhysicsComponent.Flap();
        }

        private void Flap()
        {
            if (GlobalGameState.CurrentPhase == GlobalGameState.Phase.Playing)
            {
                _birdPhysicsComponent.Flap();
            }
        }
    }
}