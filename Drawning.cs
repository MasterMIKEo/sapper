using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Threading;
using System.Xml.Linq;

namespace Saper
{
    public class Drawning
    {
        static Pen mypen = new Pen(Color.Black, 1);                    // стили рисования
        static Pen mypen2 = new Pen(Color.Black, 3);                   //
        static SolidBrush fonOpen = new SolidBrush(Color.Goldenrod);   //
        static SolidBrush flag = new SolidBrush(Color.Red);            //
        static SolidBrush fonClose = new SolidBrush(Color.GhostWhite); //

        static SolidBrush NullMines = new SolidBrush(Color.Gray); //цвет количества мин равное 0
        static SolidBrush OneOrTwoMines = new SolidBrush(Color.Black); //цвет количества мин равное 1,2
        static SolidBrush TreeAndMoreMines = new SolidBrush(Color.Red); //цвет количества мин равное 3 и более

        static Font MyFont = new Font("Arial", 25, FontStyle.Bold);

        public static void OpenField(PictureBox pictureBox1, int xShift, int yShift, int xPole, int yPole, int[,] indicate)
        {
            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);

            g.DrawRectangle(mypen, xShift, yShift, 46, 46);             //отрисовка открытого поля
            g.FillRectangle(fonOpen, xShift + 1, yShift + 1, 45, 45);   //
            string CountMines = indicate[xPole, yPole].ToString();
            SolidBrush ColorCountMines = new SolidBrush(Color.Gray);

            switch (indicate[xPole, yPole])
            {
                case 0:
                    ColorCountMines = NullMines;
                    break;
                case 1:
                    ColorCountMines = OneOrTwoMines;
                    break;
                case 2:
                    ColorCountMines = OneOrTwoMines;
                    break;
                case 3:
                    ColorCountMines = TreeAndMoreMines;
                    break;
                case 4:
                    ColorCountMines = TreeAndMoreMines;
                    break;
                case 5:
                    ColorCountMines = TreeAndMoreMines;
                    break;
                case 6:
                    ColorCountMines = TreeAndMoreMines;
                    break;
                case 7:
                    ColorCountMines = TreeAndMoreMines;
                    break;
                case 8:
                    ColorCountMines = TreeAndMoreMines;
                    break;
            }

            g.DrawString(CountMines, MyFont, ColorCountMines, xShift + 8, yShift + 5); //отрисовка количества мин в виде текста

        }


        public static void Mine(PictureBox pictureBox1, int xShift, int yShift, int xPole, int yPole)
        {
            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);

            g.DrawLine(mypen2, xShift + 15, yShift + 15, xShift + 35, yShift + 35); //отрисовка мины
            g.DrawLine(mypen2, xShift + 35, yShift + 15, xShift + 15, yShift + 35); //
            g.DrawLine(mypen2, xShift + 25, yShift + 12, xShift + 25, yShift + 38); //
            g.DrawLine(mypen2, xShift + 38, yShift + 25, xShift + 12, yShift + 25); //
            g.FillEllipse(flag, xShift + 15, yShift + 15, 20, 20);                  //
        }


        public static void Flag(PictureBox pictureBox1, int xShift, int yShift, int xPole, int yPole, bool[,] instFlag)
        {
            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);

            if (!instFlag[xPole, yPole])                             //флага нет?
            {
                g.FillRectangle(flag, xShift + 20, yShift + 20, 10, 10); //рисуем флаг
                g.DrawLine(mypen, xShift + 20, yShift + 20, xShift + 20, yShift + 40);
                instFlag[xPole, yPole] = true; //запись в массив о располложении флагов
            }

            else if (instFlag[xPole, yPole])                   //флаг есть?
            {
                g.FillRectangle(fonClose, xShift + 1, yShift + 1, 48, 48); //рисуем пустое поле
                instFlag[xPole, yPole] = false; //убрать запись в массиве о располложении флагов
            }
        }

        public static void StartGrid(PictureBox pictureBox1)
        {
            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);     //
            g.Clear(Color.GhostWhite);                              //очистка игрового поля

            for (int i = 0; i < 10; i++)                            //отрисовка новой сетки
                for (int j = 0; j < 10; j++)
                    g.DrawRectangle(mypen, j * 49 + j, i * 49 + i, 49, 49);
        }
    }
}
