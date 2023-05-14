using HtmlAgilityPack;
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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ExamWPFTestingHtml
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public class TestingHtml
    {
        // Словарь для хранения частотности тегов
        public Dictionary<string, int> tagFrequency = new Dictionary<string, int>();
        public Dictionary<string, int> frequencyDictionary = new Dictionary<string, int>();
        // Загрузка HTML-страницы
        HtmlWeb web = new HtmlWeb();
        
        public void Parser(string url)
        { 
          // Загрузка HTML-страницы
          //HtmlWeb web = new HtmlWeb();

            HtmlDocument doc = web.Load(url);

            // Очистка словаря перед подсчётом частотности тегов
            tagFrequency.Clear();

            // Подсчёт частотности тегов
            foreach (var node in doc.DocumentNode.Descendants())
            {
                if (node.NodeType == HtmlNodeType.Element)
                {
                    string tagName = node.Name;

                    if (tagFrequency.ContainsKey(tagName))
                    {
                        tagFrequency[tagName]++;
                    }
                    else
                    {
                        tagFrequency.Add(tagName, 1);
                    }
                }
            }
        }

        public bool Save(string filePath)
        {
            // Сохранение отчёта в файл
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (KeyValuePair<string, int> pair in tagFrequency)
                {
                    writer.WriteLine($"<{pair.Key}> - {pair.Value}");
                }
                return true;
            }
            return false;
        }

        //  Метод вовращает Dictionary прочитанный их сохраненного файла
        public Dictionary<string, int> ReadFrequencyDictionaryFromFile(string filePath)
        {
         //Dictionary<string, int> frequencyDictionary = new Dictionary<string, int>();
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(  ':', ' ', '\n', (char)StringSplitOptions.RemoveEmptyEntries);

                        if (parts.Length == 4)
                        {
                            string tagName = parts[0];
                            int frequency = int.Parse(parts[3]);
                            frequencyDictionary.Add(tagName, frequency);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reading frequency dictionary from file: {ex.Message}");
            }
            // возвращаем полученный словарь
            return frequencyDictionary;
        }
    }


    public partial class MainWindow : Window
    {
        TestingHtml testingHtml = new TestingHtml();

        // Словарь для хранения частотности тегов
        public Dictionary<string, int> tagFrequency;
        

        public MainWindow()
        {
            InitializeComponent();
        }

        // Запуск парсинга полученного адреса
        private void GenerateReport_Click(object sender, RoutedEventArgs e)
        {

            // парсинг полученного адреса на колличество тегов
            testingHtml.Parser(urlTextBox.Text);

            testingHtml.ReadFrequencyDictionaryFromFile(@"C:\Temp\Test\TestHtml.txt");
            // Вывод колличества подсчитаных тегов в статусбар
            statusLabel.Content = $"Отчёт успешно сформирован. Всего тегов: {testingHtml.tagFrequency.Count}";

            // Вывод результатов в ListBox
            resultListBox.Items.Clear();
            foreach (KeyValuePair<string, int> pair in testingHtml.tagFrequency)
            {
                // вывод всех тегов в ListBox
                resultListBox.Items.Add($"<{pair.Key}> - {pair.Value}");
            }
        }

        // Созранение полученных результатов в файл
        public void SaveReport_Click(object sender, RoutedEventArgs e)
        {
            // Созранение полученных результатов парсера в файл
            testingHtml.Save(@"C:\Temp\Test\TestHtml.txt");

            // Вывод в статусбар пути сохраненного файла
            statusLabel.Content = $"Отчёт успешно сохранён в файл {@"C:\Temp\Test\TestHtml.txt"}";
        }


    }
}
