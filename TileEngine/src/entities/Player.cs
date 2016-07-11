﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileEngine
{
    public class Player : Entity
    {
        public Camera camera { get; set; }
        static Player()
        {

        }
        public Player(string tag, Texture2D texture, Vector2 position_World, Vector2 sourceRectangle_Position, Vector2 sourceRectangle_Size, Color colour, float layerDepth)
            : base(tag, texture, position_World, sourceRectangle_Position, sourceRectangle_Size, colour, layerDepth)
        {
            camera = new Camera("Player", Vector2.Zero);
        }

        public override void Update(GameTime gameTime)
        {

        }
        public override void Draw()
        {

        }
    }
}