﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace _34_Nesvetaylov_Project_Eva.NeuroNetwork
{
    abstract class Layer
    {
        // ПОЛЯ
        protected string Name_Layer; // Наименование слоя, которое используется
        string PathDirWeights; // Путь к каталогу, где находится файл
        string PathFileWeights; // Путь к файлу синаптических весов
        protected int numofneurons; // Число нейронов текущего слоя
        protected int numofprevneurons; // Число нейронов предыдущего слоя
        protected const double learningrate = 0.005d; // Скорость обучения
        protected const double momentum = 0.05d; // Момент инерции
        protected double[,] lastdeltaweights; // Веса предыдущего слоя
        Neuron[] _neurons; // Массив нейронов


        // СВОЙСТВА
        public Neuron[] Neurons { 
            get => _neurons; set => _neurons = value;
        } // Массив нейронного слоя
        public double[] Data
        {
            set
            {
                for(int i=0;i<Neurons.Length;i++)
                {
                    Neurons[i].Inputs = value;
                    Neurons[i].Activator(Neurons[i].Inputs, Neurons[i].Weights);
                }
            }
        }
        // КОНСТРУКТОР
        protected Layer(int non, int nopn, NeuronType nt,string nm_Layer)
        {
            numofneurons = non; // Кол-во нейронов текущего слоя
            numofprevneurons = nopn; // количество нейронов предыдущего слоя
            Neurons = new Neuron[non]; // Определение массива нейронов
            Name_Layer=nm_Layer; // Наименование слоя, которое...
            PathDirWeights = AppDomain.CurrentDomain.BaseDirectory + "memory\\";
            PathFileWeights = PathDirWeights + Name_Layer + "_memory.csv";
            double[,] Weights;
            if (File.Exists(PathFileWeights)) // Определяет, существует ли файл
            {
                Weights = WeightInitialize(MemoryMode.GET, PathFileWeights); // (Что хотим, куда)
            }
            else
            {
                Directory.CreateDirectory(PathDirWeights);
                Weights = WeightInitialize(MemoryMode.INIT, PathFileWeights);
            }
            lastdeltaweights = new double[non, nopn + 1];
            for (int i=0;i < non;i++) // Цикл формирования нейронного слоя
            {
                double[] tmp_weights = new double[nopn + 1];
                for(int j=0;j<nopn+1; j++)
                {
                    tmp_weights[j] = Weights[i, j];
                }
                Neurons[i] = new Neuron(tmp_weights, nt); // Создание нейрона
            }
        }
        // Метод работы с массивом синаптических весов слоя
        public double[,] WeightInitialize(MemoryMode mm, string path)
        {
            char[] delim = new char[] { ';', ' ' }; // Разделитель строк
            string tmpStr;
            string[] tmpStrWeights;
            double[,] weights = new double[numofneurons, numofprevneurons + 1];

            switch (mm)
            {
                case MemoryMode.GET:
                    tmpStrWeights = File.ReadAllLines(path); // Считывание строк текстового файла
                    string[] memory_element;
                    for (int i = 0; i < numofneurons; i++)
                    {
                        memory_element = tmpStrWeights[i].Split(delim); // Разбивает строку...
                        for (int j = 0; j < numofprevneurons + 1; j++)
                        {
                            weights[i, j] = double.Parse(memory_element[j].Replace(',', '.'),
                                System.Globalization.CultureInfo.InvariantCulture);
                        }

                    }
                    break;
                // Инициализация весов слоя
                case MemoryMode.INIT:
                    Random rand = new Random();
                    for (int i = 0; i < numofneurons; i++)
                    {
                        for (int j = 0; j < numofprevneurons + 1; j++)
                        {
                            weights[i, j] = rand.NextDouble() * 2 - 1; // Инициализация случайными числами от -1 до 1
                        }
                    }
                    break;

            }
            return weights; // возвращаем веса

        }
        abstract public void Recognize(Network net, Layer nextLayer);
        abstract public double[] BackwardPass(double[] stuff);
    }
    
}






















































// Сохранение весов
/*
 private void SaveWeights(string path, double[,] weights)
{
    using (StreamWriter writer = new StreamWriter(path))
    {
        for (int i = 0; i < numofneurons; i++)
        {
            for (int j = 0; j < numofprevneurons + 1; j++)
            {
                writer.Write(weights[i, j].ToString("F4", System.Globalization.CultureInfo.InvariantCulture) + ";");
            }
            writer.WriteLine();
        }
    }
}
 */