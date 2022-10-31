using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SelectedOperator selectedOperator;

        double lastNumber;
        double result;

        public MainWindow()
        {
            InitializeComponent();
            acButton.Click += AcButton_Click;
            negativeButton.Click += NegativeButton_Click;
            equalButton.Click += EqualButton_Click;
            pointButton.Click += PointButton_Click;
        }

        private void PointButton_Click(object sender, RoutedEventArgs e)
        {
            if (!resultLabel.Content.ToString().Contains('.'))
            {
                resultLabel.Content = $"{resultLabel.Content}.";
            }
        }

        private void EqualButton_Click(object sender, RoutedEventArgs e)
        {
            double newNumber;
            if (double.TryParse(resultLabel.Content.ToString(), out newNumber))
            {
                switch(selectedOperator)
                {
                    case SelectedOperator.Addition:
                        result = Maths.Addition(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Subtraction:
                        result = Maths.Subtraction(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Multiplication:
                        result = Maths.Multiplication(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Division:
                        result = Maths.Division(lastNumber, newNumber);
                        break;
                }
                resultLabel.Content = result;
            }
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultLabel.Content.ToString(), out lastNumber))
            {
                resultLabel.Content = "0";
            }

            if (sender == additionButton)
            {
                selectedOperator = SelectedOperator.Addition;
            }
            if (sender == subtractionButton)
            {
                selectedOperator = SelectedOperator.Subtraction;
            }
            if (sender == multiplicationButton)
            {
                selectedOperator = SelectedOperator.Multiplication;
            }
            if (sender == divisionButton)
            {
                selectedOperator = SelectedOperator.Division;
            }
        }

        private void NegativeButton_Click(object sender, RoutedEventArgs e)
        {
            if(double.TryParse(resultLabel.Content.ToString(),out lastNumber)){
                lastNumber = lastNumber * -1;
                resultLabel.Content=lastNumber;
            }
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            double number = double.Parse(button.Content.ToString());
            
            if(resultLabel.Content.ToString() == "0")
            {
                resultLabel.Content = number;
                return;
            }
            resultLabel.Content = $"{resultLabel.Content}{number}";
        }

        private void AcButton_Click(object sender, RoutedEventArgs e)
        {
            resultLabel.Content = 0;
            lastNumber = 0;
        }
    }

    public enum SelectedOperator
    {
        Addition,Subtraction,Multiplication,Division
    }

    public class Maths
    {
        public static double Addition(double number1, double number2) 
        {
            return number1 + number2;
        }
        public static double Subtraction(double number1, double number2)
        {
            return number1 - number2;
        }
        public static double Multiplication(double number1, double number2)
        {
            return number1 * number2;
        }
        public static double Division(double number1, double number2)
        {
            if (number2 == 0)
            {
                MessageBox.Show("Can't divide by zero","Wrong operation", MessageBoxButton.OK, MessageBoxImage.Error);
                return 0;
            }
            return number1 / number2;
        }
    }
}
