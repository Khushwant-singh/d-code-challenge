using Pyramid.Net.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Pyramid.Net
{
   internal class PyramidExplorer
    {

        private  List<List<int>> _pyramidPathsList = new List<List<int>>();
        private  int[][] _numericArrayPyramid;

        public List<List<int>> Explore(string pyramidStructure)
        {
            //Step 1 - Construct array
            #region Construct_Numeric_Array
            string[] pyramidArray = pyramidStructure.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            _numericArrayPyramid = new int[pyramidArray.Length][];
            for (int counter = 0; counter < pyramidArray.Length; counter++)
            {
                _numericArrayPyramid[counter] = Array.ConvertAll(
                                                    pyramidArray[counter].Split(" ".ToCharArray(),
                                                                    StringSplitOptions.RemoveEmptyEntries), item =>
                                                                    {
                                                                        int.TryParse(item, out int value);
                                                                        return value;
                                                                    }
                                                        );

            }
            #endregion

            //Step 2 - Simplify pyramid structure by constructing a TREE
            #region Construct_Simplified_Tree
            PyramidItem pyramidRoot = new PyramidItem();
            pyramidRoot.NumberValue = _numericArrayPyramid[0][0];
            pyramidRoot = ConstructTree(pyramidRoot, 0, 0);
            #endregion

            //Step 3 - Optional - Construct Json file
            #region Write_Tree_Json_At_Temp_Folder
            try
            {
                string pyramidTreeFilePath = Path.Combine(Path.GetTempPath(), "PyramidTree_" + DateTime.Now.ToString("ddMMMyyy HHmmss") + ".json");
                string jsonString = JsonSerializer.Serialize(pyramidRoot);
                File.WriteAllText(pyramidTreeFilePath, jsonString);
            }
            catch
            {
                //No action required. In case temp is not writable.
            }
            #endregion

            //Step 4 - Flatten Pyramid Tree
            #region Flat_Pyramid_Tree
            _pyramidPathsList.Clear();
            List<int> firstList = new List<int>();
            firstList.Add(pyramidRoot.NumberValue);
            _pyramidPathsList.Add(firstList);
            FlattenList(pyramidRoot, firstList);

            #endregion

            //Step 4- Return flat list
            return _pyramidPathsList;

        }


        #region Private_Construct_Tree
        private  PyramidItem ConstructTree(PyramidItem parent, int parentDepth, int parentIndex)
        {
            int childDepth = ++parentDepth;

            if (childDepth == _numericArrayPyramid.Length)
                return null;

            int[] childArray = _numericArrayPyramid[childDepth];

            for (int depthCounter = parentIndex; depthCounter <= parentIndex + 1; depthCounter++)
            {
                if (depthCounter > childArray.Length - 1)
                {
                    break;
                }
                if ((childArray[depthCounter] % 2 == 0) != (parent.isEven))
                {
                    PyramidItem item = new PyramidItem
                    {
                        NumberValue = childArray[depthCounter]
                    };
                    if (parent.Children == null)
                        parent.Children = new List<PyramidItem>();

                    parent.Children.Add(item);
                    ConstructTree(item, childDepth, depthCounter);

                }
            }

            return parent;
        }
        #endregion

        #region Private_Flatten_Pyramid_Tree
        private  void FlattenList(PyramidItem parent, List<int> parentList)
        {
            if (parent.Children?.Count > 0)
            {
                List<List<int>> cloneCollection = null;
                for (int counter = 1; counter < parent.Children?.Count; counter++)
                {
                    if (cloneCollection == null)
                        cloneCollection = new List<List<int>>();
                    cloneCollection.Add(CloneList(parentList));
                }
                for (int counter = 0; counter < parent.Children?.Count; counter++)
                {
                    if (counter == 0)
                    {
                        parentList.Add(parent.Children[counter].NumberValue);
                        FlattenList(parent.Children[counter], parentList);
                    }
                    else
                    {
                        var clonedList = cloneCollection[counter - 1];
                        clonedList.Add(parent.Children[counter].NumberValue);
                        _pyramidPathsList.Add(clonedList);
                        FlattenList(parent.Children[counter], clonedList);
                    }
                }

            }
        }

        private List<int> CloneList(List<int> sourceList)
        {
            List<int> clonedList = new List<int>();
            sourceList.ForEach(s => clonedList.Add(s));
            return clonedList;
        }

        #endregion
    }
}
