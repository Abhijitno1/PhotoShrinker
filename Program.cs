using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoShrinker
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputFolderPath = ConfigurationManager.AppSettings["inputFolder"];
            string outputFolderPath = ConfigurationManager.AppSettings["outputFolder"];

            ImageResizer.ShrinkImagesInFolder(inputFolderPath, outputFolderPath);
            Console.WriteLine("Done");
        }

    }
}
