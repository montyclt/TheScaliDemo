using System;
using Godot;

namespace TheScaliDemo.Star
{
    public class Star : Node2D
    {
        [Signal]
        public delegate void StarCollected(Star star);
    
        public override void _Ready()
        {
            RandomizePosition();
        }

        public void OnBodyEntered(Node node)
        {
            // We should check that the body entered is the
            // player, but because the player is the only
            // body in this game, we can skip this check.

            EmitSignal(nameof(StarCollected), this);
        }

        public void RandomizePosition()
        {
            var rnd = new Random();
            int x = rnd.Next(50, 950);
            int y = rnd.Next(64, 576);
            Position = new Vector2(x, y);
        }
    }
}
