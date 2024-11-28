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
            for(int j=0;j<numofprevneurons+1;j++)
            {
                double sum = 0;
                for(int k=0;k<numofneurons;k++)
                    sum += Neurons[k].Weights[j] * errors[k];
                gr_sum[j] = sum;
            }

            for(int i=0;i<numofneurons;i++)
                for(int n=0;n<numofprevneurons+1;n++)
                {
                    double deltaw;
                    if (n == 0)// если порог
                        deltaw = momentum * lastdeltaweights[i, 0] + learningrate * errors[i];
                    else
                        deltaw = momentum * lastdeltaweights[i, n] + learningrate * Neurons[i].Inputs[n - 1] * errors[i];

                    lastdeltaweights[i, n] = deltaw;
                    Neurons[i].Weights[n] += deltaw;//Коррекция весов
                }
            return gr_sum;
        }
    }
}
