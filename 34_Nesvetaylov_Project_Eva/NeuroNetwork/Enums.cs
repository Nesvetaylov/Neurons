using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _34_Nesvetaylov_Project_Eva
{
    enum NeuronType
    {
        Hidden,
        Output
    }
    enum NetworkMode
    {
        Recognition,
        Testing,
        Learning
    }
    enum MemoryMode
    {
        GET, // чтоние
        SET, // сохранение
        INIT // инициализация
    }
}
