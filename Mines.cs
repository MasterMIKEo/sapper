using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saper
{
    public class Mines
    {
        public static int[,] RandomAdd() //метод для заполнения массива случайным расположением мин
        {
            int[,] result = new int[10, 10];
            Random Random = new Random();
            List<int> list = new List<int>(); //список случайных чисел

            for (int i = 0; i < 10; i++)
            {
                int temp = Random.Next(99); //случайное число

                if (!list.Contains(temp))
                {
                    list.Add(temp); //добавляем в список
                    result[temp / 10, temp % 10] = 1; // координата мины в массиве
                }
                else
                    i--;
            }

            return result;
        }



        public static int[,] Indicator(int[,] mines) //метод для заполнения массива данными о кол-ве мин вокруг каждого поля
        {
            int[,] result = new int[10, 10];

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    int K = 0; //переменная для количества мин

                    try { if (mines[i - 1, j - 1] == 1) K++; } catch { }    //проверяем каждый элемент макссива
                    try { if (mines[i, j - 1] == 1) K++; } catch { }        //вокруг "текущего" элемент
                    try { if (mines[i + 1, j - 1] == 1) K++; } catch { } //
                    try { if (mines[i + 1, j] == 1) K++; } catch { } //try..catch для всех "крайних" элементов
                    try { if (mines[i + 1, j + 1] == 1) K++; } catch { } //
                    try { if (mines[i, j + 1] == 1) K++; } catch { } //ловим исключение 
                    try { if (mines[i - 1, j + 1] == 1) K++; } catch { } //о выходе за пределы массива
                    try { if (mines[i - 1, j] == 1) K++; } catch { } //

                    result[i, j] = K;
                }
            }
            return result;
        }
    }
}
