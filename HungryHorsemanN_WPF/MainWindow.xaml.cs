using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AI_Core.Core.Node;
using AI_Core.Core.SearchingAlgorithms;
using AI_Core.Core.SearchingAlgorithms.Backtrack;
using AI_Core.Core.SearchingAlgorithms.BreadthFirst;
using AI_Core.Core.SearchingAlgorithms.DepthFirst;
using AI_Core.Core.SearchingAlgorithms.TrialAndError;
using AI_Core.StateRepresentations.HungryHorsemanNState;

namespace HungryHorsemanN_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HungryHorsemanNNode terminalNode;
        private List<HungryHorsemanNNode> solution; //Should be a List to iterate through easily
        private Image _horseman;
        private int _currentStep;
        private readonly int _size;
        private GraphSearchHungryHorsemanN _searchingAlgorithm;

        public MainWindow(int size = 3)
        {
            InitializeComponent();
            this._size = size;
            InitializeResources();
            InitializeFields();
            //PrintState(this.solution[_currentStep].State);
        }

        private void InitializeFields()
        {
            _currentStep = 0;
            //trialAndError = new TrialAndErrorHungryHorsemanN(this._size);
            //backtrack = new BacktrackHungryHorsemanN(this._size * 4, true, this._size);
            //depthFirst = new DepthFirstHungryHorsemanN(true, this._size);
            //breadthFirst = new BreadthFirstHungryHorsemanN(true, this._size);
        }
        
        private void InitializeResources()
        {
            lblTitle.Content = $"Hungry Horseman on {this._size}x{this._size} chessboard";
            GenerateStateGrid();
            PaintStateGrid();
            InitializeHorsemanImage();
        }

        private void InitializeHorsemanImage()
        {
            this._horseman = (FindResource("horsemanImage") as Image)!;
            this._horseman.Height = gridState.Height / this._size;
            this._horseman.Width = gridState.Width / this._size;
        }
        
        private void PrintState(HungryHorsemanN current)
        {
            gridState.Children.Remove(this._horseman);
            int x = current.X;
            int y = current.Y;
            gridState.Children.Add(this._horseman);
            Grid.SetRow(this._horseman, x);
            Grid.SetColumn(this._horseman, y);
            this.statusScrollViewer.Content += "\n" + current;
            this.statusScrollViewer.ScrollToBottom();
        }

        private void GenerateStateGrid()
        {
            for (int i = 0; i < this._size; i++)
            {
                RowDefinition newRow = new RowDefinition();
                ColumnDefinition newColumn = new ColumnDefinition();
                gridState.RowDefinitions.Add(newRow);
                gridState.ColumnDefinitions.Add(newColumn);
            }
        }

        private void PaintStateGrid()
        {
            var brushBlack = new SolidColorBrush(Colors.Black);
            var brushWhite = new SolidColorBrush(Colors.White);
            for (int i = 0; i < gridState.RowDefinitions.Count; i++)
            {
                for (int j = 0; j < gridState.ColumnDefinitions.Count; j++)
                {
                    Rectangle cell = new Rectangle();
                    Grid.SetRow(cell, i);
                    Grid.SetColumn(cell, j);

                    if ((i + j) % 2 == 0)
                        cell.Fill = brushWhite;
                    else
                        cell.Fill = brushBlack;
                    gridState.Children.Add(cell);
                }
            }
        }

        private void BtnForward_OnClick(object sender, RoutedEventArgs e)
        {
            this._currentStep++;
            if (solution.Count <= this._currentStep)
            {
                MessageBox.Show("There are no more steps left!");
                _currentStep--;
                return;
            }

            PrintState(this.solution[_currentStep].State);

            if (this.solution[_currentStep].State.IsGoalState())
                this.statusScrollViewer.Content += $"\nCompleted in {this.solution.Count} steps.";
        }

        private void BtnBack_OnClick(object sender, RoutedEventArgs e)
        {
            this._currentStep--;
            if (this._currentStep < 0)
            {
                MessageBox.Show("There are no more steps left!");
                _currentStep++;
                return;
            }

            PrintState(this.solution[_currentStep].State);
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (selector.SelectedIndex)
            {
                case 0: this.GetTrialAndErrorSolution(); break;
                case 1: this.GetBacktrackSolution(); break;
                case 2: this.GetDepthFirstSolution(); break;
                case 3: this.GetBreadthFirstSolution(); break;
                
                default: MessageBox.Show("Not a valid searching algorithm!"); break;
            }
        }

        private void ResetSolution()
        {
            this._currentStep = 0;
            this.terminalNode = this._searchingAlgorithm.Search();
            this.solution = this._searchingAlgorithm.GetSolution(terminalNode).ToList();
            this.statusScrollViewer.Content = "Steps: ";
            PrintState(this.solution[this._currentStep].State);
        }

        private void GetTrialAndErrorSolution()
        {
            this._searchingAlgorithm = new TrialAndErrorHungryHorsemanN(this._size);
            this.ResetSolution();
        }

        private void GetBacktrackSolution()
        {
            this._searchingAlgorithm = new BacktrackHungryHorsemanN(this._size * 4, true, this._size);
            this.ResetSolution();
        }

        private void GetDepthFirstSolution()
        {
            this._searchingAlgorithm = new DepthFirstHungryHorsemanN(true, this._size);
            this.ResetSolution();
        }

        private void GetBreadthFirstSolution()
        {
            this._searchingAlgorithm = new BreadthFirstHungryHorsemanN(true, this._size);
            this.ResetSolution();
        }
    }
}