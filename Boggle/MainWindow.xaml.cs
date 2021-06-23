using System;
using System.Collections.Generic;
using System.Linq;
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

        private TextBlock[,] m_diceTextBlock;

        private List<(int hPos, int vPos)> m_selectedDice = new List<(int, int)>();
        private char[,] m_boardConfiguration;
        private string m_currentWord;
        public List<string> ScoredWords = new List<string>();
        private List<List<(int, int)>> m_scoredPositions = new List<List<(int, int)>>();

        private int m_currentScore = 0;
        public int CurrentScore
        {
            get => m_currentScore;
            private set
            {
                m_currentScore = value;
                ScoreText.Text = $"Score: {value}";
            }
        }

        private HashSet<string> m_wordList =
            Dictionary.CreateDictionary(@"D:\DDocuments\PersonalProjects\Boggle\Boggle\Boggle\WordList.txt");

        public MainWindow()
        {
            InitializeComponent();
            m_dice = DiceBag.BasicDiceSet();
            m_diceTextBlock = new TextBlock[,]
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

            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    var horizPos = i;
                    var vertPos = j;
                    m_diceTextBlock[i, j].MouseDown += (sender, args) =>
                        HandleClickDie(horizPos, vertPos);
                }
            }
        }

        private void HandleClickDie(int i, int j)
        {
            if (m_selectedDice.Count > 0)
            {
                var lastEntry = m_selectedDice.Last();
                // Undo
                if (lastEntry == (i, j))
                {
                    var toDeselect = m_selectedDice.Last();
                    m_selectedDice.RemoveAt(m_selectedDice.Count - 1);
                    UpdateViewSelection(toDeselect.hPos, toDeselect.vPos, false);
                    UpdateCurrentWord(m_selectedDice);
                    return;
                }

                if (m_selectedDice.Contains((i, j)))
                {
                    return;
                }

                //Adjacency Check
                if (Math.Abs(lastEntry.hPos - i) > 1 || Math.Abs(lastEntry.vPos - j) > 1)
                {
                    return;
                }
            }

            m_selectedDice.Add((i, j));
            UpdateViewSelection(i, j, true);
            UpdateCurrentWord(m_selectedDice);
        }

        private void UpdateViewSelection(int i, int j, bool shouldHighlight)
        {
            m_diceTextBlock[i, j].FontWeight = shouldHighlight ? FontWeights.Bold : FontWeights.Normal;
        }


        private void UpdateCurrentWord(List<(int hPos, int vPos)> selectedDice)
        {
            m_currentWord = "";
            foreach (var diePosition in selectedDice)
            {
                var curLetter = m_boardConfiguration[diePosition.hPos, diePosition.vPos].DiceView();
                m_currentWord += curLetter;
            }

            CurrentWord.Text = m_currentWord;
        }


        private void StartButton_OnClick(object sender, RoutedEventArgs e)
        {
            // Randomize Dice
            m_boardConfiguration = m_dice.RandomBoardConfiguration();

            // Display Dice
            board00.Text = m_boardConfiguration[0, 0].DiceView();
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    m_diceTextBlock[i, j].Text = m_boardConfiguration[i, j].DiceView();
                }
            }

            // Start Timer
            // Initialize Scores
        }

        private static int ScoreWord(int wordLength)
        {
            if (wordLength <= 2)
            {
                return 0;
            }

            switch (wordLength)
            {
                case 3:
                    return 1;
                case 4:
                    return 1;
                case 5:
                    return 2;
                case 6:
                    return 3;
                case 7:
                    return 5;
                default:
                    return 11;
            }
            
        }

        private void SubmitButton_OnClick(object sender, RoutedEventArgs e)
        {
            //Check if Valid Word
            var valid = m_wordList.Contains(m_currentWord);
            if (!valid)
            {
                CurrentScore -= 2;
                //TODO: Show that word is invalid
            }
            else
            {
                var positionSequence = m_selectedDice.ToList();
                if (m_scoredPositions.Contains(positionSequence))
                {
                    //TODO: Highlight word that's been used.
                    return;
                }

                //Score Word
                var letterCount = m_selectedDice.Count;
                var wordScore = ScoreWord(letterCount);
                if (wordScore == 0)
                {
                    //TODO: Inform too few letters for word?
                    // Invalidate Button if less than 3 letters.
                    return;
                }

                CurrentScore += wordScore;
                //Add Word to List
                ScoredWords.Add(m_currentWord);
                var wordList = "";
                foreach (var word in ScoredWords)
                {
                    wordList += $"{word}\n";
                }

                ScoredWordsListView.Text = wordList;
                // Reset Selection Stack and selection view
                foreach (var selectedDie in m_selectedDice)
                {
                    UpdateViewSelection(selectedDie.hPos, selectedDie.vPos, false);
                }
                m_selectedDice.Clear();
                UpdateCurrentWord(m_selectedDice);
            }

            // Update List of words, selection, score, currentWord visualization
        }
    }
}