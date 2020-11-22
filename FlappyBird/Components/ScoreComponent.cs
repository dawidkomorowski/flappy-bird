using System.Diagnostics;
using Geisha.Common.Math;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Core.SceneModel;
using Geisha.Engine.Rendering;
using Geisha.Engine.Rendering.Components;

namespace FlappyBird.Components
{
    public sealed class ScoreComponent : BehaviorComponent
    {
        private readonly Sprite[] _digitSprites;
        private Entity _digit100Entity = null!;
        private Entity _digit10Entity = null!;
        private Entity _digit1Entity = null!;

        public ScoreComponent(
            Sprite d0Sprite,
            Sprite d1Sprite,
            Sprite d2Sprite,
            Sprite d3Sprite,
            Sprite d4Sprite,
            Sprite d5Sprite,
            Sprite d6Sprite,
            Sprite d7Sprite,
            Sprite d8Sprite,
            Sprite d9Sprite)
        {
            _digitSprites = new Sprite[10];
            _digitSprites[0] = d0Sprite;
            _digitSprites[1] = d1Sprite;
            _digitSprites[2] = d2Sprite;
            _digitSprites[3] = d3Sprite;
            _digitSprites[4] = d4Sprite;
            _digitSprites[5] = d5Sprite;
            _digitSprites[6] = d6Sprite;
            _digitSprites[7] = d7Sprite;
            _digitSprites[8] = d8Sprite;
            _digitSprites[9] = d9Sprite;
        }

        public override void OnStart()
        {
            _digit100Entity = CreateEntity();
            _digit10Entity = CreateEntity();
            _digit1Entity = CreateEntity();

            Debug.Assert(Entity != null, nameof(Entity) + " != null");

            Entity.AddChild(_digit100Entity);
            Entity.AddChild(_digit10Entity);
            Entity.AddChild(_digit1Entity);
        }

        public override void OnFixedUpdate()
        {
            if (GlobalGameState.CurrentPhase == GlobalGameState.Phase.WaitingForPlayer && GlobalGameState.IsRetry == false) return;

            SetScore(GlobalGameState.Score);
        }

        private void SetScore(int score)
        {
            var digit100SpriteRenderer = _digit100Entity.GetComponent<SpriteRendererComponent>();
            var digit10SpriteRenderer = _digit10Entity.GetComponent<SpriteRendererComponent>();
            var digit1SpriteRenderer = _digit1Entity.GetComponent<SpriteRendererComponent>();

            var scoreString = score.ToString();
            digit100SpriteRenderer.Visible = scoreString.Length > 2;
            digit10SpriteRenderer.Visible = scoreString.Length > 1;
            digit1SpriteRenderer.Visible = scoreString.Length > 0;

            if (digit100SpriteRenderer.Visible)
            {
                digit100SpriteRenderer.Sprite = GetSprite(scoreString[^3].ToString());
            }

            if (digit10SpriteRenderer.Visible)
            {
                digit10SpriteRenderer.Sprite = GetSprite(scoreString[^2].ToString());
            }

            if (digit1SpriteRenderer.Visible)
            {
                digit1SpriteRenderer.Sprite = GetSprite(scoreString[^1].ToString());
            }

            if (digit100SpriteRenderer.Visible)
            {
                SetTransform(_digit100Entity, -30);
                SetTransform(_digit10Entity, 0);
                SetTransform(_digit1Entity, 30);
            }
            else if (digit10SpriteRenderer.Visible)
            {
                SetTransform(_digit10Entity, -15);
                SetTransform(_digit1Entity, 15);
            }
            else
            {
                SetTransform(_digit1Entity, 0);
            }
        }

        private Sprite GetSprite(string digit)
        {
            var intDigit = int.Parse(digit);
            return _digitSprites[intDigit];
        }

        private static void SetTransform(Entity entity, double relativeX)
        {
            var transformComponent = entity.GetComponent<Transform2DComponent>();
            transformComponent.Translation = new Vector2(relativeX, 0);
        }

        private static Entity CreateEntity()
        {
            var entity = new Entity();
            entity.AddComponent(Transform2DComponent.CreateDefault());
            entity.AddComponent(new SpriteRendererComponent
            {
                Visible = false,
                SortingLayerName = "UI"
            });
            return entity;
        }
    }
}