using System.Windows;
using System.Windows.Controls;
using Boggle.Model;

namespace Boggle
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private DiceBag m_dice;

        private TextBlock[,] m_dicePositions;

        public MainWindow()
        {
            InitializeComponent();
            m_dice = DiceBag.BasicDiceSet();
            m_dicePositions = new TextBlock[,]
            {
                {
                    board00,
                    board01,
                    board02,
                    board03,
                    board04,
                },
                {
                    board10,
                    board11,
                    board12,
                    board13,
                    board14,
                },
                {
                    board20,
                    board21,
                    board22,
                    board23,
                    board24,
                },
                {
                    board30,
                    board31,
                    board32,
                    board33,
                    board34,
                },
                {
                    board40,
                    board41,
                    board42,
                    board43,
                    board44,
                },
            };
        }


        private void StartButton_OnClick(object sender, RoutedEventArgs e)
        {
            // Randomize Dice
            var boardConfiguration = m_dice.RandomBoardConfiguration();

            // Display Dice
            board00.Text = boardConfiguration[0, 0].DiceView();
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    m_dicePositions[i, j].Text = boardConfiguration[i, j].DiceView();
                }
            }

            // Start Timer
            // Initialize Scores
        }
    }
}