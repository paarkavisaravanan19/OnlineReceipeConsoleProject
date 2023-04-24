using Azure.Core;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineReceipesConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ONLINE RECEIPES ");
            Display();
        }

        public static void Display()
        {
            //initiate connection string for db
            string CONN_STRING = "Data Source=3SJ6SF2;Initial Catalog=OnlineReceipesDB;Integrated Security=True;Encrypt=False;";
            SqlConnection con = new SqlConnection(CONN_STRING);
            con.Open();
            SqlCommand cmdAdmin = con.CreateCommand();
            cmdAdmin.CommandText = $"select * from AdminTable;";
            SqlDataReader reader = cmdAdmin.ExecuteReader();
            string MailID = "";
            string Password = "";
            string EnteredPassword = "";
            string EnteredMailID = "";
            while (reader.Read())
            {
                MailID = reader.GetString(0);
                Password = reader.GetString(1);
            }
            Console.WriteLine("Enter your MailID:");
            EnteredMailID = Console.ReadLine();
            bool result = String.Equals(EnteredMailID, EnteredMailID);
            if (result == true)
            {
                Console.WriteLine("Enter Your Password");
                EnteredPassword = Console.ReadLine();
            }
            else
            {
                User();
            }
            bool PasswordResult = String.Equals(Password, EnteredPassword);
            if (PasswordResult == true)
            {
                Console.WriteLine("Hello ADMIN ! WELCOME");
                AdminCRUD();

            }
            else
            {
                User();
            }
            reader.Close();
            con.Close();

        }
        //Admin CRUD Operation
        public static void AdminCRUD()
        {
            string option = "";
            Console.WriteLine("1. View FoodItems ");
            Console.WriteLine("2. Insert FoodItems");
            Console.WriteLine("3. Update FoodItems ");
            Console.WriteLine("4. Remove FoodItems");

            string CONN_STRING = "Data Source=3SJ6SF2;Initial Catalog=OnlineReceipesDB;Integrated Security=True;Encrypt=False;";
            SqlConnection con = new SqlConnection(CONN_STRING);
            do
            {
                Console.WriteLine("Enter a number:");
                int num = Convert.ToInt32(Console.ReadLine());
                switch (num)
                {
                    case 1:
                        con.Open();
                        SqlCommand cmdAdminCRUD = con.CreateCommand();
                        cmdAdminCRUD.CommandText = $"select * from ReceipeTable";
                        SqlDataReader reader = cmdAdminCRUD.ExecuteReader();

                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader.GetString(0)} {reader.GetString(1)} {reader.GetDecimal(2)} ");
                        }
                        reader.Close();
                        con.Close();
                        break;
                    case 2:
                        Console.WriteLine("enter the code to be added!");
                        string code = Console.ReadLine();
                        Console.WriteLine("enter the menu");
                        string menu = Console.ReadLine();
                        Console.WriteLine("enter the price");
                        decimal price = Convert.ToDecimal(Console.ReadLine());
                        con.Open();
                        SqlCommand cmdInsert = new SqlCommand("InsertReceipe", con);
                        cmdInsert.CommandType = System.Data.CommandType.StoredProcedure;
                        cmdInsert.Parameters.Add("@code", SqlDbType.Text).Value = code;
                        cmdInsert.Parameters.Add("@menu", SqlDbType.Text).Value = menu;
                        cmdInsert.Parameters.Add("@price", SqlDbType.Decimal).Value = price;
                        SqlDataReader readerCase2 = cmdInsert.ExecuteReader();
                        while (readerCase2.Read())
                        {
                            Console.WriteLine($"{readerCase2.GetString(0)}{readerCase2.GetString(1)}{readerCase2.GetDecimal(2)}");
                        }
                        readerCase2.Close();
                        con.Close();
                        break;
                    case 3:
                        con.Open();
                        SqlCommand cmdCase3 = con.CreateCommand();
                        cmdCase3.CommandText = $"select * from ReceipeTable";
                        SqlDataReader readerCase3 = cmdCase3.ExecuteReader();
                        Console.WriteLine("**************ReceipeDetails Content**************");
                        while (readerCase3.Read())
                        {
                            Console.WriteLine($"{readerCase3.GetString(0)}{readerCase3.GetString(1)}{readerCase3.GetDecimal(2)}");
                        }
                        readerCase3.Close();
                        Console.WriteLine("enter the code to be updated!");
                        string codeUpdate = Console.ReadLine();
                        Console.WriteLine("enter the menu");
                        string menuUpdate = Console.ReadLine();
                        Console.WriteLine("enter the price");
                        decimal priceUpdate = Convert.ToDecimal(Console.ReadLine());
                        SqlCommand cmdCase3Update = new SqlCommand("UpdateReceipeDetails", con);
                        cmdCase3Update.CommandType = System.Data.CommandType.StoredProcedure;
                        cmdCase3Update.Parameters.Add("@code", SqlDbType.Text).Value = codeUpdate;
                        cmdCase3Update.Parameters.Add("@menu", SqlDbType.Text).Value = menuUpdate;
                        cmdCase3Update.Parameters.Add("@price", SqlDbType.Decimal).Value = priceUpdate;
                        SqlDataReader readerCase3Update = cmdCase3Update.ExecuteReader();
                        Console.WriteLine("*************Update Receipe Table************");
                        while (readerCase3Update.Read())
                        {
                            Console.WriteLine($"{readerCase3Update.GetString(0)}{readerCase3Update.GetString(1)}{readerCase3Update.GetDecimal(2)}");
                        }
                        readerCase3Update.Close();
                        break;
                    case 4:
                        Console.WriteLine("==================Remove Receipe================");
                        Console.WriteLine("enter code to delete ");
                        string codeDelete = Console.ReadLine();
                        SqlCommand cmdCase4 = new SqlCommand("DeletionReceipeDetails", con);
                        cmdCase4.CommandType = System.Data.CommandType.StoredProcedure;
                        cmdCase4.Parameters.Add("@code", SqlDbType.Text).Value = codeDelete;
                        SqlDataReader readerCase4 = cmdCase4.ExecuteReader();
                        while (readerCase4.Read())
                        {
                            Console.WriteLine($"{readerCase4.GetString(0)}{readerCase4.GetString(1)}{readerCase4.GetDecimal(2)}");
                        }
                        readerCase4.Close();
                        con.Close();
                        Console.WriteLine("It has been Deleted");
                        break;

                }
                Console.WriteLine("do you want to proceed ! yes or no");
                option = Console.ReadLine();
            } while (string.Equals("yes", option));

            if (string.Equals("no", option))
            {
                Console.WriteLine("ENDS!");
                Console.WriteLine("====================================");
            }
        }

        public static void User()
        {
            string CONN_STRING = "Data Source=3SJ6SF2;Initial Catalog=OnlineReceipesDB;Integrated Security=True;Encrypt=False;";
            SqlConnection con = new SqlConnection(CONN_STRING);
            con.Open();
            SqlCommand cmdAdminCRUD = con.CreateCommand();
            cmdAdminCRUD.CommandText = $"select menu,price from ReceipeTable";
            SqlDataReader reader = cmdAdminCRUD.ExecuteReader();
            //string[] usercode = new string[6];
            string[] usermenu = new string[6];
            decimal[] userprice = new decimal[6];
            int count = 0;
            while (reader.Read())
            {
                //usercode[count] = reader.GetString(0);
                usermenu[count] = reader.GetString(0);
                userprice[count] = reader.GetDecimal(1);
                count++;
                //Console.WriteLine($"{reader.GetString(0)} {reader.GetString(1)} {reader.GetDecimal(2)} ");
            }
            reader.Close();


            con.Close();
            string[] usercode = new string[6] { "F1", "F2", "F3", "F4", "F5", "F6" };
            string strprice = "";

            string transact = "N";
            do
            {

                Console.Clear();
                //display menu
                Console.WriteLine("Code".PadRight(10) + "Menu".PadRight(30) + "Price");
                for (int i = 0; i < usermenu.Length; i++)
                {
                    if (userprice[i] > 0) { strprice = userprice[i].ToString(); } else { strprice = ""; }
                    Console.WriteLine(  usercode[i].PadRight(10) + usermenu[i].PadRight(30) + strprice);
                }
                //creation order list
                string[] order_list = new string[1];
                int qty;
                string strQty;
                decimal subtotal = 0;
                string order;
                int code_index;
                int current_order_index = 0;
                decimal grand_total = 0;
                
                
                do
                {
                    //take orders
                    Console.Write("Enter menu code: ");
                    order = Console.ReadLine().ToUpper();
                    code_index = Array.IndexOf(usercode, order);
                    if (code_index < 0)
                    {
                        Console.WriteLine("Invalid code!!!!");
                    }
                    else
                    {
                        if (order != "F6")
                        {
                            do
                            {
                                Console.Write("Enter Qty: ");
                                strQty = Console.ReadLine();
                                if (int.TryParse(strQty, out qty) == false)
                                {
                                    Console.WriteLine("Invalid quantity value!!!");
                                }
                            }
                            while (int.TryParse(strQty, out qty) == false);

                            subtotal = userprice[code_index] * qty;
                            grand_total = grand_total + subtotal;
                            order_list[current_order_index] = order.PadRight(10) +  usermenu[code_index].Trim().PadRight(30) +
                                userprice[code_index].ToString().PadRight(10) + qty.ToString().PadRight(10) + subtotal.ToString().PadLeft(10);

                            Array.Resize(ref order_list, order_list.Length + 1);
                            current_order_index++;
                        }
                        else
                        {
                            if (grand_total == 0)
                            {
                                Environment.Exit(0);
                            }
                        }
                    }
                } while (order != "F6");



                decimal amount_tendered = 0;
                decimal change = 0;
                string str_amount;


                if (grand_total > 0)
                {
                    //display orders
                    Console.WriteLine("\nCode".PadRight(11) + "Menu".PadRight(30) + "Price".PadRight(10) + "Qty".PadRight(10) + "Sub Total".PadLeft(10));
                    for (int i = 0; i < order_list.Length; i++)
                    {
                        Console.WriteLine(order_list[i]);
                        Console.WriteLine();
                    }
                    string str_total = "Total Amount: " + grand_total.ToString("#,0.00");
                    Console.WriteLine(str_total.PadLeft(70));

                    //accept payment and compute change
                    do
                    {
                        do
                        {
                            Console.Write("\nEnter amount tendered: ");
                            str_amount = Console.ReadLine();
                        } while (decimal.TryParse(str_amount, out amount_tendered) == false);

                        if (Convert.ToDecimal(str_amount) < grand_total)
                        {
                            Console.WriteLine("Amount tendered must be greater than the total amount...");
                        }


                    } while (Convert.ToDecimal(str_amount) < grand_total);

                    change = amount_tendered - grand_total;
                    Console.WriteLine("Change: ".PadRight(23) + change.ToString("#,0.00"));
                }



                do
                {
                    Console.Write("\nAnother trasaction:(Y/N): ");
                    transact = Console.ReadLine().ToUpper();
                } while (transact != "Y" && transact != "N");



            } while (transact != "N");
            Console.WriteLine("Press any key to exit.....");


            Console.ReadKey();
        }
    }
}

