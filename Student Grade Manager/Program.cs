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
            string fileErrorLog = "data/errorLog.txt";
            string fileReportsFolder = "reports/";
            string errorMessage = "";
            string errorLine = "";
            bool programLoop = true;
            string[] fileLinesOfErrorLog = new string[0];
            string[] fileLinesOfStudentGrades = new string[0];
            

            while (programLoop)
            {
                string blockLocation = "";
                int sequencePart = 0;
                int userMenuChoice = 0;
                try
                {

                    blockLocation = "R";
                    if (File.Exists(fileErrorLog))
                    {
                        fileLinesOfErrorLog = File.ReadAllLines(fileErrorLog);
                    }
                    Console.WriteLine(File.Exists(fileStudentGrades));
                    if (File.Exists(fileStudentGrades))
                    {
                        fileLinesOfStudentGrades = File.ReadAllLines(fileStudentGrades);
                    }
                    else
                        File.AppendAllText(fileStudentGrades, "StudentID,LastName,FirstName,DataStructuresGrade,Programming2Grade,MathApplicationITGrade\n");

                    blockLocation = "M";
                    Console.WriteLine("- - - Students Grade Manager - - -");
                    List<string> menuOptions = new List<string>
                    { "Add New Student Record",
                      "Add New Student Records",
                      "Export Student Record List",
                      "Check and Export Student Record",
                      "View Error Logs",
                      "Exit" };

                    for (int i = 0; i < menuOptions.Count; i++)
                        Console.WriteLine($"{i + 1}. {menuOptions[i]}");
                    Console.Write("\nEnter the number of your desired option: ");

                    string inputMenu = Console.ReadLine();
                    if (inputMenu == "")
                        throw new ArgumentException();

                    userMenuChoice = int.Parse(inputMenu);
                    if (userMenuChoice > menuOptions.Count || userMenuChoice < 1)
                        throw new IndexOutOfRangeException();

                    bool idFound = false;

                    switch (userMenuChoice)
                    {
                        case 1:
                            Console.Write("\nSTUDENT ID: ");
                            string studentID = Console.ReadLine().ToUpper();
                            if (studentID == "")
                                throw new ArgumentException();

                            foreach (string line in fileLinesOfStudentGrades)
                            {
                                if (line.Contains(studentID))
                                {
                                    idFound = true;
                                    break;
                                }
                            }

                            if (idFound)
                            {
                                blockLocation += $"-C{Convert.ToString(userMenuChoice)}-S{Convert.ToString(sequencePart)}";
                                errorMessage = "Input is a non-unique ID.";
                                errorLine = $"({DateTime.Now}) Duplicate ID Error ({blockLocation}) - {errorMessage}";
                                File.AppendAllText(fileErrorLog, $"{errorLine}\n");
                                Console.WriteLine($"\n{errorMessage}");
                                break;
                            }

                            Console.Write("\nLAST NAME: ");
                            string studentLastName = Console.ReadLine().ToUpper();
                            if (studentLastName == "")
                                throw new ArgumentException();

                            Console.Write("\nFIRST NAME: ");
                            string studentFirstName = Console.ReadLine().ToUpper();
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

                            string studentRecord = $"{studentID.ToUpper()},{studentLastName.ToUpper()},{studentFirstName.ToUpper()},{gradeDataStructures},{gradeProgramming2},{gradeMathApplicationIT}";
                            Console.WriteLine($"\nSuccessfully added the following student record: {studentRecord}");
                            File.AppendAllText(fileStudentGrades, $"{studentRecord}\n");
                            break;

                        case 2:
                            Console.Write("\nTo get the file path:\n" +
                                          "(1) Find your .csv file\n" +
                                          "(2) Hold shift\n" +
                                          "(3) Right click the file\n" +
                                          "(4) Select \"Copy as path\"\n" +
                                          "\nFile Path: ");

                            string rawFilePath = Console.ReadLine();
                            string filePath = rawFilePath.Substring(1, rawFilePath.Length - 2);
                            if (rawFilePath == "")
                                throw new ArgumentException();

                            string[] filePathLines = File.ReadAllLines(filePath);
                            List<string[]> filePathLinesSplit = new List<string[]>();
                            List<string> filePathLinesUnsplit = new List<string>();

                            foreach (string line in filePathLines)
                                filePathLinesSplit.Add(line.Split(','));

                            foreach (string[] splittedLine in filePathLinesSplit)
                            {
                                foreach (string line in fileLinesOfStudentGrades)
                                {
                                    if (line.Contains(splittedLine[0]))
                                    {
                                        idFound = true;
                                        break;
                                    }
                                }
                                if (idFound)
                                    break;
                            }

                            if (idFound)
                            {
                                blockLocation += $"-C{Convert.ToString(userMenuChoice)}-S{Convert.ToString(sequencePart)}";
                                errorMessage = "File contains non-unique ID/s.";
                                errorLine = $"({DateTime.Now}) Duplicate ID Error ({blockLocation}) - {errorMessage}";
                                File.AppendAllText(fileErrorLog, $"{errorLine}\n");
                                Console.WriteLine($"\n{errorMessage}");
                                break;
                            }

                            foreach (string[] line in filePathLinesSplit)
                            {
                                string addLine = "";
                                for (int i = 0; i < line.Length; i++)
                                {
                                    if (i < 3)
                                        addLine += line[i].ToUpper();
                                    else
                                        addLine += line[i];
                                    if (i < line.Length - 1)
                                        addLine += ',';
                                }
                                filePathLinesUnsplit.Add(addLine);
                            }

                            Console.WriteLine("Successfully added the following student records:");
                            foreach (string line in filePathLinesUnsplit)
                                Console.WriteLine(line);

                            File.AppendAllLines(fileStudentGrades, filePathLinesUnsplit);
                            break;

                        case 3:
                            foreach (string line in fileLinesOfStudentGrades)
                                Console.WriteLine(line);

                            Console.Write("\nDo you want to export this file? (Y or N): ");

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
                catch (ArgumentException)
                {
                    blockLocation += $"-C{Convert.ToString(userMenuChoice)}-S{Convert.ToString(sequencePart)}";
                    errorMessage = "Input cannot be empty.";
                    errorLine = $"({DateTime.Now}) Empty Error ({blockLocation}) - {errorMessage}";
                    File.AppendAllText(fileErrorLog, $"{errorLine}\n");
                    Console.WriteLine($"\n{errorMessage}");
                }
                catch (FormatException e)
                {
                    blockLocation += $"-C{Convert.ToString(userMenuChoice)}-S{Convert.ToString(sequencePart)}";
                    errorLine = $"({DateTime.Now}) Format Error ({blockLocation}) - {e.Message}";
                    File.AppendAllText(fileErrorLog, $"{errorLine}\n");
                    Console.WriteLine($"\n{e.Message}");
                }
                catch (OverflowException e)
                {
                    blockLocation += $"-C{Convert.ToString(userMenuChoice)}-S{Convert.ToString(sequencePart)}";
                    errorLine = $"({DateTime.Now}) Overflow Error ({blockLocation}) - {e.Message}";
                    File.AppendAllText(fileErrorLog, $"{errorLine}\n");
                    Console.WriteLine($"\n{e.Message}");
                }
                catch (IndexOutOfRangeException e)
                {
                    blockLocation += $"-C{Convert.ToString(userMenuChoice)}-S{Convert.ToString(sequencePart)}";
                    errorLine = $"({DateTime.Now}) Index Out Of Range Error ({blockLocation}) - {e.Message}";
                    File.AppendAllText(fileErrorLog, $"{errorLine}\n");
                    Console.WriteLine($"\n{e.Message}");
                }
                catch (FileNotFoundException e)
                {
                    blockLocation += $"-C{Convert.ToString(userMenuChoice)}-S{Convert.ToString(sequencePart)}";
                    errorLine = $"({DateTime.Now}) File Not Found Error ({blockLocation}) - {e.Message}";
                    File.AppendAllText(fileErrorLog, $"{errorLine}\n");
                    Console.WriteLine($"\n{e.Message}");
                }
                catch (IOException e)
                {
                    blockLocation += $"-C{Convert.ToString(userMenuChoice)}-S{Convert.ToString(sequencePart)}";
                    errorLine = $"({DateTime.Now}) Input/Output Error ({blockLocation}) - {e.Message}";
                    File.AppendAllText(fileErrorLog, $"{errorLine}\n");
                    Console.WriteLine($"\n{e.Message}");
                }
                catch (UnauthorizedAccessException e)
                {
                    blockLocation += $"-C{Convert.ToString(userMenuChoice)}-S{Convert.ToString(sequencePart)}";
                    errorLine = $"({DateTime.Now}) Unauthorized Access Error ({blockLocation}) - {e.Message}";
                    File.AppendAllText(fileErrorLog, $"{errorLine}\n");
                    Console.WriteLine($"\n{e.Message}");
                }
                catch (Exception e)
                {
                    blockLocation += $"-C{Convert.ToString(userMenuChoice)}-S{Convert.ToString(sequencePart)}";
                    errorLine = $"({DateTime.Now}) Unknown Error ({blockLocation}) - {e.Message}";
                    File.AppendAllText(fileErrorLog, $"{errorLine}\n");
                    Console.WriteLine($"\n{e.Message}");
                }

                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
