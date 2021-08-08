#region Namespaces

using BlazorGE.Game;
using BlazorGE.Game.Components;
using BlazorGE.Game.Entities;
using BlazorGE.Graphics2D.Services;
using DemoPongStyleGame.Game.Components;
using System.Numerics;

#endregion

namespace DemoPongStyleGame.Game.Factories
{
    public class OppositionFactory
    {
        #region Protected Constants

        protected const int DefaultHeight = 100;
        protected const int DefaultWidth = 50;

        #endregion

        #region Protected Properties

        protected GameWorld GameWorld;
        protected IGraphicsService2D GraphicsService2D;

        #endregion

        #region Constructors

        public OppositionFactory(GameWorld gameWorld, IGraphicsService2D graphicsService2D)
        {
            GameWorld = gameWorld;
            GraphicsService2D = graphicsService2D;
        }

        #endregion

        #region Public Methods

        public GameEntity CreateOpposition()
        {
            // Generate starting position for the opposition
            var position = new Vector2(0, GraphicsService2D.CanvasHeight / 2);

            // Create the entity
            var entity = GameWorld.CreateGameEntity();
            entity.AttachGameComponent(new OppositionMovementComponent(GraphicsService2D));
            entity.AttachGameComponent(new Transform2DComponent { Width = DefaultWidth, Height = DefaultHeight, Position = position });
            entity.AttachGameComponent(new OppositionDrawComponent(GraphicsService2D));
            entity.Activate();

            return entity;
        }

        #endregion
    }
}
