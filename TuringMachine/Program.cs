using System;
using System.IO;
using System.Text;

namespace TuringMachine
{
    internal class Program
    {
        public static void Main()
        {
            bool work = true;
            
            while (work)
            {


                Console.WriteLine("1) Открыть файл" +
                                  "\n2) Выйти");
                string point;
                point = Console.ReadLine();
                if (point == "1")
                {
                    file();
                }
                if (point == "2")
                {
                    work = false;
                }
                if (point!="1" && point!="2")
                {
                    Console.WriteLine("Wrong input");
                    Console.WriteLine("Print 1 or 2");
                }
            }
        }
        

        public static void file()
        {
            string file1, file ;
            Console.WriteLine("Введите имя файла: ");
            file1 = Console.ReadLine();
            file = Directory.GetCurrentDirectory(); 
            file = file + @"\" + file1;
            Console.WriteLine(file);
            if (File.Exists(file))
            {
                StreamReader reader = new StreamReader(file);
                int NumberOfCon;
                string str;
                string[] line1, line2, line3;
                char[] charr = new char[] {' '};
                NumberOfCon = Convert.ToInt32(reader.ReadLine());
                str = reader.ReadLine();
                line1 = str.Split(charr);
                str = reader.ReadLine();
                line2 = str.Split(charr);
                str = reader.ReadLine();
                line3 = str.Split(charr);
                reader.Close();
                /*Console.WriteLine(NumberOfCon);
                for (int i = 0; i < line1.Length; i++)
                {
                    Console.Write(line1[i]);
                }*/
                /* for (int i = 0; i < line2.Length; i++)
                 {
                     Console.WriteLine(line2[i]);
                 }*/
                /*for (int i = 0; i < line3.Length; i++)
                {
                    Console.Write(line3[i]);
                }*/
                matrix(line1, line2, line3, NumberOfCon);
            }
            else
            {
                Console.WriteLine("File not exist");
            }
        }

        public static void matrix( string[] line1, string[] line2, string[] line3, int Num)
        {
            string[,] C= new string[Num, Num+1];
            int k = 0;
            for (int i = 0; i < Num; i++)
            {
                for (int j = 0; j < Num+1; j++)
                {
                    
                    C[i, j] = line2[k];
                    k += 1;
                }
            }
            for (int i = 0; i < Num; i++)
            {
                Console.WriteLine("\n");
                for (int j = 0; j < Num+1; j++)
                {
                    Console.Write(" ");
                    Console.Write(C[i,j]);
                   
                    
                }
            }
            
            int steps=0;
            string step;
            Console.WriteLine("\nВведите максимальное число ходов"); 
            step = Console.ReadLine();
            bool IsInt = Int32.TryParse(step, out steps);
            while (IsInt!= true)
            {
                Console.WriteLine("Неверный ввод"); 
                Console.WriteLine("\nВведите максимальное число ходов"); 
                step = Console.ReadLine();
                IsInt = Int32.TryParse(step, out steps);
            }
            turn(C, line3, Num, steps);   
            
        }

        
        public static void turn(string[,] C, string[] line3, int Num, int steps)
        {
            int rule = 0;// текущее правило q1, q2, q3 и тд
            int pos = 0;// текущая позиция 0, 1, 2 и тд
            int exit = 1;
            int per;
            int l = 0;
            string[] output = new String[1];
            
            do
            {
                if (l> steps-1)
                {
                    exit = 0;
                }
               
                char simbol;
                string str;
                if (line3[pos]=="S")
                {
                    per = -1;
                }
                else
                {
                    per = Convert.ToInt32(line3[pos]);
                }
                
                str = C[rule, per+1];

                simbol = str[1];
               
                for (int i = 0; i < Num; i++)
                {
                    
                    if (simbol=='0')
                    {
                        exit=0;
                    }
                    if (simbol=='1')
                    {
                        rule = 0;
                    }
                    if (simbol=='2')
                    {
                        rule = 1;
                    }
                    if (simbol=='3')
                    {
                        rule = 2;
                    }
                    /*if (ii == Convert.ToString(simbol) )
                    {
                        rule = i ;
                    }*/
                    
                }
                
                simbol = str[2];
                /*Console.WriteLine("/n simbol= ");
                Console.WriteLine(simbol);*/
                line3[pos] = Convert.ToString(simbol);
                simbol = str[3];
                /*Console.WriteLine("/n simbol= ");
                Console.WriteLine(simbol);*/
                if (simbol=='R')
                {
                    pos += 1;
                    if (pos>= line3.Length)
                    {
                        Array.Resize(ref line3, line3.Length + 1);
                        line3[line3.Length-1] = "S";
                    }
                }
                if (simbol=='L')
                {
                    pos -= 1;
                    if (pos<0)
                    {
                       
                        string[] mas= new string[line3.Length];
                        for (int i = 0; i < line3.Length; i++)
                        {
                            mas[i] = line3[i];
                        }
                        Array.Resize(ref line3, line3.Length + 1);
                        for (int i = 0; i < line3.Length-1; i++)
                        {

                            line3[i+1] = mas[i];

                        }
                        line3[0] = "S";
                        pos = 0;
                    }
                }
                string output1= "";
                for (int i = 0; i < line3.Length; i++)
                {
                    
                    output1 += line3[i];
                }
                string a = Convert.ToString(l);
                output[l] = "Шаг: " + a + "|" + " Правило: " + str + "|" + " Ряд: " + output1;
                Array.Resize(ref output, output.Length + 1);
                l += 1;
            } while (exit!=0);
            Console.WriteLine("\n");
            for (int i = 0; i < output.Length; i++)
            {
                Console.WriteLine(output[i]);
            }
            while (true)
            {
                string b;
                Console.WriteLine("Хотите сохранить результат? y/n");
                b= Console.ReadLine();
                if (b== "y")
                {
                    
                    string puth= Directory.GetCurrentDirectory()+ @"\output.txt";
                    File.WriteAllLines(puth, output, Encoding.UTF8);
                    Console.WriteLine("Saved");
                    break;
                }
                if (b== "n")
                {
                    break; 
                }
                if(b!="y" && b!="n")
                {
                    Console.WriteLine("Введено неверно");
                } 
            }
            
        }

    }
}