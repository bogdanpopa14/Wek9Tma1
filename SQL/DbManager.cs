using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQL
{
    public class DbManager
    {



        SqlConnection connection;
        //SqlCommand command;


        public void MaxYearBook()
        {
            try
            {
                var querry = "select * from Book where [Year]=(select max([Year]) from Book)";
                SqlCommand command = new SqlCommand(querry, connection);
                command.ExecuteNonQuery();
                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    var currentRow = dataReader;

                    var idB = currentRow["BookId"];
                    var title = currentRow["Title"];


                    Console.WriteLine($"BookID: {idB} Title: {title}");
                }

            }
            catch(SqlException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void BooksFromYear()
        {
            Console.WriteLine("introduceti anul din care doriti: ");
            int id = Convert.ToInt32(Console.ReadLine());
            SqlParameter idParam = new SqlParameter("@IdParam", id);
            try
            {
                var querry = "select * from Book where Year=@IdParam ";
                SqlCommand command = new SqlCommand(querry, connection);
                command.Parameters.Add(idParam);
                command.ExecuteNonQuery();
                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    var currentRow = dataReader;

                    var idB = currentRow["BookId"];
                    var title = currentRow["Title"];


                    Console.WriteLine($"BookID: {idB} Title: {title}");
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
        }

       

        public void SelectBook()
        {
            Console.WriteLine("introduceti un id: ");
            int id = Convert.ToInt32(Console.ReadLine());
            SqlParameter idParam = new SqlParameter("@IdParam", id);
            try
            {
                var querry = "select * from Book where BookId=@IdParam ";
                SqlCommand command = new SqlCommand(querry, connection);
                command.Parameters.Add(idParam);
                command.ExecuteNonQuery();
                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    var currentRow = dataReader;

                    var idB = currentRow["BookId"];
                    var title = currentRow["Title"];


                    Console.WriteLine($"{idB} title {title}");
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void DeleteBook()
        {
            Console.WriteLine("introduceti un id: ");
            int id = Convert.ToInt32(Console.ReadLine());
            SqlParameter idParam = new SqlParameter("@IdParam", id);
            try
            {
                var querry = "delete from Book where BookId=@IdParam ";
                SqlCommand command = new SqlCommand(querry, connection);
                command.Parameters.Add(idParam);
                command.ExecuteNonQuery();
               
            }
            catch(SqlException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void UpdateBook()
        {
            Console.WriteLine("introduceti un id: ");
            int id = Convert.ToInt32(Console.ReadLine());
            SqlParameter idParam = new SqlParameter("@IdParam", id);

            Console.WriteLine("introd un titlu: ");
            string title = Console.ReadLine();
            SqlParameter titleParam = new SqlParameter("@TitleParam", title);

            Console.WriteLine("introd un pub: ");
            string pub = Console.ReadLine();
            SqlParameter pubParam = new SqlParameter("@PubParam", pub);

            Console.WriteLine("introduceti un an ");
            int year = Convert.ToInt32(Console.ReadLine());
            SqlParameter yearParam = new SqlParameter("@YearParam", year);

            Console.WriteLine("introduceti un an ");
            int price = Convert.ToInt32(Console.ReadLine());
            SqlParameter priceParam = new SqlParameter("@PriceParam", price);




            try
            {
                var querry = "Update Book set Title=@TitleParam, PublisherId=@PubParam, Year=@YearParam, Price=@PriceParam where BookId=@IdParam";
                SqlCommand comand = new SqlCommand(querry, connection);
                comand.Parameters.Add(idParam);
                comand.Parameters.Add(titleParam);
                comand.Parameters.Add(pubParam);
                comand.Parameters.Add(yearParam);
                comand.Parameters.Add(priceParam);
                comand.ExecuteNonQuery();
            }

            catch(SqlException e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public void TotalPrice()
        {
            Console.WriteLine("introduceti nume publisher: ");
            string name = Console.ReadLine();
            SqlParameter nameParam = new SqlParameter("@NameParam", name);
            try
            {
                var querry = "select [Name] ,(select sum(Price) from Book where Publisher.PublisherId=Book.PublisherId) as Nr from Publisher where [Name] = @NameParam";
                SqlCommand comand = new SqlCommand(querry, connection);
                comand.Parameters.Add(nameParam);
                SqlDataReader dataReader = comand.ExecuteReader();
                while (dataReader.Read())
                {
                    var currentRow = dataReader;

                    var id = currentRow["Nr"];
                    

                    Console.WriteLine($"Total books price for {name} - {id}");
                }

            }
            catch(SqlException e)
            {
                Console.WriteLine(e.Message);
            }
        }


        public void NrBooks()
        {
            try
            {
                var querry = "select [Name] ,(select count(BookId) from Book where Publisher.PublisherId=Book.PublisherId) as Nr from Publisher";
                SqlCommand command = new SqlCommand(querry, connection);

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    var currentRow = dataReader;

                    var id = currentRow["Nr"];
                    var name = currentRow["Name"];

                    Console.WriteLine($"{name} - {id}");
                }
            }
            catch(SqlException e)
            {
                Console.WriteLine(e.Message);
            }
        }


        public void Close()
        {
            connection.Close();
        }

        //Select Top N
        public void SelectTopNfrom()
        {
            Console.WriteLine("introduceti coloana pe care o doriti: ");
            string name = Console.ReadLine();
            if(name=="Publisher")
            {
                TopPublisher();
            }
            else if(name=="Book")
            {
                TopBook();
            }
            else
            {
                Console.WriteLine("Column not exist");
            }

        }

        private void TopPublisher()
        {
            Console.WriteLine("introduceti cate randuri vreti: ");
            int nr = Convert.ToInt32(Console.ReadLine());
            SqlParameter nrParam = new SqlParameter("@NrParam", nr);
            try
            {
                var querry = $"select top {nr} * from Publisher";

                SqlCommand command = new SqlCommand(querry, connection);
                command.Parameters.Add(nrParam);
                command.ExecuteNonQuery();
                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    var currentRow = dataReader;

                    var id = currentRow["PublisherId"];
                    var name = currentRow["Name"];

                    Console.WriteLine($"{id} - {name}");
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void TopBook()
        {
            Console.WriteLine("introduceti cate randuri: ");
            int id = Convert.ToInt32(Console.ReadLine());
            SqlParameter idParam = new SqlParameter("@IdParam", id);
            try
            {
                var querry = $"select top {id} * from Book  ";
                SqlCommand command = new SqlCommand(querry, connection);
                command.Parameters.Add(idParam);
                command.ExecuteNonQuery();
                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    var currentRow = dataReader;

                    var idB = currentRow["BookId"];
                    var title = currentRow["Title"];
                    var price = currentRow["Price"];

                    Console.WriteLine($"{idB} title {title} price{price}");
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
        }




        //Connect
        public void Connect(string DbName)
        {
            string connectionString = $"Data Source=.;Initial Catalog={DbName};Integrated Security=True";
            connection = new SqlConnection(connectionString);
            connection.Open();
        }




        //Count
        public int CountRows()
        {
            int cnt = 0;
            try
            {
                string commandQuery = "select count(PublisherId) from Publisher";
                SqlCommand countCommand = new SqlCommand(commandQuery, connection);
                var count = countCommand.ExecuteScalar();
                Console.WriteLine(count);
               cnt= Convert.ToInt32(count);
            }

            catch(SqlException e)
            {
                Console.WriteLine(e.Message);
            }

            return cnt;
            

        }




        //Insert into
        public void InsertInto()
        {
            Console.WriteLine("Introduceti coloana in care vreti sa inserati: ");
            string name = Console.ReadLine();
            if (name == "Book")
            {
                InsertIntoBook();
            }
            else if (name == "Publisher")
            {
                InsertIntoPublisher();
            }
            else
            {
                Console.WriteLine("Column don't exist");
            }
        }


        private void InsertIntoBook()
        {
            Console.WriteLine("introd un titlu: ");
            string title = Console.ReadLine();
            SqlParameter titleParam = new SqlParameter("@TitleParam", title);

            Console.WriteLine("introd un pub: ");
            string pub = Console.ReadLine();
            SqlParameter pubParam = new SqlParameter("@PubParam", pub);

            Console.WriteLine("introduceti un an ");
            int year = Convert.ToInt32(Console.ReadLine());
            SqlParameter yearParam = new SqlParameter("@YearParam", year);

            Console.WriteLine("introduceti un pret ");
            int price = Convert.ToInt32(Console.ReadLine());
            SqlParameter priceParam = new SqlParameter("@PriceParam", price);




            try
            {
                var querry = "insert into Book values (@TitleParam, @PubParam, @YearParam, @PriceParam ); select scope_identity();";
                SqlCommand comand = new SqlCommand(querry, connection);
               
                comand.Parameters.Add(titleParam);
                comand.Parameters.Add(pubParam);
                comand.Parameters.Add(yearParam);
                comand.Parameters.Add(priceParam);
                comand.ExecuteNonQuery();
                var id = comand.ExecuteScalar();
                Console.WriteLine($"Book ID={id}");
            }

            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void InsertIntoPublisher()
        {
            Console.WriteLine("introduceti un id: ");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("introduceti nume publisher: ");
            string name = Console.ReadLine();

            SqlParameter idParam = new SqlParameter("@IdParam", id);
            SqlParameter nameParam = new SqlParameter("@NameParam", name);

            try
            {
                var comQuerry = "insert into Publisher values(@IdParam, @NameParam)";
                SqlCommand comand = new SqlCommand(comQuerry, connection);
                comand.Parameters.Add(idParam);
                comand.Parameters.Add(nameParam);
                comand.ExecuteNonQuery();
                //var iden = comand.ExecuteScalar();
                //Console.WriteLine(iden);


            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
