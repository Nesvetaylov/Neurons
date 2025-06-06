﻿using System;
using System.IO;

namespace _34_Nesvetaylov_Project_Eva.NeuroNetwork
{
    class InputLayer
    {
        // Поля
        private Random random = new Random(); //Рандом выборок
        private double[,] trainset = new double[100, 16]; // 100 изобр, желаемый отклик на 1 месте
        private double[,] testset = new double[10, 16];

        // Свойства
        public double[,] Trainset { get => trainset; }
        public double[,] Testset { get => testset; }

        // Конструктор
        public InputLayer(NetworkMode nm)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string[] tmpStr;
            string[] tmpArrStr;
            double[] tmpArr;

            switch(nm)
            {
                case NetworkMode.Train:
                    tmpArrStr = File.ReadAllLines(path + "train.txt");
                    for (int i =0;i<tmpArrStr.Length;i++)
                    {
                        tmpStr = tmpArrStr[i].Split();
                        tmpArr = new double[tmpStr.Length];
                        for (int j=0; j < tmpArrStr.Length;j++)
                        {
                            tmpArr[j] = double.Parse(tmpStr[j], System.Globalization.CultureInfo.InvariantCulture);
                        }
                    }
                    for (int n=trainset.GetLength(0)-1;n>=1;n--)
                    {
                        int j = random.Next(n + 1);
                        double[] temp = new double[trainset.GetLength(1)];
                        for (int i =0;i< trainset.GetLength(1); i++)
                        {
                            temp[i] = trainset[n, i];
                        }
                        for (int i=0; i<trainset.GetLength(1);i++)
                        {
                            trainset[n, i] = trainset[j, i];
                            trainset[j, i] = temp[i];
                        }
                    }
                    break;
            }
        }
        //Перетасовка строк массива методом Фишера-Йетса
        public void Shuffling_Array_Rows(double[,] arr)
        {
            int j;//Номер случайно выбранной строки
            Random random = new Random();
            double[] temp = new double[arr.GetLength(1)];//вспомогательный массив

            for(int n=arr.GetLength(0)-1;n>=1;n--)//Цикл перебора строк снизу вверх
            {
                j = random.Next(n + 1);//Выбор случайной строки из выше расположенных строк
                for(int i=0;i<arr.GetLength(1);i++)//Цикл копирования n-ой строки
                {
                    temp[i] = arr[n, i];
                }
                for(int i=0;i<arr.GetLength(1);i++)//Цикл перестановки двух строк
                {
                    arr[n, i] = arr[j, i];//заполнение n-ой строки значениями j-ой строки
                    arr[j, i] = temp[i];//заполнение j-ой строки значениями n-ой строки
                }
            }
        }
    }
}
