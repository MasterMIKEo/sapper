using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saper
{
    public class Mines
    {
        public static bool[,] RandomAdd() //метод для заполнения массива случайным расположением мин
        {
            bool[,] result = new bool[10, 10];
            Random Random = new Random();
            List<int> list = new List<int>();

            for (int i = 0; i < 20; i++)
            {
                int temp = Random.Next(99);

                if (!list.Contains(temp))
                {
                    list.Add(temp);
                    result[temp / 10, temp % 10] = true;
                    // случайное число это координата мины в виде массива
                }
                else
                    i--;
            }

            return result;
        }



        public static int[,] Indicator(bool[,] mines) //метод для заполнения массива данными о кол-ве мин вокруг каждого поля
        {
            int[,] result = new int[10, 10];

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    int K = 0; //переменная для количества мин

                    try { if (mines[i - 1, j - 1]) K++; } catch { }    //проверяем каждый элемент макссива
                    try { if (mines[i, j - 1]) K++; } catch { }        //вокруг "текущего" элемент
                    try { if (mines[i + 1, j - 1]) K++; } catch { } //
                    try { if (mines[i + 1, j]) K++; } catch { } //try..catch для всех "крайних" элементов
                    try { if (mines[i + 1, j + 1]) K++; } catch { } //
                    try { if (mines[i, j + 1]) K++; } catch { } //ловим исключение 
                    try { if (mines[i - 1, j + 1]) K++; } catch { } //о выходе за пределы массива
                    try { if (mines[i - 1, j]) K++; } catch { } //

                    result[i, j] = K;
                }
            }
            return result;
        }
    }
}
