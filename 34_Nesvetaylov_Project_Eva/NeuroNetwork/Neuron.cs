using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace _34_Nesvetaylov_Project_Eva.NeuroNetwork
{ 
    internal class Neuron
    {
        private NeuronType _type; // тип нейрона
        private double[] _weights; // его веса
        private double[] _inputs; // его входы
        private double _output; // его выходы
        private double _derivative; // производная ф-ии активации

        private double a = 0.01d;

        public double[] Weights
        {
            get => _weights;
            set => _weights = value;
        }

        public double[] Inputs
        {
            get => _inputs;
            set => _inputs = value;
        }

        public double Output => _output;
        public double Derivative => _derivative;

        public Neuron(double[] weights, NeuronType type)
        {
            _type = type;
            _weights = weights;
        }
        // Функция активации
        public void Activator(double[] i, double[] w)
        {
            double sum = 0;
            for (int j = 0; j < i.Length; j++)
            {
                sum += i[j] * w[j + 1]; // j + 1, потому что w[0] - смещение
            }
            sum += w[0]; // добавляем смещение

            _output = 1 / (1 + Exp(-sum)); // логистическая функция активации
            _derivative = _output * (1 - _output); // производная логистической функции активации

            _inputs = i;
            _weights = w;
        }
    }
}
