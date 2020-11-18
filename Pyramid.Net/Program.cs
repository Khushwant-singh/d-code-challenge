using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pyramid.Net
{
    class Program
    {

        static void Main(string[] args)
        {
            string pyramidStructure = string.Empty;


            #region Read_Pyramid_Structure
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Pyramid.txt");
            if (File.Exists(filePath))
            {
                pyramidStructure = File.ReadAllText(filePath);
            }
            if (string.IsNullOrEmpty(pyramidStructure))
                pyramidStructure = StaticData.GetPyramidStructure();
            #endregion

            Console.WriteLine("Processing below Pyramid" + Environment.NewLine +  pyramidStructure);

            #region Get_Pyramid_All_Possible_Paths
            var pyramidExplorer = new PyramidExplorer();
            var pyramidPaths =  pyramidExplorer.Explore(pyramidStructure);
            #endregion

            Console.WriteLine($"{Environment.NewLine}Found {pyramidPaths?.Count()} paths.");
            Console.Read();
        }
    }
}
