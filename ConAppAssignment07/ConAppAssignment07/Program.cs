using System;
using System.Data.SqlClient;
using System.Data;

namespace Assignment07
{
    internal class Program
    {
        static SqlConnection con;
        static SqlCommand cmd;
        static SqlDataAdapter adapter;
        static DataSet ds;
        static string conString = "server= DESKTOP-N29NEU1;database=Assignment7Db; trusted_connection = true;";

        static void LoadData()
        {
            con = new SqlConnection(conString);
            cmd = new SqlCommand("Select * From Books", con);
            con.Open();
            adapter = new SqlDataAdapter(cmd);
            ds = new DataSet();
            adapter.Fill(ds, "Books");
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            con.Close();
        }
        static void Display()
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Console.WriteLine("\n");
                Console.WriteLine("BookID : " + ds.Tables[0].Rows[i]["BookId"]);
                Console.WriteLine("Title : " + ds.Tables[0].Rows[i]["Title"]);
                Console.WriteLine("Author : " + ds.Tables[0].Rows[i]["Author"]);
                Console.WriteLine("Genre : " + ds.Tables[0].Rows[i]["Genre"]);
                Console.WriteLine("Quantity : " + ds.Tables[0].Rows[i]["Quantity"]);
                Console.WriteLine("\n");
            }
        }

        static void Add()
        {
            DataRow newRow = ds.Tables["Books"].NewRow();
            Console.WriteLine("Enter Book Id");
            newRow["BookId"] = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter Title");
            newRow["Title"] = Console.ReadLine();
            Console.WriteLine("Enter Author");
            newRow["Author"] = Console.ReadLine();
            Console.WriteLine("Enter Genre");
            newRow["Genre"] = Console.ReadLine();
            Console.WriteLine("Enter Quantity");
            newRow["Quantity"] = int.Parse(Console.ReadLine());
            ds.Tables["Books"].Rows.Add(newRow);
            adapter.Update(ds, "Books");
            Console.WriteLine("Added!!!\n\n");

        }
        static void Update()
        {
            DataTable dt = ds.Tables[0];
            DataRow dr = null;
            Console.WriteLine("Enter Book id to update book quantity");
            int id = int.Parse(Console.ReadLine());
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if ((int)dt.Rows[i]["BookId"] == id)
                {
                    dr = dt.Rows[i];
                    break;
                }
            }
            if (dr != null)
            {
                Console.WriteLine("Enter New Quantity");
                dr["Quantity"] = int.Parse(Console.ReadLine());
                adapter.Update(ds, "Books");
                Console.WriteLine("Quantity Updated!!\n");
            }
            else
            {
                Console.WriteLine("No Such Book Id found!!!");
            }
        }
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("1. Display Book Inventory");
                Console.WriteLine("2. Add New Book");
                Console.WriteLine("3. Update Book Quantity");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your choice: ");
                int op = int.Parse(Console.ReadLine());
                LoadData();

                switch (op)
                {
                    case 1:
                        Display();
                        break;
                    case 2:
                        Add();
                        break;
                    case 3:
                        Update();
                        break;
                    case 4:
                        Console.WriteLine("exit");
                        return;
                    default:
                        Console.WriteLine("Invalid choice!!!!.");
                        break;
                }
            }
        }
    }
}
