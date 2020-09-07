using Godot;

namespace TheScaliDemo.Scali
{
    public class Player : KinematicBody2D
    {
        [Export]
        private float Velocity { get; set; }

        private Sprite _sprite;
        private Texture _regularTexture;
        private Texture _happyTexture;
        private Texture _veryHappyTexture;

        public override void _Ready()
        {
            _sprite = GetNode<Sprite>("Sprite");
            _regularTexture = GD.Load<Texture>("res://Scali/Sprites/regular.jpg");
            _happyTexture = GD.Load<Texture>("res://Scali/Sprites/happy.png");
            _veryHappyTexture = GD.Load<Texture>("res://Scali/Sprites/very-happy.png");
        }

        public override void _PhysicsProcess(float delta)
        {
            MoveAndCollide(new Vector2(Input.GetActionStrength("move_right") * Velocity, 0));
            MoveAndCollide(new Vector2(Input.GetActionStrength("move_left") * Velocity * -1, 0));
            MoveAndCollide(new Vector2(0, Input.GetActionStrength("move_up") * -1 * Velocity));
            MoveAndCollide(new Vector2(0, Input.GetActionStrength("move_down") * Velocity));
        }

        public void BeHappy()
        {
            _sprite.Texture = _happyTexture;
        }

        public void BeVeryHappy()
        {
            _sprite.Texture = _veryHappyTexture;
        }

        public void BeRegular()
        {
            _sprite.Texture = _regularTexture;
        }
    }
}