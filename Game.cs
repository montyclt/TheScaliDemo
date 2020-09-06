using Godot;
using TheScaliDemo.Scali;

namespace TheScaliDemo
{
    public class Game : Node
    {
        [Export]
        private int PointsToHappy { get; set; }

        [Export]
        private int PointsToVeryHappy { get; set; }

        private int _points = 0;

        public override void _Ready()
        {
            CreateNewStar();
        }

        private void CreateNewStar()
        {
            var star = (Star.Star) GD.Load<PackedScene>("res://Star/Star.tscn").Instance();
            star.Connect(nameof(Star.Star.StarCollected), this, nameof(CollectStar));
            AddChild(star);
        }

        private void CollectStar()
        {
            _points++;
            GetChild(2).GetChild<Label>(0).Text = _points.ToString();
            CallDeferred(nameof(CreateNewStar));

            if (_points == PointsToHappy || _points == PointsToVeryHappy)
            {
                // We are checking if points is equals to "Points To Happy" or
                // "Points To Very Happy" to avoid getting the player every time
                // that a star is collected.

                var player = GetChild<Player>(0);

                if (_points == PointsToHappy)
                    player.BeHappy();

                if (_points == PointsToVeryHappy)
                    player.BeVeryHappy();
            }
        }
    }
}