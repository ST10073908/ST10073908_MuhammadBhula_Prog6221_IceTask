using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.MyInkCanvas.EditingMode = InkCanvasEditingMode.Ink;
            this.inkRadio.IsChecked = true;
            this.comboColors.SelectedIndex = 0;
        }

        private void RadioButtonClicked(object sender, RoutedEventArgs e)
        {
            this.MyInkCanvas.EditingMode = (sender as RadioButton)?.Content.ToString() switch
            {
                "Ink Model" => InkCanvasEditingMode.Ink,
                "Erase Model" => InkCanvasEditingMode.EraseByStroke,
                "Select Model" => InkCanvasEditingMode.Select,
                _ => this.MyInkCanvas.EditingMode
            };

        }
        private void ColorChanged(object sender, SelectionChangedEventArgs e)
        {
            string colorToUse = (this.comboColors.SelectedItem as StackPanel).Tag.ToString();
            this.MyInkCanvas.DefaultDrawingAttributes.Color = (Color)ColorConverter.ConvertFromString(colorToUse);

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            FileStream fs;
            using(fs = new FileStream("StrokeData.bin", FileMode.Create))
                this.MyInkCanvas.Strokes.Save(fs);
            MessageBox.Show("File saved");
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            FileStream fs;
            StrokeCollection strokes;
            using (fs = new FileStream("StrokeData.bin", FileMode.Open, FileAccess.Read)) 
            {
                strokes = new StrokeCollection(fs);
                this.MyInkCanvas.Strokes = strokes;
            };
            

        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            this.MyInkCanvas.Strokes.Clear();    
        }
    }
}
