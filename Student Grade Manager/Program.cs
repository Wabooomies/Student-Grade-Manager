using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Student_Grade_Manager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string fileStudentGrades = "data/student_grades.csv";

            bool programLoop = true;

            while (programLoop)
            {
                HashSet<string> existingLines = new HashSet<string>();
                string[] lines = File.ReadAllLines(fileStudentGrades);
                foreach (string line in lines)
                    existingLines.Add(line);

                Console.Write("- - - Students Grade Manager - - -\n" +
                              "1. Add New Student Record\n" +
                              "2. Add New Student Records\n" +
                              "3. Export Student Record List\n" +
                              "4. Check and Export Student Record\n" +
                              "5. View Error Logs\n" +
                              "6. Exit\n\n" +
                              "Enter your choice: ");

                try
                {
                    string inputMenu = Console.ReadLine();
                    if (inputMenu == "")
                        throw new ArgumentException();
                    int userMenuChoice = int.Parse(inputMenu);
                    if (userMenuChoice > 6 || userMenuChoice < 1)
                        throw new IndexOutOfRangeException();

                    switch (userMenuChoice)
                    {
                        case 1:
                            Console.Write("\nSTUDENT ID: ");
                            string studentID = Console.ReadLine();
                            if (studentID == "")
                                throw new ArgumentException();

                            Console.Write("\nLAST NAME: ");
                            string studentLastName = Console.ReadLine();
                            if (studentLastName == "")
                                throw new ArgumentException();

                            Console.Write("\nFIRST NAME: ");
                            string studentFirstName = Console.ReadLine();
                            if (studentFirstName == "")
                                throw new ArgumentException();

                            string gradeOverflow = "Grades cannot be greater than 100 or less than 0.";

                            Console.Write("\nDATA STRUCTURES GRADE: ");
                            int gradeDataStructures = int.Parse(Console.ReadLine());
                            if (gradeDataStructures > 100 || gradeDataStructures < 0)
                                throw new OverflowException(gradeOverflow);

                            Console.Write("\nPROGRAMMING 2 GRADE: ");
                            int gradeProgramming2 = int.Parse(Console.ReadLine());
                            if (gradeProgramming2 > 100 || gradeProgramming2 < 0)
                                throw new OverflowException(gradeOverflow);

                            Console.Write("\nMATH APPLICATION IT GRADE: ");
                            int gradeMathApplicationIT = int.Parse(Console.ReadLine());
                            if (gradeMathApplicationIT > 100 || gradeMathApplicationIT < 0)
                                throw new OverflowException(gradeOverflow);

                            string studentRecord = $"{studentID},{studentLastName.ToUpper()},{studentFirstName.ToUpper()},{gradeDataStructures},{gradeProgramming2},{gradeMathApplicationIT}";
                            File.AppendAllText(fileStudentGrades, $"{studentRecord}\n");
                            Console.WriteLine("\nInformation has been added!");
                            break;

                        case 2:
                            Console.Write("\nFile Path: ");
                            string filePath = Console.ReadLine();
                            if (filePath == "")
                                throw new ArgumentException();

                            string[] filePathLines = File.ReadAllLines(filePath);
                            List<string[]> filePathLinesSplit = new List<string[]>();
                            List<string> filePathLinesUnsplit = new List<string>();

                            foreach (string line in filePathLines)
                                filePathLinesSplit.Add(line.Split(','));


                            foreach (string[] line in filePathLinesSplit)
                            {
                                string addLine = "";
                                for (int i = 0; i < line.Length; i++)
                                {
                                    if (i == 1 || i == 2)
                                        addLine += line[i].ToUpper();
                                    else
                                        addLine += line[i];
                                    if (i < line.Length - 1)
                                        addLine += ',';
                                }
                                filePathLinesUnsplit.Add(addLine);
                            }

                            foreach (string line in filePathLinesUnsplit)
                                Console.WriteLine(line);

                            File.AppendAllLines(fileStudentGrades, filePathLinesUnsplit);
                            break;

                        case 3:
                            foreach (string line in lines)
                                Console.WriteLine(line);

                            break;
                        case 4:
                            break;
                        case 5:
                            break;
                        case 6:
                            programLoop = false;
                            Console.WriteLine("\nYou have exited the program!");
                            break;
                    }
                }
                catch (ArgumentException e)
                {
                    string errorMessage = "Input cannot be empty.\n";
                    string errorLine = $"({DateTime.Now}) Empty Error - {errorMessage}";
                    File.AppendAllText("errorLog.txt", $"{errorLine}\n");
                    Console.WriteLine(e.Message);
                }
                catch (FormatException e)
                {
                    string errorLine = $"({DateTime.Now}) Format Error - {e.Message}";
                    File.AppendAllText("errorLog.txt", $"{errorLine}\n");
                    Console.WriteLine(e.Message);
                }
                catch (OverflowException e)
                {
                    string errorLine = $"({DateTime.Now}) Overflow Error - {e.Message}";
                    File.AppendAllText("errorLog.txt", $"{errorLine}\n");
                    Console.WriteLine(e.Message);
                }
                catch (IndexOutOfRangeException e)
                {
                    string errorLine = $"({DateTime.Now}) Index Out Of Range Error - {e.Message}";
                    File.AppendAllText("errorLog.txt", $"{errorLine}\n");
                    Console.WriteLine(e.Message);
                }
                catch (FileNotFoundException e)
                {
                    string errorLine = $"({DateTime.Now}) File Not Found Error - {e.Message}";
                    File.AppendAllText("errorLog.txt", $"{errorLine}\n");
                    Console.WriteLine(e.Message);
                }
                catch (IOException e)
                {
                    string errorLine = $"({DateTime.Now}) Input/Output Error - {e.Message}";
                    File.AppendAllText("errorLog.txt", $"{errorLine}\n");
                    Console.WriteLine(e.Message);
                }
                catch (UnauthorizedAccessException e)
                {
                    string errorLine = $"({DateTime.Now}) Unauthorized Access Error - {e.Message}";
                    File.AppendAllText("errorLog.txt", $"{errorLine}\n");
                    Console.WriteLine(e.Message);
                }

                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
