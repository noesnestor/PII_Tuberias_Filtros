using System;
using CompAndDel.Pipes;
using CompAndDel.Filters;
using TwitterUCU;

namespace CompAndDel
{
    class Program
    {
        static void Main(string[] args)
        {
            var twitter = new TwitterImage();
            PipeNull pipeNull = new PipeNull();
            FilterNegative filterNegative = new FilterNegative();
            PipeSerial pipeSerial2 = new PipeSerial(filterNegative,pipeNull);
            PipeFork pipeFork = new PipeFork(pipeSerial2,pipeNull);
            FilterGreyscale filterGreyscale = new FilterGreyscale();
            PipeSerial pipeSerial= new PipeSerial(filterGreyscale,pipeFork);

            PictureProvider proveedor = new PictureProvider();
            IPicture imagen = proveedor.GetPicture(@"luke.jpg");
            

            proveedor.SavePicture(pipeSerial.Send(imagen),@"luke_distorted.jpg");
            proveedor.SavePicture(pipeFork.Send(imagen),@"luke_negative.jpg");
            twitter.PublishToTwitter("Mark Hamil",@"luke_distorted.jpg");
        }
    }
}
