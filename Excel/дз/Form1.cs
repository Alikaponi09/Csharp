using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace дз
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Stack<string> contries = new Stack<string>(); // Создали стек
            contries.Push("Datch");
            contries.Push("Armenia");
            contries.Push("USA");
            contries.Push("Canada");//добавляем в начало стека элемент
            contries.Push("Norvey");

            List<string[]> books = new List<string[]>() { new string[] { "Письма незнакомке", "Приют Грез" }, new string[] { "Война миров", "Война и мир" }, new string[] { "Портрет Дориана Грея", "Дюна" }, new string[] { "Пятая Салли", "Три товарища" }, new string[] { "Цветы для Элджернона", "Доктор Сон" } }; //создали список

            if (contries.Count > books.Count) dataGridView1.RowCount = contries.Count;
            else dataGridView1.RowCount = books.Count;

            dataGridView1.ColumnCount = 3;
            dataGridView1.Columns[0].HeaderText = "Страна издания"; //дали названия столбацам
            dataGridView1.Columns[1].HeaderText = "Книги";
            dataGridView1.Columns[2].HeaderText = "Книги";

            int s = contries.Count; //количество элементов в стеке
            for (int i = 0; i < s;  i++) // заполнения из стека
            {
                dataGridView1.Rows[i].Cells[0].Value = contries.Pop();
            }

            for (int i = 0; i < books.Count; i++) // заполнения из списка
            {
                string[] ar = books[i];

                dataGridView1.Rows[i].Cells[1].Value = ar[0];
                dataGridView1.Rows[i].Cells[2].Value = ar[1];
            }
        }

        private void button2_Click(object sender, EventArgs e) // для переноса в эксель
        {
            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application(); 
            ExcelApp.Application.Workbooks.Add(Type.Missing);
            ExcelApp.Columns.ColumnWidth = 15;
            ExcelApp.Cells[1, 1] = "Страны издания";
            ExcelApp.Cells[1, 2] = "Книги";
            ExcelApp.Cells[1, 3] = "Книги";
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                for (int j = 0; j < dataGridView1.RowCount; j++)
                {
                    ExcelApp.Cells[j + 2, i + 1] = (dataGridView1[i, j].Value);
                }
            }
            if (MessageBox.Show("Открыть файл?", "Открытие", MessageBoxButtons.YesNo) == DialogResult.Yes) //для сохранения на доп оценку (Открывать ли файл?))
            {
                ExcelApp.Visible = true;
            }
            else ExcelApp.Visible = false;

            //if (MessageBox.Show("Сохранить файл?", "Сохранение", MessageBoxButtons.YesNo) == DialogResult.Yes) // сохранять ли файл ?
            //{
            //    SaveFileDialog save = new SaveFileDialog // создание диалогового окна с фильтром на все файлы
            //    {
            //        Filter = "All files (*.*)|*.*", 
            //        Title = "Сохранение файла"
            //    };
            //    if (save.ShowDialog() == DialogResult.OK)
            //    {
            //      ExcelApp.Application.Workbooks.Item[0].SaveAs(save.FileName);
            //        MessageBox.Show("Файл сохранен(поставьте доп.оценку:)");
            //    }
               
            //}

        }
        
    }
}

