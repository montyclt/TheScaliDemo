using Godot;

namespace TheScaliDemo.Scali
{
    public class Player : KinematicBody2D
    {
        [Export]
        private float Velocity { get; set; }

        private Sprite _sprite;

        public override void _Ready()
        {
            _sprite = GetNode<Sprite>("Sprite");
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
            // I'm not sure that is a good idea load the texture when the player become happy
            // or preload into a texture field and set it when player become happy.

            _sprite.Texture = GD.Load<Texture>("res://Scali/Sprites/happy.png");
        }

        public void BeVeryHappy()
        {
            _sprite.Texture = GD.Load<Texture>("res://Scali/Sprites/very-happy.png");
        }
    }
}