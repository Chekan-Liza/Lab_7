using System;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Lab_7
{
    public static class Printer
    {
        public static void iAmPrinting(Organization obj)
        {
            Console.WriteLine($"Это {obj.GetType()}");
        }
    }

    interface IConfirmed
    {
        bool DocConfirmed();
        bool NotConfirmed();
    }

    public abstract class Organization
    {
        public string O_Name { get; set; }
        public Organization(string o_name)
        {
            O_Name = o_name;
        }
    }

    public class Department : Organization
    {
        public string Departm_t { get; set; }
        public Department(string o_name, string dep) : base(o_name)
        {
            Departm_t = dep;
        }

        public void Display()
        {
            Console.WriteLine($"Отдел {Departm_t} организации \"{O_Name}\"");
        }
    }

    public abstract class Document : Department
    {
        public int date; //
        public int cost; //
        public string name; //

        public string Department { get; set; }

        public Document(string o_name, string dep, int date, int cost, string name) : base(o_name, dep)
        {
            Department = O_Name + ' ' + " (" + Departm_t + ')';
            this.date = date; //
            this.cost = cost; //
            this.name = name; //
        }

        public virtual string DepartmentName()
        {
            return Department;
        }

        public virtual string TypeDoc()
        {
            return "Документ."; //
        }

        protected bool IsConfirmed;
    }

    public class Receipt : Document, IConfirmed
    {

        public Receipt(string o_name, string dep, int date, int cost, string name) : base(o_name, dep, date, cost, name)
        {
            this.name = name; //
            this.date = date; //
            this.cost = cost; //
        }

        public bool DocConfirmed()
        {
            this.IsConfirmed = true;
            return true;
        }

        public bool NotConfirmed()
        {
            this.IsConfirmed = false;
            return true;
        }

        public override string TypeDoc()
        {
            if (this.IsConfirmed)
            {
                return "Бумажная квитанция"; //
            }
            return "Электронная квитанция";
        }

        public override string ToString()
        {
            if (this.IsConfirmed)
            {
                return "Квитанция оплачена.";
            }
            return "Квитанция не оплачена.";
        }

        public override string DepartmentName()
        {
            return base.DepartmentName();
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Квитанция: {this.name}, организация: {DepartmentName()}");
        } //
    }

    public partial class Invoice : Document, IConfirmed
    {


        public Invoice(string o_name, string dep, int date, int cost, string name) : base(o_name, dep, date, cost, name)
        {

            this.date = date; //
            this.cost = cost; //
            this.name = name; //
        }

        public bool NotConfirmed()
        {
            this.IsConfirmed = false;
            return true;
        }

        public override string TypeDoc()
        {
            if (this.IsConfirmed)
            {
                return "Накладная бумажная";
            }
            return "Накладная электронная"; //
        }

        public override string ToString()
        {
            if (this.IsConfirmed)
            {
                return "Накладная оплачена.";
            }
            return "Накладная не оплачена.";
        }

        public override string DepartmentName()
        {
            return base.DepartmentName();
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Накладная: {this.name}, организация: {DepartmentName()}");
        } //
    }

    sealed class Check : Receipt
    {
        public bool isManual = true; //
        public Check(string o_name, string dep, int year, int cost, string name) : base(o_name, dep, year, cost, name)
        {
            this.name = name; //
            this.date = year; //
            this.cost = cost; //
        }
        public override string TypeDoc()
        {
            if (this.IsConfirmed)
            {
                return "Чек бумажный.";
            }
            return "Чек электронный."; //
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            Document a = obj as Check;
            if (a as Check == null)
            {
                return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    enum MyEnum : int
    { 
        receipt,
        invoice,
        check
    }

    struct MyStruct
    { 
        public string TypeDocument;
        public string Department;

        public MyStruct(string TypeDocument, string Department)
        { 
            this.TypeDocument = TypeDocument;
            this.Department = Department;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Type: {TypeDocument} ||  number: {Department}");
        }
    }

    public class ExccessDisplayDate : Exception
    {
        public ExccessDisplayDate(string mes) : base() { }
    }

    public class ExccessCheckCount : Exception
    {
        public ExccessCheckCount(string mes) : base() { }
    }

    public class ExccessCostInvoice : Exception
    {
        public ExccessCostInvoice(string mes) : base() { }
    }

    public class ExccessBookkeepingEmpty : Exception
    {
        public ExccessBookkeepingEmpty(string mes) : base() { }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try { 
            Receipt Receipt_1 = new Receipt("Красная звезда", "финансовый", 2018, 300, "Receipt");
            Receipt_1.Display();
            Receipt_1.DisplayInfo();

            Console.WriteLine();

            Invoice Invoice_1 = new Invoice("Светлое будущее", "реализации", 2019, 355, "Invoice");
            Invoice_1.Display();
            Invoice_1.DisplayInfo();

            Console.WriteLine();

            Check Check_1 = new Check("Черная линия", "расчетный", 2020, 460, "Check");
            Check Check_2 = new Check("Черная линия", "расчетный", 2019, 290, "Check");
            Check_1.Display();
            Check_1.DisplayInfo();

            Console.WriteLine();

            if (Receipt_1 is Receipt)
            {
                Console.WriteLine("Объект Receipt_1 принадлежит классу Receipt.");
            }
            else
            {
                Console.WriteLine("Объект Receipt_1 не принадлежит классу Receipt.");
            }

            List<Organization> list = new List<Organization>() {
            Receipt_1, Invoice_1, Check_1, Check_2
            };

            foreach (Organization i in list)
            {
                Printer.iAmPrinting(i);
            }

            Bookkeeping lib = new Bookkeeping();

            lib.Push(Receipt_1);
            lib.Push(Invoice_1);
            lib.Push(Check_1);
            lib.Push(Check_2);
            lib.Output();


            Controller.DisplayByDate(lib);
            Controller.CheckCount(lib);
            Controller.CostInvoice(lib);

            Bookkeeping libr = new Bookkeeping();

            //libr.Output();//err_1
            //Controller.CheckCount( libr );//err_3

            Invoice Invoice_2 = new Invoice("Красная звезда", "финансовый", 2017, -1000, "Invoice");
            Invoice Invoice_3 = new Invoice("Черная линия", "расчетный", 2017, -1000, "Invoice");

            libr.Push(Invoice_2);
            libr.Push(Invoice_3);

            //Controller.CostInvoice( librrrrrrr );//err_4
            int zero = 0;
            // int x = 45454 / zero;

            Debug.Assert(zero > 0, "Переменная не больше 0.");

            //Exception exception = new Exception();
            //exception.

            }
            catch (ExccessBookkeepingEmpty err_1) {
                Console.WriteLine(err_1);
            }
            catch (ExccessDisplayDate err_2) {
                Console.WriteLine(err_2);
            }
            catch (ExccessCheckCount err_3) {
                Console.WriteLine(err_3);
            }
            catch (ExccessCostInvoice err_4) {
                Console.WriteLine(err_4);
            }
            catch (Exception err_5) {
                Console.WriteLine(err_5);
            }
            finally {
                Console.WriteLine("The End!");
            }

        } 
    } 
}