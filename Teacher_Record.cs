using System;
using System.Collections.Generic;
using System.IO;

namespace Records_Assgn1
{


    class Teacher_data
    {
        public int id { get; set; }
        public string name { get; set; }
        public int clas { get; set; }
        public char sec { get; set; }
    }


    class Teacher_Record
    {
        static public void add_Record(Teacher_data new_data, List<Teacher_data> t_list)
        {
            Console.WriteLine("\nEnter the number of records you want to be entered..");
            int rcount = int.Parse(Console.ReadLine());
            for (int i = 0; i < rcount; i++)
            {

                Console.WriteLine("\nEnter the teacher's ID :");
                int tid = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter teacher's name :");
                string tname = Console.ReadLine();

                Console.WriteLine("Enter Class : ");
                int tclass = int.Parse(Console.ReadLine()); // reading class from user input

                Console.WriteLine("Enter Section :");
                char tsec = Console.ReadLine()[0];  // reading single char from user

                new_data = new Teacher_data();
                new_data.id = tid;
                new_data.name = tname;
                new_data.clas = tclass;
                new_data.sec = tsec;
                t_list.Add(new_data);
            }
        }

        static public void display_Record(Teacher_data new_data, List<Teacher_data> t_list)
        {
            for (int i = 0; i < t_list.Count; i++)

            {
                Console.WriteLine("\nTeacher ID = " + t_list[i].id);
                Console.WriteLine("Name = " + t_list[i].name);
                Console.WriteLine("Class = " + t_list[i].clas);
                Console.WriteLine("Section = " + t_list[i].sec + "\n");

            }
            if (t_list.Count < 1)
                Console.WriteLine("\n Alert... No data found\n\n");

        }


        static public void put_data_in_text_file( Teacher_data new_data, List<Teacher_data> t_list)
        {
            string file_path = @"E:\Teacher_File\teacher_records.txt";
            string record = "";

            for (int i = 0; i < t_list.Count; i++)
            {
                record += t_list[i].id + " " + t_list[i].name + " " + t_list[i].clas + " " + t_list[i].sec + "\n";
            }
            File.WriteAllText(file_path, record);
            Console.WriteLine("Data saved successfully in text file\n\n");

        }

        static public void convert_to_list(Teacher_data new_data, List<Teacher_data> t_list)
        {

            string file_path = @"E:\Teacher_File\teacher_records.txt";
            if (File.Exists(file_path))
            {

                string[] lines = File.ReadAllLines(file_path);  //reading lines from file
                string[] text_from_file;

                for (int i = 0; i < lines.Length; i++)
                {
                    text_from_file = lines[i].Split(' ');
                    new_data = new Teacher_data();
                    new_data.id = int.Parse(text_from_file[0]);
                    new_data.name = text_from_file[1];
                    new_data.clas = int.Parse(text_from_file[2]);
                    new_data.sec = text_from_file[3][0];
                    t_list.Add(new_data);

                }

            }

            else
                Console.WriteLine("\n File not found at location, A new file will be created\n");

        }

        static public void delete_records(List<Teacher_data> t_list)
        {
            Console.WriteLine("Enter the teacher's id for which record is to be deleted ");
            string del_id = Console.ReadLine();
            int del__id = int.Parse(del_id);
            int pos = t_list.FindIndex(i => i.id == del__id);
            if (pos > -1)
            {
                Console.WriteLine("Found record. Deleting. record..");
                t_list.RemoveAt(pos);

            }
            else
            {


                string file_path = @"E:\Teacher_File\teacher_records.txt";
                try
                {
                    List<string> file1 = new List<string>(System.IO.File.ReadAllLines(file_path));
                    List<string> file2 = new List<string>();
                    int flag = 0;
                    foreach (var record in file1)
                    {
                        if (record.StartsWith(del_id))
                        {
                            flag = 1;
                        }
                        else
                        {
                            file2.Add(record);
                            File.WriteAllLines(file_path, file2.ToArray());

                        }
                    }
                    if (flag == 0)
                    {
                        Console.WriteLine("\nInvalid Id entered\n\n");
                    }
                    else
                    {
                        Console.WriteLine("Record Found. Deleting record...\n\n");

                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception occured\n\n" + e.Message);

                }
            }

        }

        
        static public void update_record(List<Teacher_data> t_list)
        {
            Console.WriteLine("\nEnter id for which data to be updated");
            int u_id = int.Parse(Console.ReadLine());

            int pos = t_list.FindIndex(i => i.id == u_id);
            if (pos > -1)
            {
                Console.WriteLine("Enter choice to update for\n1. Name \n2. CLass\n3. Section");
                int ch = int.Parse(Console.ReadLine());

                switch (ch)
                {
                    case 1:
                        Console.WriteLine("Enter new name..");
                        string newname = Console.ReadLine();
                        t_list[pos].name = newname;
                        break;

                    case 2:
                        Console.WriteLine("Enter new class alloted..");
                        int newclass = int.Parse(Console.ReadLine());
                        t_list[pos].clas = newclass;
                        break;

                    case 3:
                        Console.WriteLine("Enter new section alloted..");
                        char newsection = Console.ReadLine()[0];
                        t_list[pos].sec = newsection;
                        break;

                    default:
                        Console.WriteLine("Wrong choice entered");
                        break;
                }
            }
            else
                Console.WriteLine("\n Index not found in records");
        }


       static void Main(string[] args)
        {
            
            List<Teacher_data> t_list = new List<Teacher_data>(); //creating a list of teachers where each index will hold an object of teacher

            Teacher_data new_data = null;

            convert_to_list(new_data, t_list);

            while(true)
            {
                Console.WriteLine(" Enter your choice \n1.Add a record \n2. Display records \n3. Save data to text file \n4. Update record \n5. Delete records \n6. Exit without saving");
                int key = int.Parse(Console.ReadLine());

                switch (key)
                {
                    case 1: add_Record(new_data, t_list);
                        break;

                    case 2: display_Record(new_data, t_list);
                        break;

                    case 3: put_data_in_text_file(new_data, t_list);
                        break;

                    case 4: update_record( t_list);
                        break;
                     
                    case 5: delete_records(t_list);
                             break;
                    case 6: break;

              
                }
                if (key == 6) //breaking out of while loop
                    break;
            }
           
        }


    }
}



