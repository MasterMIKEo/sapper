using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Saper
{
    public class Drawning
    {

        public static void OpenField(PictureBox pictureBox1, int xShift, int yShift, int xPole, int yPole, int[,] indicate)
        {
            Pen mypen = new Pen(Color.Black, 1);                    // стили рисования
            Pen mypen2 = new Pen(Color.Black, 3);                   //
            SolidBrush fonOpen = new SolidBrush(Color.Goldenrod);   //
            SolidBrush flag = new SolidBrush(Color.Red);            //
            SolidBrush fonClose = new SolidBrush(Color.GhostWhite); //

            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);

            g.DrawRectangle(mypen, xShift, yShift, 46, 46);             //отрисовка открытого поля
            g.FillRectangle(fonOpen, xShift + 1, yShift + 1, 45, 45);   //

            for (int i = 0; i < indicate[xPole, yPole]; i++) //отрисовка количества мин вокруг этого поля
                g.DrawLine(mypen2, xShift + 10 + 5 * i, yShift + 15, xShift + 10 + 5 * i, yShift + 35);
        }
    }
}
