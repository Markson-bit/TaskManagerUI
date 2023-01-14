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
using System.IO;

namespace TaskManagerUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // String that contains path to: App location + task.txt file.
        string filePath = AppDomain.CurrentDomain.BaseDirectory + @"\tasks.txt"; 
        public MainWindow()
        {
            InitializeComponent();

            // Reading tasks from file and appending them to List box.
            TaskList.Items.Clear();
            string[] content = File.ReadAllLines(filePath);
            foreach (string inside in content)
            {
                TaskList.Items.Add(inside);
            }
        }

        // If Exit(X) button is pressed, closing application.
        private void ExitClick(object sender, RoutedEventArgs e)
        {
            Close();  
        }

        // If Minimize(-) button is pressed, minimizing application.
        private void MinimizeClick(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }


        // If Add button is pressed.
        private void AddClick(object sender, RoutedEventArgs e)
        {
            // Checking if Input box is empty.
            if (TaskBox.Text.Length > 0)
            {
                // Checking if string in Input box is same as one of these in List box. If yes, opening dialog window.
                if (TaskList.Items.Contains(TaskBox.Text))
                {
                    // Asking user, if wants to add same task as existing one. 
                    MessageBoxResult result = MessageBox.Show("Exactly same tasks already exist. Are you sure you want to add it again?", "Action needed.", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        // Adding task to tasklist and file tasks.txt.
                        TaskList.Items.Add(TaskBox.Text);
                        string content = TaskBox.Text + Environment.NewLine;
                        File.AppendAllText(filePath, content);
                        TaskBox.Clear();

                    }
                    else
                    {
                        // Clearing Input box when user do not want to add same task.
                        TaskBox.Clear();
                        return;
                    }
                }
                else
                {
                    // Adding task to tasklist and file tasks.txt.
                    TaskList.Items.Add(TaskBox.Text);
                    string content = TaskBox.Text + Environment.NewLine;
                    File.AppendAllText(filePath, content);
                    TaskBox.Clear();

                }
            }
        }

        // If Delete button is pressed.
        private void DelClick(object sender, RoutedEventArgs e)
        {
            // Making an array from lines of file.
            string[] inventoryData = File.ReadAllLines(filePath);
            // Making a list from components of array.
            List<string> inventoryDataList = inventoryData.ToList();

            // Removing from the file specific task that is converted to string.
            if (inventoryDataList.Remove(Convert.ToString(this.TaskList.SelectedItem)))
            {
                File.WriteAllLines(filePath, inventoryDataList.ToArray());
            }

            // Removing selected task from List box.
            if (this.TaskList.SelectedIndex != -1)
            {
                this.TaskList.Items.RemoveAt(this.TaskList.SelectedIndex);
            }
        }

        // Allows user to moving window.
        private void Window_mousedown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void DragOverExit(object sender, DragEventArgs e)
        {

        }
    }
}
