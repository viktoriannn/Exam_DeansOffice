using Exam_Student;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ex
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int _index = 0;
        private int _count = 0;
        private readonly DeansOffice _deansOffice = new();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(SizeArr.Text, out int count) || count <= 0)
            {
                MessageBox.Show("Некорректный формат количества элементов");
                return;
            }
            if (_count > 0)
            {
                MessageBox.Show("Массив уже создан");
                return;
            }

            _count = count;
            _deansOffice.Students = new Student[count];

            LstUnsorted.ItemsSource = _deansOffice.Students;
            MessageBox.Show("Массив создан");
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (_deansOffice.Students == null)
            {
                MessageBox.Show("Массив еще не создан");
                return;
            }
            if (_index >= _count)
            {
                MessageBox.Show("Массив уже полностью заполнен");
                return;
            }
            if (string.IsNullOrWhiteSpace(BoxSurname.Text) || string.IsNullOrWhiteSpace(BoxName.Text) || string.IsNullOrWhiteSpace(BoxNumberBook.Text))
            {
                MessageBox.Show("Заполните все поля");
                return;
            }

            if (!int.TryParse(BoxNumberBook.Text, out int numberBook) || numberBook < 0)
            {
                MessageBox.Show("Неверный формат или значение зачетной книжки");
                return;
            }

            _deansOffice.Students[_index++] = new Student
            {
                Surname = BoxSurname.Text,
                Name = BoxName.Text,
                NumberBook = numberBook
            };

            LstUnsorted.ItemsSource = null;
            LstUnsorted.ItemsSource = _deansOffice.Students;

            BoxSurname.Clear();
            BoxName.Clear();
            BoxNumberBook.Clear();
        }

        private void BtnSortSave_Click(object sender, RoutedEventArgs e)
        {
            if (_deansOffice.Students == null || _index == 0)
            {
                MessageBox.Show("Нет данных для сортировки");
                return;
            }
            _deansOffice.Sort();

            LstSorted.ItemsSource = null;
            LstSorted.ItemsSource = _deansOffice.SortStudents;

            const string path = "students.txt";
            _deansOffice.SaveToFile(path);

            MessageBox.Show($"Данные также сохранены в файле {"students.txt"}");
        }
    }
}
