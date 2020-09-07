using System.Linq;
using System.Timers;
using Godot;
using TheScaliDemo.Scali;
using Timer = System.Timers.Timer;

namespace TheScaliDemo
{
    public class Game : Node
    {
        [Export]
        private int PointsToHappy { get; set; }

        [Export]
        private int PointsToVeryHappy { get; set; }

        [Export]
        private int InitialSeconds { get; set; }

        private int _points = 0;
        private int _remainingSeconds;
        private bool _isGameOver;

        private Timer _timer;
        private Label _pointsLabel;
        private Control _gameOverLabels;
        private PackedScene _starScene;
        private Player _player;

        public override void _Ready()
        {
            LoadNodes();
            CreateNewStar();
            InitializeTimer();

            void LoadNodes()
            {
                _starScene = GD.Load<PackedScene>("res://Star/Star.tscn");
                _player = GetChild<Player>(0);
                _pointsLabel = GetChild(2).GetChild<Label>(0);
                _gameOverLabels = GetChild(2).GetChild<Control>(3);
            }
        }

        public override void _Process(float delta)
        {
            if (_isGameOver && Input.IsActionJustReleased("restart"))
                RestartGame();
        }

        private void RestartGame()
        {
            ResetState();
            InitializeTimer();
            CreateNewStar();
            _isGameOver = false;
            
            void ResetState()
            {
                _player.BeRegular();
                _points = 0;
                _pointsLabel.Text = "0";
                _gameOverLabels.Hide();
            }
        }

        private void InitializeTimer()
        {
            _remainingSeconds = InitialSeconds;
            var remainingSecondsLabel = GetChild(2).GetChild<Label>(1);
            remainingSecondsLabel.Text = _remainingSeconds.ToString();

            _timer = new Timer(1000);
            _timer.Elapsed += UpdateRemainingSeconds;
            _timer.Start();
            
            void UpdateRemainingSeconds(object sender, ElapsedEventArgs e)
            {
                _remainingSeconds--;
                remainingSecondsLabel.Text = _remainingSeconds.ToString();

                if (_remainingSeconds == 0)
                {
                    _timer.Stop();
                    FireGameOver();
                }
            }
        }

        private void FireGameOver()
        {
            var star = GetChildren().OfType<Star.Star>().First();
            star.QueueFree();

            _gameOverLabels.Show();
            _isGameOver = true;
        }

        private void CreateNewStar()
        {
            var star = _starScene.Instance();
            star.Connect(nameof(Star.Star.StarCollected), this, nameof(CollectStar));
            AddChild(star);
        }

        private void CollectStar(Star.Star star)
        {
            star.RandomizePosition();
            UpdateState();
            UpdatePlayer();
            
            void UpdatePlayer()
            {
                if (_points == PointsToHappy)
                    _player.BeHappy();

                if (_points == PointsToVeryHappy)
                    _player.BeVeryHappy();
            }

            void UpdateState()
            {
                _points++;
                _pointsLabel.Text = _points.ToString();
            }
        }
    }
}