using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQL
{
    class Program
    {
        static void Main(string[] args)
        {
            DbManager manager1 = new DbManager();
            manager1.Connect("Week9Tema1");
            //manager1.InsertIntoPublisher();
            //manager1.CountRows();
            //manager1.SelectTopN(10);
            //manager1.NrBooks();
            //manager1.TotalPrice();
            //manager1.InsertInto();
            //manager1.SelectTopNfrom();
            //manager1.BooksFromYear();
            manager1.MaxYearBook();
            manager1.Close();
            Console.ReadLine();
        }


        
        
    }


}
