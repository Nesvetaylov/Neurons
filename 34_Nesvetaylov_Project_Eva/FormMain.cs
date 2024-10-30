using System;
using System.IO;//используем классы чтобы работать с файлами
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _34_Nesvetaylov_Project_Eva
{
    public partial class FormMain : Form
    {
        private double[] inputpixels = new double[15] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        private void ChangeState(Button b, int index)
        {
            if (b.BackColor == Color.Black)
            {
                b.BackColor = Color.White;
                inputpixels[index] = 1d;
            }
            else if (b.BackColor == Color.White)
            {
                b.BackColor = Color.Black;
                inputpixels[index] = 0d;
            }
        }
        public FormMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChangeState(button1, 0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ChangeState(button2, 1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ChangeState(button3, 2);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ChangeState(button4, 3);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ChangeState(button5, 4);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ChangeState(button6, 5);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ChangeState(button7, 6);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ChangeState(button8, 7);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ChangeState(button9, 8);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            ChangeState(button10, 9);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            ChangeState(button11, 10);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            ChangeState(button12, 11);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            ChangeState(button13, 12);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            ChangeState(button14, 13);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            ChangeState(button15, 14);
        }


        private void SaveTrain(decimal vale, double[] input)
        {
            string PathDir; // Каталог с файлом обучающей выборки
            string NameFileTrain; // Имя файла обучающей выборки
            PathDir = AppDomain.CurrentDomain.BaseDirectory;//получаем папку с exe
            NameFileTrain = PathDir + "Train.txt";//Указали название файла
            string[] tmpstr = new string[1];//Временная строка
            tmpstr[0] = vale.ToString();
            for (int i = 0; i < 15; i++)
            {
                tmpstr[0] += input[i].ToString();
            }
            File.AppendAllLines(NameFileTrain, tmpstr);
        }

        private void SaveTestSample(decimal vale, double[] input)
        {
            string PathDir; // Каталог с файлом обучающей выборки
            string NameFileTestSample; // Имя файла обучающей выборки
            PathDir = AppDomain.CurrentDomain.BaseDirectory;//получаем папку с exe
            NameFileTestSample = PathDir + "TestSample.txt";//Указали название файла
            string[] tmpstr_2 = new string[1];//Временная строка
            tmpstr_2[0] = vale.ToString();
            for (int i = 0; i < 15; i++)
            {
                tmpstr_2[0] += input[i].ToString();
            }
            File.AppendAllLines(NameFileTestSample, tmpstr_2);
        }

        private void buttonSaveTrainSample_Click(object sender, EventArgs e)
        {
            SaveTrain(numericUpDownTrue.Value, inputpixels);
        }

        private void buttonSaveTestSample_Click(object sender, EventArgs e)
        {
            SaveTestSample(numericUpDownTrue.Value, inputpixels);
        }
    }
}
