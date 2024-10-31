namespace _34_Nesvetaylov_Project_Eva.NeuroNetwork
{
    class HiddenLayer: Layer// Потомок класса Layer
    {
        // конструктор
        public HiddenLayer(int non, int nopn, NeuronType nt, string type) : base(non, nopn, nt, type) { }


        public override void Recognize(Network net, Layer nextLayer)
        {
            double[] hidden_out = new double[Neurons.Length];

            for(int i=0;i<Neurons.Length;i++)
            {
                hidden_out[i] = Neurons[i].Output;
            }

            nextLayer.Data = hidden_out;
        }


        public override double [] BackwardPass(double[] gr_sums)
        {
            double[] gr_sum = new double[numofprevneurons];
            // Здесь пропишем код обучения нейронной сети

            return gr_sum;
        }


    }
}
