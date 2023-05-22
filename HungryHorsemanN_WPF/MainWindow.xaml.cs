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
using AI_Core.Core.SearchingAlgorithms.Backtrack;
using AI_Core.StateRepresentations.HungryHorsemanNState;

namespace HungryHorsemanN_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    { 
        private BacktrackHungryHorsemanN backtrack;
        private HungryHorsemanNNode terminalNode;
        private List<HungryHorsemanNNode> solution; //Should be a List to iterate through easily
        private Image _horseman;
        private int _currentStep;


        public MainWindow(int size = 3)
        {
            InitializeComponent();
            InitializeResources(size);
            InitializeFields(size);
            PrintState(this.solution[_currentStep].State);
        }

        private void InitializeFields(int size)
        {
            _currentStep = 0;
            backtrack = new BacktrackHungryHorsemanN(size * 4, true, size);
            this.terminalNode = backtrack.Search();
            this.solution = backtrack.GetSolution(terminalNode).ToList();
        }
        
        private void InitializeResources(int size)
        {
            lblTitle.Content = $"Hungry Horseman on {size}x{size} chessboard";
            GenerateStateGrid(size);
            PaintStateGrid();
            InitializeHorsemanImage(size);
        }

        private void InitializeHorsemanImage(int size)
        {
            this._horseman = (FindResource("horsemanImage") as Image)!;
            this._horseman.Height = gridState.Height / size;
            this._horseman.Width = gridState.Width / size;
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

        private void GenerateStateGrid(int size)
        {
            for (int i = 0; i < size; i++)
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
                case 0: break;
                case 1: break;
                case 2: break;
                case 3: break;
                
                default: MessageBox.Show("Not a valid searching algorithm!"); break;
            }
        }
    }
}