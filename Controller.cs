using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    static class Controller
    {

        public static void DisplayByDate(Bookkeeping obj)
        {
            var display = from i in obj.data
                          orderby i.date
                          select i;

            Console.Write("Введите желаемую дату: ");
            int userdate = Convert.ToInt32(Console.ReadLine());

            if (userdate < 0)
            {
                ExccessDisplayDate err = new ExccessDisplayDate("ERROR!!!\n There is no negative year");
                throw err;
            }

            foreach (Document i in display)
            {
                if (i.date <= userdate)
                {
                    Console.WriteLine(i.name);
                }
            }
        }

        public static void CheckCount(Bookkeeping obj)
        {
            var check = from i in obj.data
                        orderby i.TypeDoc()
                        select i;
            int count = 0;
            foreach (Document i in check)
            {
                if (i.TypeDoc() == "Чек бумажный." || i.TypeDoc() == "Чек электронный.")
                {
                    count++;
                }
            }

            if (count == 0)
            {
                ExccessCheckCount err = new ExccessCheckCount("Error!!!\n There are no manuals in the library");
                throw err;
            }

            Console.WriteLine($"Количество чеков составляет: {count}.");
        }

        public static void CostInvoice(Bookkeeping obj)
        {
            var costInv = from i in obj.data
                          orderby i.cost
                          select i;

            int summ = 0;

            Console.Write("Введите тип документа: ");
            string user_o_name = Convert.ToString(Console.ReadLine());

            foreach (Document i in costInv)
            {
                if (i.name == user_o_name)
                {
                    summ += i.cost;
                }
            }

            if (summ < 0)
            {
                ExccessCostInvoice err = new ExccessCostInvoice("Error!!!\n Negative cost!!!");
                throw err;
            }

            Console.WriteLine($"Общая стоимость по типу: {summ}");
        }
    }
}