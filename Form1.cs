using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saper
{
    public partial class Form1 : Form
    {
        bool[,] instFlag = new bool[10, 10];  //массив для хранения позиций установленного
        bool[,] openField = new bool[10, 10]; //массив для хранения позиций открытых полей
        bool[,] mines = new bool[10, 10];     
        int[,] indicate = new int[10, 10];  //массив для хранения числового значения кол-ва мин вокруг каждого поля
        int winCount = 0;                   //счетчик для победы
        public int xShift = 0; //координаты сдвига
        public int yShift = 0; //для отрисовки элемента в поле

        Pen mypen = new Pen(Color.Black, 1);                    // стили рисования
        Pen mypen2 = new Pen(Color.Black, 3);                   //
        SolidBrush fonOpen = new SolidBrush(Color.Goldenrod);   //
        SolidBrush flag = new SolidBrush(Color.Red);            //
        SolidBrush fonClose = new SolidBrush(Color.GhostWhite); //


        public Form1()
        {
            InitializeComponent();
        }

        


        //оснвоная логика игры - нажатие кнопки мыши по полю pictureBox1
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            var xPole = e.X / 50; //координаты нажатия кнопки преобразуем в координаты поля (каждое поле 50х50 пикселей)
            var yPole = e.Y / 50; //нужны для взаимодействия с массивами

            xShift = xPole * 49 + xPole; //координаты сдвига
            yShift = yPole * 49 + yPole; //для отрисовки элемента в поле



            if (e.Button == MouseButtons.Left) // открываем поля
            {
                if (!instFlag[xPole, yPole] && !openField[xPole, yPole]) //проверка - нет ли на поле флага, и не открыто ли оно уже
                {
                    if (!mines[xPole, yPole]) //есть ли мина в этом поле
                    {
                        Drawning.OpenField(pictureBox1, xShift, yShift, xPole, yPole, indicate);

                        openField[xPole, yPole] = true; //запись в массив - поле открыто
                        winCount++;                 //на 1 шаг ближе к победе

                        if (winCount == 85)// открыли все чистые поля? - Победа
                        {
                            //процедура завершения игры
                            label1.Text = "Позравляю!";

                        }
                    }

                    else if (mines[xPole, yPole]) //наткнулись на мину Game over
                    {
                        Drawning.Mine(pictureBox1, xShift, yShift, xPole, yPole);

                        //процедура завершения игры
                        label1.Text = "Game over";
                      
                    }
                }

            }


            if (e.Button == MouseButtons.Right && !openField[xPole, yPole]) // ставим или убираем флаг           
                    Drawning.Flag(pictureBox1, xShift, yShift, xPole, yPole, instFlag);       
            

        }


        private void button1_Click(object sender, EventArgs e) //нажатие кнопки "Начать игру"
        {            


            mines = Mines.RandomAdd(); //случайное расположение мин из метода
            indicate = Mines.Indicator(mines); //заполнение массива данными о кол-ве мин вокруг каждого поля

            label1.Text = "";                                       //убрать надпись после предыдущей игры
            instFlag = new bool[10, 10];                             //обнуление массивов
            openField = new bool[10, 10];                            //
            winCount = 0;                                           //обнуление счетчиков


            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);     //
            g.Clear(Color.GhostWhite);                              //очистка игрового поля

            for (int i = 0; i < 10; i++)                            //отрисовка новой сетки
                for (int j = 0; j < 10; j++)
                    g.DrawRectangle(mypen, j * 49 + j, i * 49 + i, 49, 49);           
        }

        private void button2_Click(object sender, EventArgs e) //кнопка "наступить на поле"
        {

        }

        private void button3_Click(object sender, EventArgs e)//кнопка "флаг"
        {

        }

    }
}
