using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    //Работу немного переделали прошлогоднюю, тут сразу Флойд.
    public partial class Form1 : Form 
    {
        public Point[] point = new Point[5];
        public int Kvo = 0, x, y;
        Graphics graph;
        Pen n = new Pen(Color.Red, 2);
        Font font = new Font("Arial", 12);
        public int[,] matrix, matrix1;
        public int con = 9999;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) //Создается матрица смежности, 
        {

            Random rand = new Random();
            for (int i = 0; i < Kvo; i++) 
            {
                dataGridView1.Columns.Add((i+1).ToString(),(i+1).ToString());
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
            }
            for (int i = 0; i < Kvo; i++) // Заполняется датагрид и рисуются ребра
            {
                for (int j = i; j < Kvo; j++)
                {
                    if (i != j)
                    {
                        int h = rand.Next(0, 5);
                        if (h > 1) { h = rand.Next(1,10); }
                        else { h = 0; }
                        dataGridView1.Rows[i].Cells[j].Value = h;
                        dataGridView1.Rows[j].Cells[i].Value = h;
                        if (h != 0) 
                        {
                            graph.DrawLine(n, point[i].X + 2, point[i].Y + 2, point[j].X + 2, point[j].Y + 2);
                        }
                    }
                    else { dataGridView1.Rows[i].Cells[j].Value = 0; }
                }
            }
            Vvod();
            Floyd();
            Vivod();
        }

        public void Floyd() // Флойд, который не нужен сейчас, но нужен.
        {
            matrix1 = new int[Kvo, Kvo];
            for (int k = 0; k < Kvo; k++)
            {
                for (int i = 0; i < Kvo; i++)
                {
                    for (int j = 0; j < Kvo; j++)
                    {
                        matrix[i,j] = Min(matrix[i,j], matrix[i,k] + matrix[k,j]);
                    }
                }
            }
        }
        public void Vvod() // из датагрид кидаем в обычную матрицу
        {
            matrix = new int[Kvo, Kvo];
            for (int i = 0; i < Kvo; i++)
            {
                for (int j = 0; j < Kvo; j++)
                {
                    if (Convert.ToInt32(dataGridView1.Rows[i].Cells[j].Value) == 0) matrix[i, j] = con;
                    else matrix[i, j] = Convert.ToInt32(dataGridView1.Rows[i].Cells[j].Value);
                }
            }
        }

        public void Vivod() // из  матрицы вывод в датагрид
        {
            for (int i = 0; i < Kvo; i++)
            {
                dataGridView2.Columns.Add((i + 1).ToString(), (i + 1).ToString());
                dataGridView2.Rows.Add();
                dataGridView2.Rows[i].HeaderCell.Value = (i + 1).ToString();
            }
            
            for (int i = 0; i < Kvo; i++)
            {
                for (int j = 0; j < Kvo; j++)
                {
                    if (matrix[i, j] == con || i == j) matrix[i,j] = 0;
                    dataGridView2.Rows[i].Cells[j].Value = matrix[i, j];
                }
            }
        }

        private void button2_Click(object sender, EventArgs e) // очищаем(Вы просили, мы сделали)
        {
            graph.Clear(Color.White);
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView2.Rows.Clear();
            dataGridView2.Columns.Clear();
            point = new Point[5];
            Kvo = 0;
        }

        public int Min(int x, int y) //Поиск минимума (это функция нужна для Флойдв, сделано тупо, но нам было лень:) )
        {
            if (x > y) return y;
            else return x;
        }


        private void pictureBox1_MouseMove(object sender, MouseEventArgs e) //координаты внутри картинки ищет
        {
            x = e.X; y = e.Y;
        }

        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e) //рисует точки
        {
            if (Kvo < 5)
            {
                point[Kvo] = new Point(x, y);
                graph = pictureBox1.CreateGraphics();
                n = new Pen(Color.Blue, 4);
                graph.DrawEllipse(n, point[Kvo].X, point[Kvo].Y, 4, 4);
                graph.DrawString((Kvo + 1).ToString(), font, Brushes.Black, point[Kvo].X - 15, point[Kvo].Y - 15);
                Kvo++;
            }
            else MessageBox.Show("Больше нельзя");
        }
    }
}