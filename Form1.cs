﻿using System;
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
        int[,] instFlag = new int[10, 10];  //массив для хранения позиций установленного флага 0 - нет 1 - есть
        int[,] openField = new int[10, 10]; //массив для хранения позиций открытых полей 0 - не трогали 1 - открыто
        bool[,] mines = new bool[10, 10];     //массив для хранения позиций мин 0 - чисто,  1 - мина
        int[,] indicate = new int[10, 10];  //массив для хранения числового значения кол-ва мин вокруг каждого поля
        int winCount = 0;                   //счетчик для победы
        int flagsCount = 10;                //счетчик установленных флагов

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
            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);

            var xPole = e.X / 50; //координаты нажатия кнопки преобразуем в координаты поля (каждое поле 50х50 пикселей)
            var yPole = e.Y / 50; //нужны для взаимодействия с массивами

            int xShift = xPole * 49 + xPole; //координаты сдвига
            int yShift = yPole * 49 + yPole; //для отрисовки элемента в поле




            // case 1: //режим - 1, ждем нажатия кнопки "начать игру"
            //   for (int i = 0; i < 10; i++) //эта отрисовка сетки необязательна если убрать косяк с отображением label1
            //     for (int j = 0; j < 10; j++)
            //       g.DrawRectangle(mypen, j * 49 + j, i * 49 + i, 49, 49);
            // label1.Text = "";
            // break;



            if (e.Button == MouseButtons.Left) //режим - 2, открываем поля
            {
                if (instFlag[xPole, yPole] == 0 && openField[xPole, yPole] == 0) //проверка - нет ли на поле флага, и не открыто ли оно уже
                {
                    if (!mines[xPole, yPole]) //есть ли мина в этом поле
                    {
                        g.DrawRectangle(mypen, xShift, yShift, 46, 46);             //отрисовка открытого поля
                        g.FillRectangle(fonOpen, xShift + 1, yShift + 1, 45, 45);   //

                        for (int i = 0; i < indicate[xPole, yPole]; i++) //отрисовка количества мин вокруг этого поля
                            g.DrawLine(mypen2, xShift + 10 + 5 * i, yShift + 15, xShift + 10 + 5 * i, yShift + 35);

                        openField[xPole, yPole] = 1; //запись в массив - поле открыто
                        winCount++;                 //на 1 шаг ближе к победе

                        if (winCount == 90)// открыли все чистые поля? - Победа
                        {
                            //процедура завершения игры
                            label1.Text = "Позравляю!\nТы Выиграл!!!";

                        }
                    }

                    else if (mines[xPole, yPole]) //наткнулись на мину Game over
                    {
                        g.DrawLine(mypen2, xShift + 15, yShift + 15, xShift + 35, yShift + 35); //отрисовка мины
                        g.DrawLine(mypen2, xShift + 35, yShift + 15, xShift + 15, yShift + 35); //
                        g.DrawLine(mypen2, xShift + 25, yShift + 12, xShift + 25, yShift + 38); //
                        g.DrawLine(mypen2, xShift + 38, yShift + 25, xShift + 12, yShift + 25); //
                        g.FillEllipse(flag, xShift + 15, yShift + 15, 20, 20);                  //

                        //процедура завершения игры
                        label1.Text = "Game over";
                      
                    }
                }

            }


            if (e.Button == MouseButtons.Right) // Режим - 3, ставим или убираем флаг123
            {
                if (openField[xPole, yPole] == 0) //проверка, если поле открыто (1) - ничего не далаем
                {
                    if (instFlag[xPole, yPole] == 0)                             //флага нет?
                    {
                        g.FillRectangle(flag, xShift + 20, yShift + 20, 10, 10); //рисуем флаг
                        g.DrawLine(mypen, xShift + 20, yShift + 20, xShift + 20, yShift + 40);
                        instFlag[xPole, yPole] = 1; //запись в массив о располложении флагов

                        flagsCount--;               //кол-во флагов
                    }

                    else if (instFlag[xPole, yPole] == 1)                   //флаг есть?
                    {
                        g.FillRectangle(fonClose, xShift + 1, yShift + 1, 48, 48); //рисуем пустое поле
                        instFlag[xPole, yPole] = 0; //убрать запись в массиве о располложении флагов

                        flagsCount++; //кол-во флагов

                    }
                }
            }
            

        }


        private void button1_Click(object sender, EventArgs e) //нажатие кнопки "Начать игру"
        {            


            mines = Mines.RandomAdd(); //случайное расположение мин из метода
            indicate = Mines.Indicator(mines); //заполнение массива данными о кол-ве мин вокруг каждого поля

            label1.Text = "";                                       //убрать надпись после предыдущей игры
            instFlag = new int[10, 10];                             //обнуление массивов
            openField = new int[10, 10];                            //
            winCount = 0;                                           //обнуление счетчиков
            flagsCount = 10;                                        //

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
