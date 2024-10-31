namespace _34_Nesvetaylov_Project_Eva.NeuroNetwork
{
    class OutputLayer: Layer
    {
        public OutputLayer(int non, int nopn, NeuronType nt, string type) : base(non, nopn, nt, type) { }

        public override void Recognize(Network net, Layer nextLayer)
        {
            // Реализация функции softMax
            double e_sum = 0;
            for(int i=0;i<Neurons.Length;i++)
            {
                e_sum += Neurons[i].Output;
            }
            for(int i=0;i<Neurons.Length;i++)
            {
                net.fact[i] = Neurons[i].Output / e_sum; // Вероятность выхода
            }
        }
        public override double[] BackwardPass(double[] errors)
        {
            double[] gr_sum = new double[numofprevneurons + 1];
            // Код для обучения нейронной сети
            return gr_sum;
        }
    }
}
