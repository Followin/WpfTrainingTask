using System;
using System.Windows;
using System.Windows.Input;

namespace WpfTrainingTask1
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			IncreaseRowButton.Click += ChangeGridHeight(5);
			DecreaseRowButton.Click += ChangeGridHeight(-5);
		}
		
		private void CanvasMouseLeave(object sender, MouseEventArgs e)
		{
			MouseCoordinatesTextBlock.Text = "(x;y)";
		}

		private void CanvasMoveHandler(object sender, MouseEventArgs e)
		{
			var position = e.GetPosition(sender as IInputElement);
			MouseCoordinatesTextBlock.Text = $"({position.X: ##.##};{position.Y: ##.##})";
		}

		private RoutedEventHandler ChangeGridHeight(int n)
		{
			var secondRow = CustomGrid.RowDefinitions[1];

			return (s, args) =>
			{
				if (n > 0 || secondRow.ActualHeight > Math.Abs(n))
				{
					secondRow.Height = new GridLength(secondRow.ActualHeight + n);
				}
			};
		}
	}
}
