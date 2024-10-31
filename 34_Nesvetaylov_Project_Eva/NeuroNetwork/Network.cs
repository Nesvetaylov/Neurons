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
        private double e_error_avr;

        public double E_error_avr { }

    }
}
