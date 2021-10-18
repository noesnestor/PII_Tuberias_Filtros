using System;
using CompAndDel.Pipes;
using CompAndDel.Filters;

namespace CompAndDel
{
    class Program
    {
        static void Main(string[] args)
        {
            PipeNull pipeNull = new PipeNull();
            FilterNegative filterNegative = new FilterNegative();
            PipeSerial pipeSerial2 = new PipeSerial(filterNegative,pipeNull);
            FilterGreyscale filterGreyscale = new FilterGreyscale();
            PipeSerial pipeSerial= new PipeSerial(filterGreyscale,pipeSerial2);

            PictureProvider proveedor = new PictureProvider();
            IPicture imagen = proveedor.GetPicture(@"luke.jpg");
            
            proveedor.SavePicture(pipeSerial.Send(imagen),@"luke_distorted.jpg");
        }
    }
}
