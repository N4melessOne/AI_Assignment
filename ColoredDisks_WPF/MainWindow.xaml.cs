using System;
using System.Collections.Generic;
using System.Linq;
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
using AI_Core.StateRepresentations.ColoredDisksState;

namespace ColoredDisks_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ColoredDisksNode terminalNode;
        private List<ColoredDisksNode> solution; //Should be a List to iterate through easily
        private Image _horseman;
        private int _currentStep;
        private readonly int _size;
        private GraphSearchColoredDisks _searchingAlgorithm;

        public MainWindow(int size = 3)
        {
            InitializeComponent();
            this._size = size;
            InitializeResources();
            InitializeFields();
        }

        private void InitializeFields()
        {
            _currentStep = 0;
        }

        private void InitializeResources()
        {
            lblTitle.Content = $"{this._size}x{this._size} Colored Disk problem";
            GenerateStateGrid();
            PaintStateGrid();
        }

        private void PrintState(ColoredDisksState current)
        {
            var brushRed = new SolidColorBrush(Colors.Red);
            var brushBlue = new SolidColorBrush(Colors.Blue);

            for (int i = 0; i < current.Disks.GetLength(0); i++)
            {
                for (int j = 0; j < current.Disks.GetLength(1); j++)
                {
                    Ellipse cell = new Ellipse();
                    cell.Stretch = Stretch.Uniform;
                    Grid.SetRow(cell, i);
                    Grid.SetColumn(cell, j);
                    if (current.Disks[i, j])
                        cell.Fill = brushBlue;
                    else
                        cell.Fill = brushRed;
                    gridState.Children.Add(cell);
                }
            }
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
            var brushRed = new SolidColorBrush(Colors.Red);
            for (int i = 0; i < gridState.RowDefinitions.Count; i++)
            {
                for (int j = 0; j < gridState.ColumnDefinitions.Count; j++)
                {
                    Ellipse cell = new Ellipse();
                    cell.Stretch = Stretch.Uniform;
                    Grid.SetRow(cell, i);
                    Grid.SetColumn(cell, j);
                    cell.Fill = brushRed;
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
                case 0:
                    this.GetTrialAndErrorSolution();
                    break;
                case 1:
                    this.GetBacktrackSolution();
                    break;
                case 2:
                    this.GetDepthFirstSolution();
                    break;
                case 3:
                    this.GetBreadthFirstSolution();
                    break;

                default:
                    MessageBox.Show("Not a valid searching algorithm!");
                    break;
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
            try
            {
                this._searchingAlgorithm = new TrialAndErrorColoredDisks(this._size);
                this.ResetSolution();
            }
            catch (StackOverflowException)
            {
                MessageBox.Show("There is no solution to the problem.");
                this.ResetSolution();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("There is no solution to the problem.");
                this.ResetSolution();
            }
            catch (Exception e)
            {
                MessageBox.Show("Other error:\n" + e.Message);
                this.ResetSolution();
            }
        }

        private void GetBacktrackSolution()
        {
            try
            {
                this._searchingAlgorithm = new BacktrackColoredDisks(this._size * 4, true, this._size);
                this.ResetSolution();
            }
            catch (StackOverflowException)
            {
                MessageBox.Show("There is no solution to the problem.");
                this.ResetSolution();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("There is no solution to the problem.");
                this.ResetSolution();
            }
            catch (Exception e)
            {
                MessageBox.Show("Other error:\n" + e.Message);
                this.ResetSolution();
            }
        }

        private void GetDepthFirstSolution()
        {
            try
            {
                this._searchingAlgorithm = new DepthFirstColoredDisks(true, this._size);
                this.ResetSolution();
            }
            catch (StackOverflowException)
            {
                MessageBox.Show("There is no solution to the problem.");
                this.selector.SelectedIndex = 0;
                this.ResetSolution();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("There is no solution to the problem.");
                this.selector.SelectedIndex = 0;
                this.ResetSolution();
            }
            catch (Exception e)
            {
                MessageBox.Show("Other error:\n" + e.Message);
                this.selector.SelectedIndex = 0;
                this.ResetSolution();
            }
        }

        private void GetBreadthFirstSolution()
        {
            try
            {
                this._searchingAlgorithm = new BreadthFirstColoredDisks(true, this._size);
                this.ResetSolution();
            }
            catch (StackOverflowException)
            {
                MessageBox.Show("There is no solution to the problem.");
                this.ResetSolution();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("There is no solution to the problem.");
                this.ResetSolution();
            }
            catch (Exception e)
            {
                MessageBox.Show("Other error:\n" + e.Message);
                this.ResetSolution();
            }
        }
    }
}