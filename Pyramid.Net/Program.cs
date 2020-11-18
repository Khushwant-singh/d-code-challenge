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


            #region Paint_Output_Console
            int pyramidPathsCount = pyramidPaths.Count();
            Console.WriteLine($"{Environment.NewLine}Found {pyramidPathsCount} paths. (Complete & Incomplete)");

            //Below are testing methods. Intentionally commented.
            //pyramidPaths.Select(s => s.Sum()).ToList().ForEach(s => Console.WriteLine("Sum of path is:" + s));
            //long maxSumOfPyramidPath = pyramidPaths.Max(p => p.Sum());
            
            var maximumSumPyramidPathIndexAndSum = pyramidPaths.Select((n, i) => (Max: n.Sum(), Index: i)).Max();
            var maximumSumPyramidPath = pyramidPaths[maximumSumPyramidPathIndexAndSum.Index];
            
            Console.WriteLine($"{Environment.NewLine}Maximum sum of Pyramid path: {maximumSumPyramidPathIndexAndSum.Max}.");
            Console.WriteLine($"{Environment.NewLine}List of items from the maximum sum path:");
            maximumSumPyramidPath.ForEach(p => Console.Write($"{p} "));
            Console.Write($"= {maximumSumPyramidPathIndexAndSum.Max}");
            #endregion
            Console.Read();
        }
    }
}
