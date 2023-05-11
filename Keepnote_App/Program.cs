using System.Data;
using System.Data.SqlClient;

namespace Keepnote_App
{
    class Keepnote
    {
       static DataSet ds = new DataSet();
       static string query = "select*from keepnote";
        public static void Addnote(SqlConnection con)
        {
            SqlDataAdapter adp = new SqlDataAdapter(query, con);
            adp.Fill(ds);
            Console.WriteLine("Enter Id: ");
            int id=Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Title: ");
            string title=Console.ReadLine();
            Console.WriteLine("Enter Description: ");
            string desc = Console.ReadLine();
            DateTime date = DateTime.Now;
            var row = ds.Tables[0].NewRow();
            row["Id"] = id;
            row["Title"] = title;
            row["Description"] = desc;
            row["Date"]=date;
            ds.Tables[0].Rows.Add(row);
            SqlCommandBuilder cmd = new SqlCommandBuilder(adp);
            adp.Update(ds);
            Console.WriteLine("New note added successfully");
        }
        public static void Viewnote(SqlConnection con)
        {
            Console.WriteLine("Enter Id to view : ");
            int id = Convert.ToInt32(Console.ReadLine());
            SqlDataAdapter adp = new SqlDataAdapter($"select * from keepnote where Id={id} ", con);
            adp.Fill(ds);
            for(int i = 0; i < ds.Tables[0].Rows.Count;i++)
            {
                for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                {
                    Console.Write($"{ds.Tables[0].Rows[i][j]} \t");
                }
                Console.WriteLine();
            }
        }
        public static void Viewallnotes(SqlConnection con)
        {
            SqlDataAdapter adp = new SqlDataAdapter(query, con);
            adp.Fill(ds);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                {
                    Console.Write($"{ds.Tables[0].Rows[i][j]} \t");
                }
                Console.WriteLine();
            }
        }
        public static void Updatenote(SqlConnection con)
        {
            Console.WriteLine("Enter Id: ");
            int id= Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Title:");
            string title = Console.ReadLine();
            Console.WriteLine("Enter Description:");
            string desc = Console.ReadLine();
            SqlDataAdapter adp = new SqlDataAdapter(query, con);
            adp.Fill(ds);
            DataRow[] foundRows = ds.Tables[0].Select("ID = " + id);
            if (foundRows.Length == 1)
            {
                DataRow row = foundRows[0];
                row["Title"] = title;
                row["Description"] = desc;
            }
            SqlCommandBuilder cmd = new SqlCommandBuilder(adp);
            adp.Update(ds);
            Console.WriteLine("Note updated successfully");

        }
        public static void Deletenote(SqlConnection con)
        {
            Console.WriteLine("Enter Id: ");
            string id=Console.ReadLine();
            SqlDataAdapter adp = new SqlDataAdapter(query, con);
            adp.Fill(ds);
            for(int i=0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["Id"].ToString()==id)
                {
                    ds.Tables[0].Rows[i].Delete();
                    break;    
                }
            }
            SqlCommandBuilder cmd = new SqlCommandBuilder(adp);
            adp.Update(ds);
            Console.WriteLine("Note deleted successfully");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            string a = "";
            do
            {
                SqlConnection con = new SqlConnection("server=IN-8JRQ8S3; database=mydb;Integrated Security=true");
                con.Open();
                Console.WriteLine("Welcome to Take Note App");
                Console.WriteLine("1. Create Note");
                Console.WriteLine("2. View Note");
                Console.WriteLine("3. View All Notes");
                Console.WriteLine("4. Update Note");
                Console.WriteLine("5. Delete Note");

                int choice = Convert.ToInt16(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        {
                            Keepnote.Addnote(con);
                            break;
                        }
                    case 2:
                        {
                            Keepnote.Viewnote(con);
                            break;
                        }
                    case 3:
                        {
                            Keepnote.Viewallnotes(con);
                            break;
                        }
                    case 4:
                        {
                            Keepnote.Updatenote(con);
                            break;
                        }
                    case 5:
                        {
                            Keepnote.Deletenote(con);
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("you have entered wrong choice");
                            break;
                        }
                }
                Console.WriteLine("do you wish to continue (y/n");
                a= Console.ReadLine();
            } while (a.ToLower() == "y");

        }
    }
}