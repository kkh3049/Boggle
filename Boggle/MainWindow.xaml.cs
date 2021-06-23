﻿using System;
using System.Collections.Generic;
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

        private Stack<(int hPos, int vPos)> m_selectedDice = new Stack<(int, int)>();
        private char[,] m_boardConfiguration;
        
        private HashSet<string> m_wordList = Dictionary.CreateDictionary(@"D:\DDocuments\PersonalProjects\Boggle\Boggle\Boggle\WordList.txt");

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
                        HandleClickDie(horizPos, vertPos, m_diceTextBlock[horizPos, vertPos]);
                }
            }
        }

        private void HandleClickDie(int i, int j, TextBlock textBlock)
        {
            if (m_selectedDice.Count > 0)
            {
                var lastEntry = m_selectedDice.Peek();
                // Undo
                if (lastEntry == (i, j))
                {
                    m_selectedDice.Pop();
                    textBlock.FontWeight = FontWeights.Normal;
                    UpdateCurrentWord();
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

            m_selectedDice.Push((i, j));
            textBlock.FontWeight = FontWeights.Bold;
            UpdateCurrentWord();
        }


        private void UpdateCurrentWord()
        {
            string curWord = "";
            foreach (var diePosition in m_selectedDice)
            {
                var curLetter = m_boardConfiguration[diePosition.hPos, diePosition.vPos].DiceView();
                curWord += curLetter;
            }

            CurrentWord.Text = curWord;
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
    }
}