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
using System.Text.RegularExpressions;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Display.Text = "0";
        }

        double firstNumber;
        double secondNumber;
        Regex reg = new Regex(@"\s[-+*/%]\s");

        bool equal = false; // for erasing result after clicking a number

        private void ClickNumber(object sender, RoutedEventArgs e)
        {
            if (equal == true)
            {
                Display.Text = ((Button)sender).Content.ToString();
                equal = false;
                Erase.IsEnabled = true;
            }
            else
            {
                if (Display.Text.Contains(','))
                {
                    Comma.IsEnabled = false;
                }
               
                if (reg.Match(Display.Text).Length > 0) // get rid of zero after [*/+-%] and some more logic with the second number
                {

                    if (Display.Text[Display.Text.Length - 1] == '0')
                    {
                        Display.Text = Display.Text.Substring(0, Display.Text.Length - 1) + ((Button)sender).Content.ToString();
                    }
                    else
                    {
                        Display.Text += ((Button)sender).Content.ToString();
                    }
                    string secondNumber = Display.Text.Substring(Display.Text.LastIndexOf(' ') + 1);
                    int commaCount = 0;
                    foreach (Match match in Regex.Matches(secondNumber, ","))
                    {
                        commaCount++;
                    }
                    if (commaCount < 1)
                    {
                        Comma.IsEnabled = true;
                    }
                    else
                    {
                        Comma.IsEnabled = false;
                    }
                }

                else
                {
                    Erase.IsEnabled = true;
                    if (Display.Text == "0")
                    {
                        Display.Text = ((Button)sender).Content.ToString();
                    }
                    else if (Display.Text == "-0")
                    {
                        Display.Clear();
                        Display.Text = "-" + ((Button)sender).Content.ToString();
                    }
                    else
                    {
                        Display.Text += ((Button)sender).Content.ToString();
                    }
                }
            }
        }
        
        private void ClickCE(object sender, RoutedEventArgs e)
        {
            Display.Text = "0";
            foreach (Control ctrl in Main.Children)
            {
                ctrl.IsEnabled = true;
            }
            equal = false;
        }

        private void ClickBack(object sender, RoutedEventArgs e) // erasing data on the display
        {
            if (Display.Text[0] == '-') 
            {
                if (Display.Text.Length != 2)
                {
                    Display.Text = Display.Text.Substring(0, Display.Text.Length - 1);
                    CheckComma();
                }
                else
                {
                    Display.Text = "0";
                }
            }
            else
            {
                if (Display.Text.Length != 1)
                {
                    if (reg.Match(Display.Text).Length > 0) // if we work with the second number
                    {
                        if (!(Display.Text[Display.Text.Length-1] == ' '))
                        {
                            Display.Text = Display.Text.Substring(0, Display.Text.Length - 1);
                        }
                    }
                    else
                    {
                        Display.Text = Display.Text.Substring(0, Display.Text.Length - 1);
                    }
                }
                else
                {
                    Display.Text = "0";
                }
            }
        }

        private void ClickComma(object sender, RoutedEventArgs e) // adding a comma
        {
            Display.Text += ",";
            Comma.IsEnabled = false;
        }

        private void ClickMinus(object sender, RoutedEventArgs e) // adding a minus before a number
        {
            if (reg.Match(Display.Text).Length > 0) // if we work with the second number
            {
                if (Display.Text.Substring(Display.Text.LastIndexOf(' '), 1) == " " && Display.Text.Substring(Display.Text.LastIndexOf(' ') + 1, 1) != "-")
                {
                    Display.Text = Display.Text.Substring(0, Display.Text.LastIndexOf(' ')) + " -" + Display.Text.Substring(Display.Text.LastIndexOf(' ') + 1);
                }
                else
                {
                    Display.Text = Display.Text.Substring(0, Display.Text.LastIndexOf(' ')) + " " + Display.Text.Substring(Display.Text.LastIndexOf(' ') + 2);
                }
            }
            else // if we work with the first number
            {
                if (Display.Text[0] == '-')
                {
                    Display.Text = Display.Text.Substring(1);
                }
                else
                {
                    Display.Text = "-" + Display.Text;
                }
            }
        }

        private void ClickAction(object sender, RoutedEventArgs e) // choosing action
        {
            equal = false;
            Erase.IsEnabled = true;
            Sin.IsEnabled = Cos.IsEnabled = Tg.IsEnabled = DevideByOne.IsEnabled = Sqrt.IsEnabled = Pow.IsEnabled = Plus.IsEnabled
               = Min.IsEnabled = Devide.IsEnabled = Multiply.IsEnabled = Procent.IsEnabled = false;
            string name = ((Button)sender).Content.ToString();
            firstNumber = Convert.ToDouble(Display.Text);
            Comma.IsEnabled = true;
            switch (name)
            {
                case "+":
                    {
                        Display.Text = firstNumber + " + 0";
                        break;
                    }
                case "-":
                    {
                        Display.Text = firstNumber + " - 0";
                        break;
                    }
                case "x":
                    {
                        Display.Text = firstNumber + " * 0";
                        break;
                    }
                case "/":
                    {
                        Display.Text = firstNumber + " / 0";
                        break;
                    }
                case "%":
                    {
                        Display.Text = firstNumber + " % 0";
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        private void ClickTrig(object sender, RoutedEventArgs e)
        {
          //  equal = false;
            string name = ((Button)sender).Content.ToString();
            switch (name)
            {
                case "tg":
                    {
                    Display.Text = Math.Tan(Convert.ToDouble(Display.Text)).ToString();
                    break;
                    }
                case "sin":
                    {
                    Display.Text = Math.Sin(Convert.ToDouble(Display.Text)).ToString();
                    break;
                    }
                case "cos":
                    {
                    Display.Text = Math.Cos(Convert.ToDouble(Display.Text)).ToString();
                    break;
                    }
                default: break;
            }
        }

        private void ClickEqual(object sender, RoutedEventArgs e)
        {
            try
            {
                Sin.IsEnabled = Cos.IsEnabled = Tg.IsEnabled = DevideByOne.IsEnabled = Sqrt.IsEnabled = Pow.IsEnabled = Plus.IsEnabled
                   = Min.IsEnabled = Devide.IsEnabled = Multiply.IsEnabled = Procent.IsEnabled = true;
                Erase.IsEnabled = false;
                CheckComma();
                equal = true; // block erase
                secondNumber = Convert.ToDouble(Display.Text.Substring(Display.Text.LastIndexOf(' ')));
                string action = Display.Text.Substring(Display.Text.IndexOf(' ') + 1, 1);
                switch (action)
                {
                    case "+":
                        {
                            Display.Text = (firstNumber + secondNumber).ToString();
                            break;
                        }
                    case "-":
                        {
                            Display.Text = (firstNumber - secondNumber).ToString();
                            break;
                        }
                    case "*":
                        {
                            Display.Text = (firstNumber * secondNumber).ToString();
                            break;
                        }
                    case "/":
                        {
                            if (secondNumber == 0)
                            {
                                throw new DivideByZeroException();
                            }
                            Display.Text = (firstNumber / secondNumber).ToString();
                            break;
                        }
                    case "%":
                        {
                            Display.Text = ((secondNumber * 100) / firstNumber).ToString();
                            break;
                        }
                }
            }
            catch (DivideByZeroException)
            {
                DisableAllButtons();
                Display.Text = "Error! Devision by zero is forbidden";
            }
            catch
            {
                Display.Text = "Error!";
            }
        }

        private void ClickOneDevideByX(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Display.Text == "0") throw new DivideByZeroException();
                Display.Text = (1 / Convert.ToDouble(Display.Text)).ToString();
                Erase.IsEnabled = false;
            }
            catch (DivideByZeroException)
            {
                DisableAllButtons();
                Display.Text = "Error! Devision by zero is forbidden";
            }
        }

        private void ClickSqrt(object sender, RoutedEventArgs e)
        {  
            try
            {
                Display.Text = (Math.Sqrt(Convert.ToDouble(Display.Text))).ToString();
                CheckComma();
            }
            catch
            {
                DisableAllButtons();
                Display.Text = "Error!";
            }
        }

        private void ClickPow(object sender, RoutedEventArgs e)
        {
            Display.Text = Math.Pow(Convert.ToDouble(Display.Text),2).ToString();
            Erase.IsEnabled = false;
        }

        private void DisableAllButtons()
        {
            foreach (Control ctrl in Main.Children)
            {
                ctrl.IsEnabled = false;
            }
            Display.IsEnabled = true;
            Clean.IsEnabled = true;
        }

        private void CheckComma()
        {
            if (Display.Text.Contains(','))
            {
                Comma.IsEnabled = false;
            }
            else
            {
                Comma.IsEnabled = true;
            }
        }
    }
}
