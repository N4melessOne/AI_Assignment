using System.Windows;

namespace ColoredDisks_WPF;

public partial class StartWindow : Window
{
    public StartWindow()
    {
        InitializeComponent();
    }

    private void ResetTbSize()
    {
        tbSize.Text = "";
        tbSize.Focus();
    }

    private void BtnGo_OnClick(object sender, RoutedEventArgs e)
    {
        int size;
        if (!int.TryParse(tbSize.Text, out size))
        {
            MessageBox.Show("You have not entered a valid number!");
            ResetTbSize();
            return;
        }

        if (size <= 2)
        {
            MessageBox.Show("Please enter a number greater than 2!");
            ResetTbSize();
            return;
        }

        if (size >= 5)
        {
            MessageBox.Show("It is possible that the problem of the given size has no solution!\n Please specify a lower size", 
                "WARNING", MessageBoxButton.OK);
            ResetTbSize();
            return;
        }

        MainWindow mainWindow = new MainWindow(size);
        this.Close();
        mainWindow.ShowDialog();
    }
}