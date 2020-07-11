using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int x = 0; x < 100; x++)
            {
                int count = 5;
                var inputIds = new int[100];

                for (int y = 0; y < inputIds.Length; y++)
                {
                    inputIds[y] = y + 1;
                }

                var outputIds = new int[count];

                RandomFetch(inputIds, outputIds, count);


                StringBuilder sb = new StringBuilder();
                outputIds.ToList().ForEach((id) =>
                {
                    sb.Append(id.ToString() + "  ");
                });

                Console.WriteLine(sb.ToString());
            }

            Console.ReadLine();
        }

        static void RandomFetch(int[] inputIds, int[] outputIds, int count)
        {
            if (inputIds.Length == 0 || count == 0)
            {
                return;
            }

            if (count-- > 0)
            {
                var randomId = (new Random(Guid.NewGuid().ToString().GetHashCode())).Next(0, inputIds.Length - 1);

                outputIds[outputIds.Length - count - 1] = inputIds[randomId];

                if (randomId == 0)
                {
                    var newArray = new int[inputIds.Length - 1];
                    for (int i = 0; i < newArray.Length; i++)
                    {
                        newArray[i] = inputIds[i+1];
                    }
                    inputIds = newArray;
                }
                else
                {
                    var leftArray = new int[randomId];
                    for (int i = 0; i < randomId; i++)
                    {
                        leftArray[i] = inputIds[i];
                    }
                    var rightArray = new int[inputIds.Length - randomId - 1];
                    for (int i = randomId + 1; i < inputIds.Length; i++)
                    {
                        rightArray[i-randomId - 1] = inputIds[i];
                    }
                    //inputIds.CopyTo(rightArray, randomId+1);

                    var newArray = leftArray.Concat(rightArray).ToArray();

                    inputIds = newArray;
                }

                
            }

            if (inputIds.Count() > 0 && count > 0)
            {
                RandomFetch(inputIds, outputIds, count);
            }
            
        }


    }
}
