using System;

namespace _34_Nesvetaylov_Project_Eva.NeuroNetwork

{
    class Network
    {
        // Все слои сети
        private InputLayer input_Layer = null;
        private HiddenLayer hiden_Layer1 = new HiddenLayer(71, 15, NeuronType.Hidden, nameof(hiden_Layer1));
        private HiddenLayer hiden_Layer2 = new HiddenLayer(34, 71, NeuronType.Hidden, nameof(hiden_Layer2));
        private OutputLayer output_Layer = new OutputLayer(10, 34, NeuronType.Output, nameof(output_Layer));

        //Массив для хранения выхода сети
        public double[] fact = new double[10];

        // Среднее значение энергии ошибки эпохи обучения
        private double[] e_error_avr;

        public double[] E_error_avr { get => e_error_avr; set => e_error_avr = value; }

        public void ForwardPass(Network net, double[] netInput)
        {
            net.hiden_Layer1.Data = netInput;
            net.hiden_Layer1.Recognize(null, net.hiden_Layer2);
            net.hiden_Layer2.Recognize(null, net.output_Layer);
            net.output_Layer.Recognize(net, null);
        }
        //Метод для обучения
        public void Train(Network net)
        {
            int epoches = 70; // количество эпох обучения
            net.input_Layer = new InputLayer(NetworkMode.Train);// Инициализация входного слоя
            double tmpSumError;// Временная переменная суммы ошибки
            double[] erros;// Вектор(массив) сигнала ошибки выходного слоя
            double[] temp_gsums1;// Вектор градиента 1-го скрытого слоя
            double[] temp_gsums2;// Вектор градиента 2-го скрытого слоя

            e_error_avr = new double[epoches];
            for(int k=0;k<epoches;k++)
            {
                e_error_avr[k] = 0;
                net.input_Layer.Shuffling_Array_Rows(net.input_Layer.Trainset);//Перетасовка
                for(int i =0;i<net.input_Layer.Trainset.GetLength(0);i++)
                {
                    double[] tmpTrain = new double[15];
                    for (int j = 0; j < tmpTrain.Length; j++)
                        tmpTrain[j] = net.input_Layer.Trainset[i, j + 1];

                    //Прямой проход
                    ForwardPass(net, tmpTrain);//прямой проход обучающего образа


                    //вычисление ошибки по итерации
                    tmpSumError = 0;
                    erros = new double[net.fact.Length];
                    for(int x=0;x<erros.Length;x++)
                    {
                        if (x == net.input_Layer.Trainset[i, 0])
                            erros[x] = 1.0 - net.fact[x];
                        else
                            erros[x] = -net.fact[x];

                        tmpSumError += erros[x] * erros[x] / 2;
                    }
                    e_error_avr[k] += tmpSumError / erros.Length;

                    //Обратный проход и коррекция весов
                    temp_gsums2 = net.output_Layer.BackwardPass(erros);
                    temp_gsums1 = net.hiden_Layer2.BackwardPass(temp_gsums2);
                    net.hiden_Layer1.BackwardPass(temp_gsums1);
                }
                e_error_avr[k] /= net.input_Layer.Trainset.GetLength(0);
            }

        }

    }
}
