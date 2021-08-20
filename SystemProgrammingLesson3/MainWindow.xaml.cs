using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace SystemProgrammingLesson3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Thread> threads;
        public MainWindow()
        {
            InitializeComponent();
            priorityComboBox.ItemsSource = Enum.GetValues(typeof(ThreadPriority));
            threads = new List<Thread> { new Thread(GenerateElementsNums), new Thread(GenerateElementsChars), new Thread(GenerateElementsSymbols) };
            foreach (Thread item in threads)
            {
                item.IsBackground = true;
            }
        }
        private bool state = false;
        void GenerateElementsNums()
        {
            Random rnd = new Random();
            while (state)
            {
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    elemsNumsListBox.Items.Add(rnd.Next(1,10000));
                }));
                    Thread.Sleep(500);
            }
        }
        void GenerateElementsChars()
        {
            while (state)
            {
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    elemsCharListBox.Items.Add(GenerateSymbol());
                }));
                    Thread.Sleep(500);
            }
        }
        static char GenerateSymbol() {
            Random rnd = new Random();
            return (char)rnd.Next(97,123);
        }
        void GenerateElementsSymbols()
        {
            Random rnd = new Random();

            while (state)
            {
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    String word = String.Empty;
                    
                    for (int i = 0; i < rnd.Next(5,15); i++)
                    {
                        Thread.Sleep(1);
                        word += GenerateSymbol();
                    }
                    elemsSymbolsListBox.Items.Add(word);
                }));
                    Thread.Sleep(450);
            }
        }
        private void generateBtn_Click(object sender, RoutedEventArgs e)
        {
            state = !state ;
            if (state)
            {
                foreach (Thread item in threads)
                {
                    item.Start();
                }
            }
            //ThreadPool.QueueUserWorkItem(GenerateElementsNums, null);
            //ThreadPool.QueueUserWorkItem(GenerateElementsChars, null);
            //ThreadPool.QueueUserWorkItem(GenerateElementsSymbols, null);
            
        }

        private void priorityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (threadComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please, select item to set priority");
                return;
            }
            switch (threadComboBox.SelectedIndex)
            {
                case 0:
                    threads[0].Priority = priorityComboBox.SelectedItem as ThreadPriority;
                    break;
                default:
                    break;
            }

        }
    }
}
