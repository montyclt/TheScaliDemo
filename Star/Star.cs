using System;
using Godot;

namespace TheScaliDemo.Star
{
    public class Star : Node2D
    {
        [Signal]
        public delegate void StarCollected();
    
        public override void _Ready()
        {
            RandomizePosition();
        }

        public void OnBodyEntered(Node node)
        {
            EmitSignal(nameof(StarCollected));
            QueueFree();
        }

        private void RandomizePosition()
        {
            var rnd = new Random();
            int x = rnd.Next(50, 950);
            int y = rnd.Next(64, 576);
            Position = new Vector2(x, y);
        }
    }
}
