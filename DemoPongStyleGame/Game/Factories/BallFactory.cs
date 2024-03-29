﻿#region Namespaces

using BlazorGE.Game;
using BlazorGE.Game.Components;
using BlazorGE.Game.Entities;
using BlazorGE.Graphics2D.Services;
using DemoPongStyleGame.Game.Components;
using System.Numerics;

#endregion

namespace DemoPongStyleGame.Game.Factories
{
    public class BallFactory
    {
        #region Protected Constants

        protected const int DefaultHeight = 50;
        protected const int DefaultWidth = 50;
        protected const float DefaultSpeed = 0.25f;

        #endregion

        #region Protected Properties

        protected GameWorld GameWorld;
        protected IGraphicsService2D GraphicsService2D;

        #endregion

        #region Constructors

        public BallFactory(GameWorld gameWorld, IGraphicsService2D graphicsService2D)
        {
            GameWorld = gameWorld;
            GraphicsService2D = graphicsService2D;
        }

        #endregion

        #region Public Methods

        public GameEntity CreateBall()
        {
            // Generate starting position and direction for the ball
            var position = new Vector2(GraphicsService2D.CanvasWidth / 2, GraphicsService2D.CanvasHeight / 2);
            var direction = new Vector2(1, 1);

            // Create ball entity
            var entity = GameWorld.CreateGameEntity();
            entity.AttachGameComponent(new BallMovementComponent(GraphicsService2D));
            entity.AttachGameComponent(new Transform2DComponent(position, direction, DefaultSpeed, DefaultWidth, DefaultHeight));
            entity.AttachGameComponent(new BallDrawComponent(GraphicsService2D));
            entity.Activate();

            return entity;
        }

        #endregion
    }
}
