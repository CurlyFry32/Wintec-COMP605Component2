using System.Reflection.Metadata;
using static System.Console;

namespace COMP605Component2
{
    internal class Program
    {
        private static dblDictionary dblDict = new dblDictionary();
        static void Main(string[] args)
        {
            Interface();
            WriteLine("");
            WriteLine("Press any key to continue...");
            ReadKey();
        }
        /* Add menu system with submenus
         * when loading files choose between files
         * ability to return to menu
         * 
         * Create a series of operations that demonstrate the data structure working correctly. These
         *  should cover insert, delete, find, and print operations with screen prompts providing
         *  feedback.
         */
        static void Interface()
        {
            int opt = 0; // initialise menu option
            bool isFinished = false; // boolean flag
            while ((opt < 1 || opt > 9) || !isFinished)
            {   //looking for valid option

                Clear();
                WriteLine("******** Menu ********");
                WriteLine();
                WriteLine("1: Load File");
                WriteLine("2: Insert Item");
                WriteLine("3: Remove Item");
                WriteLine("4: Find Item");
                WriteLine("5: Print");
                WriteLine("6: Demo");
                WriteLine();
                WriteLine("7: Exit");

                WriteLine("");
                WriteLine("Enter option: ");
                opt = int.Parse(ReadLine());

                if (opt >= 1 && opt <= 7)
                {
                    MenuOptionEval(opt);
                }
                if (opt == 7)
                {
                    //How to invoke the exit option using a boolean flag
                    isFinished = true;
                }
            }
        }

        static void MenuOptionEval(int opt)
        {
            switch (opt)
            {
                case 1:
                    dblDict.LoadFile();
                    menuReturn();
                    break;
                case 2:
                    Insert();
                    menuReturn();
                    break;
                case 3:
                    WriteLine("Insert word to delete: ");
                    String deleteWord = ReadLine();
                    dblDict.RemoveNode(deleteWord);
                    menuReturn();
                    break;
                case 4:
                    Find();
                    menuReturn();
                    break;
                case 5:
                    WriteLine(dblDict.ToPrint());
                    menuReturn();
                    break;
                case 6:
                    Demo();
                    menuReturn();
                    break;
                case 7:
                    WriteLine("Exiting...");
                    break;

                default:
                    // invalid menu option
                    WriteLine("Invalid menu option");
                    break;
            }
        }

        public static void Insert()//sub-menu
        {
            WriteLine("Word to Insert: " );
            String wordInsert = ReadLine();

            int opt = 0; // initialise menu option
            bool isFinished = false; // boolean flag

            while ((opt < 1 || opt > 5) || !isFinished)
            {   //valid option not selected

                Clear();
                WriteLine("******** Insert ********");
                WriteLine("1. Add to Front");
                WriteLine("2. Add to Rear");
                WriteLine("3. Add Before");
                WriteLine("4. Add After");
                WriteLine("");
                WriteLine("5. Menu");
                WriteLine();
                WriteLine("Choose Option: ");

                opt = int.Parse(ReadLine()); //choose option
            
                if (opt >= 1 && opt <= 5)
                {

                    //looking for valid option
                    switch (opt)
                    {
                        case 1://add start
                            dblDict.AddToFront(wordInsert);
                            break;
                        case 2://add end
                            dblDict.AddToRear(wordInsert);
                            break;
                        case 3://add before
                            WriteLine("Choose word to insert Before:");
                            String TargetBefore = ReadLine();
                            dblDict.AddBefore(wordInsert, TargetBefore);
                            break;
                        case 4://add after
                            WriteLine("Choose word to insert after:");
                            String TargetAfter = ReadLine();
                            dblDict.AddAfter(wordInsert, TargetAfter);
                            break;
                        case 5:
                            WriteLine("Exiting...");
                            break;

                        default:
                            // invalid menu option
                            WriteLine("Invalid menu option");
                            break;
                    }
                }
                if (opt == 5)
                {
                    //How to invoke the exit option using a boolean flag
                    isFinished = true;
                }
            }
        }

        public static void Find()
        {
            WriteLine("Insert word to Find: ");
            String findWord = ReadLine();
            int line =  dblDict.ToSearch(findWord);
            if (line >= 0)
            {
                WriteLine($"Word found at line: {line}");
            }
            else
            {
                WriteLine("Word Not Found...");
            }
        }

        public static void Demo()
        {
            dblDict.LoadFile();
            WriteLine("Continue...");
            ReadKey();
            Insert();
            WriteLine("Continue...");
            ReadKey();
            WriteLine("Insert word to delete: ");
            String deleteWord = ReadLine();
            dblDict.RemoveNode(deleteWord);
            WriteLine("Continue...");
            ReadKey();
            Find();
            WriteLine("Continue...");
            ReadKey();
            WriteLine(dblDict.ToPrint());
            WriteLine("Continue...");
            ReadKey();
        }

        public static void menuReturn()
        {   
            WriteLine("");
            WriteLine("Press any key to return to menu...");
            ReadKey();
        }
    }
}
