using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    class Bookkeeping
    {

        public List<Document> data = new List<Document>();

        public static int counter = 0;

        public void Output()
        {
            if (counter == 0)
            {
                ExccessBookkeepingEmpty err = new ExccessBookkeepingEmpty("ERROR!!!\n Bookkeeping is empty");
                throw err;
            }
            else
            {
                foreach (object i in data)
                {
                    Console.WriteLine($"Объект {i}");
                }
            }
        }

        public void Remove(Document A)
        {
            data.Remove(A);
        }

        public void ClearLibrary()
        {
            if (counter == 0)
            {
                ExccessBookkeepingEmpty err = new ExccessBookkeepingEmpty("ERROR!!!\n Bookkeeping is empty");
                throw err;
            }
            else
            {
                counter = 0;
                data.Clear();
                Console.WriteLine("Бухгалтерия очищена!");

            }
        }

        public void Push(Document N)
        {
            data.Add(N);
            counter++;
        }
    }
}