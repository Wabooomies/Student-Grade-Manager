using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace Student_Grade_Manager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string fileStudentGradesFolder = "data/";
            string fileReportsFolder = "reports/";
            string fileStudentGrades = "data/student_grades.csv";
            string fileErrorLog = "data/errorLog.txt";
            string errorMessage = "";
            string errorLine = "";
            bool programLoop = true;
            string[] fileLinesOfErrorLog = new string[0];
            string[] fileLinesOfStudentGrades = new string[0];


            while (programLoop)
            {
                string blockLocation = "";
                int sequencePart = 0;
                int casePart = 0;

                try
                {
                    blockLocation = "R";

                    sequencePart++;
                    if (!Directory.Exists(fileStudentGradesFolder))
                        Directory.CreateDirectory(fileStudentGradesFolder);
                    if (!Directory.Exists(fileReportsFolder))
                        Directory.CreateDirectory(fileReportsFolder);

                    sequencePart++;
                    if (File.Exists(fileErrorLog))
                        fileLinesOfErrorLog = File.ReadAllLines(fileErrorLog);
                    if (File.Exists(fileStudentGrades))
                        fileLinesOfStudentGrades = File.ReadAllLines(fileStudentGrades);
                    else
                        File.AppendAllText(fileStudentGrades, "StudentID,LastName,FirstName,DataStructuresGrade,Programming2Grade,MathApplicationITGrade\n");

                    blockLocation = "M";

                    sequencePart++;
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

                    int userMenuChoice = int.Parse(inputMenu);
                    if (userMenuChoice > menuOptions.Count || userMenuChoice < 1)
                        throw new IndexOutOfRangeException();

                    bool idFound = false;

                    sequencePart = 0;
                    switch (userMenuChoice)
                    {
                        case 1:
                            casePart = 1;

                            sequencePart++;
                            Console.Write("\nSTUDENT ID: ");
                            string studentID = Console.ReadLine().ToUpper();
                            if (studentID == "")
                                throw new ArgumentException();

                            sequencePart++;
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
                                blockLocation += $"-C{Convert.ToString(casePart)}-S{Convert.ToString(sequencePart)}";
                                errorMessage = "Input is a non-unique ID.";
                                errorLine = $"({DateTime.Now}) Duplicate ID Error ({blockLocation}) - {errorMessage}";
                                File.AppendAllText(fileErrorLog, $"{errorLine}\n");
                                Console.WriteLine($"\n{errorMessage}");
                                break;
                            }

                            sequencePart++;
                            Console.Write("\nLAST NAME: ");
                            string studentLastName = Console.ReadLine().ToUpper();
                            if (studentLastName == "")
                                throw new ArgumentException();

                            sequencePart++;
                            Console.Write("\nFIRST NAME: ");
                            string studentFirstName = Console.ReadLine().ToUpper();
                            if (studentFirstName == "")
                                throw new ArgumentException();

                            string gradeOverflow = "Grades cannot be greater than 100 or less than 0.";

                            sequencePart++;
                            Console.Write("\nDATA STRUCTURES GRADE: ");
                            decimal gradeDataStructures = decimal.Parse(Console.ReadLine());
                            if (gradeDataStructures > 100 || gradeDataStructures < 0)
                                throw new OverflowException(gradeOverflow);

                            sequencePart++;
                            Console.Write("\nPROGRAMMING 2 GRADE: ");
                            decimal gradeProgramming2 = decimal.Parse(Console.ReadLine());
                            if (gradeProgramming2 > 100 || gradeProgramming2 < 0)
                                throw new OverflowException(gradeOverflow);

                            sequencePart++;
                            Console.Write("\nMATH APPLICATION IT GRADE: ");
                            decimal gradeMathApplicationIT = decimal.Parse(Console.ReadLine());
                            if (gradeMathApplicationIT > 100 || gradeMathApplicationIT < 0)
                                throw new OverflowException(gradeOverflow);

                            sequencePart++;
                            string studentRecord = $"{studentID.ToUpper()},{studentLastName.ToUpper()},{studentFirstName.ToUpper()},{gradeDataStructures},{gradeProgramming2},{gradeMathApplicationIT}";
                            Console.WriteLine($"\nSuccessfully added the following student record: {studentRecord}");
                            File.AppendAllText(fileStudentGrades, $"{studentRecord}\n");
                            break;

                        case 2:
                            casePart = 2;

                            sequencePart++;
                            Console.Write("\nTo get the file path:\n" +
                                          "(1) - Find your .csv file\n" +
                                          "(2) - Hold shift\n" +
                                          "(3) - Right click the file\n" +
                                          "(4) - Select \"Copy as path\"\n" +
                                          "\nFile Path: ");
                            string rawFilePath = Console.ReadLine();
                            string filePath = "";
                            if (rawFilePath == "")
                                throw new ArgumentException();

                            sequencePart++;
                            if (rawFilePath.Contains(".csv") && rawFilePath[0] == '\"' && rawFilePath[rawFilePath.Length - 1] == '\"')
                                filePath = rawFilePath.Substring(1, rawFilePath.Length - 2);
                            else
                                throw new FormatException();

                            string[] filePathLines = File.ReadAllLines(filePath);
                            List<string[]> filePathLinesSplit = new List<string[]>();
                            List<string[]> fileStudentGradesSplit = new List<string[]>();

                            sequencePart++;
                            foreach (string line in fileLinesOfStudentGrades)
                                fileStudentGradesSplit.Add(line.Split(','));

                            sequencePart++;
                            foreach (string line in filePathLines)
                                filePathLinesSplit.Add(line.Split(','));

                            sequencePart++;
                            foreach (string[] line2 in filePathLinesSplit)
                            {
                                idFound = false;
                                string addLine = "";

                                sequencePart++;
                                foreach (string[] line1 in fileStudentGradesSplit)
                                {
                                    if (line1[0].ToUpper() == line2[0].ToUpper())
                                    {
                                        Console.WriteLine($"ID ({line2[0]}) is non-unique");
                                        idFound = true;
                                        break;
                                    }
                                }
                                if (idFound)
                                    continue;

                                sequencePart++;
                                for (int i = 0; i < line2.Length; i++)
                                {
                                    if (i < 3)
                                        addLine += line2[i].ToUpper();
                                    else
                                        addLine += line2[i];
                                    if (i < line2.Length - 1)
                                        addLine += ",";
                                }
                                Console.WriteLine($"Successfully added {addLine}");
                                File.AppendAllText(fileStudentGrades, $"{addLine}\n");
                            }
                            break;

                        case 3:
                            casePart = 3;

                            sequencePart++;
                            foreach (string line in fileLinesOfStudentGrades)
                                Console.WriteLine(line);
                            Console.Write("\nDo you want to export this file? (Press Y or N): ");
                            string fileExportChoice = Console.ReadLine();
                            if (fileExportChoice == "")
                                throw new ArgumentException();
                            if (fileExportChoice.Length > 1)
                                throw new OverflowException("Length of input is too long.");

                            sequencePart++;
                            if (fileExportChoice.ToUpper() == "Y")
                            {
                                fileReportsFolder = "reports/";
                                string fileExportName = $"Updated List of Student_{DateTime.Now.Year}{DateTime.Now.Month}{DateTime.Now.Day}";
                                if (File.Exists($"{fileReportsFolder}{fileExportName}.csv"))
                                    File.Delete($"{fileReportsFolder}{fileExportName}.csv");
                                File.AppendAllLines($"{fileReportsFolder}{fileExportName}.csv", fileLinesOfStudentGrades);
                                Console.WriteLine($"\nSuccessfully exported {fileReportsFolder}{fileExportName}.csv");
                            }
                            else if (fileExportChoice.ToUpper() == "N")
                            {
                                Console.WriteLine("\nYou have cancelled exporting the .csv file.");
                                break;
                            }
                            else
                            {
                                blockLocation += $"-C{Convert.ToString(casePart)}-S{Convert.ToString(sequencePart)}";
                                errorMessage = "Input was not Y or N.";
                                errorLine = $"({DateTime.Now}) Invalid Error ({blockLocation}) - {errorMessage}";
                                File.AppendAllText(fileErrorLog, $"{errorLine}\n");
                                Console.WriteLine($"\n{errorMessage}");
                                break;
                            }

                            break;
                        case 4:
                            casePart = 4;

                            sequencePart++;
                            Console.Write("\nSearch for Student via ID: ");
                            string searchStudentID = Console.ReadLine();
                            if (searchStudentID == "")
                                throw new ArgumentException();

                            sequencePart++;
                            foreach (string line in fileLinesOfStudentGrades)
                            {
                                string[] splitLine = line.Split(',');
                                if (splitLine[0] == searchStudentID.ToUpper())
                                {
                                    idFound = true;
                                    int dsaGrade = int.Parse(splitLine[3]);
                                    int progGrade = int.Parse(splitLine[4]);
                                    int mathappGrade = int.Parse(splitLine[5]);
                                    int averageGrade = (dsaGrade + progGrade + mathappGrade) / 3;

                                    string fileExportName = $"{splitLine[1]}, {splitLine[2]}_Grades";
                                    if (File.Exists($"{fileReportsFolder}{fileExportName}.csv"))
                                        File.Delete($"{fileReportsFolder}{fileExportName}.csv");
                                    File.AppendAllText($"{fileReportsFolder}{fileExportName}.csv", "StudentID,LastName,FirstName,DataStructuresGrade,Programming2Grade,MathApplicationITGrade,GradeAverage\n");
                                    File.AppendAllText($"{fileReportsFolder}{fileExportName}.csv", $"{splitLine[0]},{splitLine[1]},{splitLine[2]},{splitLine[3]},{splitLine[4]},{splitLine[5]},{averageGrade}");
                                    Console.WriteLine($"\nSuccessfully exported {fileReportsFolder}{fileExportName}.csv");
                                    break;
                                }
                            }

                            sequencePart++;
                            if (!idFound)
                            {
                                blockLocation += $"-C{Convert.ToString(casePart)}-S{Convert.ToString(sequencePart)}";
                                errorMessage = $"Could not find {searchStudentID.ToUpper()}";
                                errorLine = $"({DateTime.Now}) Empty Error ({blockLocation}) - {errorMessage}";
                                File.AppendAllText(fileErrorLog, $"{errorLine}\n");
                                Console.WriteLine($"\n{errorMessage}");
                            }

                            break;
                        case 5:
                            casePart = 5;

                            sequencePart++;
                            Console.WriteLine("\nError Log:");
                            foreach (string line in fileLinesOfErrorLog)
                                Console.WriteLine(line);
                            break;
                        case 6:
                            casePart = 6;

                            sequencePart++;
                            programLoop = false;
                            Console.WriteLine("\nYou have exited the program!");
                            break;
                    }
                }
                catch (ArgumentException)
                {
                    blockLocation += $"-C{Convert.ToString(casePart)}-S{Convert.ToString(sequencePart)}";
                    errorMessage = "Input cannot be empty.";
                    errorLine = $"({DateTime.Now}) Empty Error ({blockLocation}) - {errorMessage}";
                    File.AppendAllText(fileErrorLog, $"{errorLine}\n");
                    Console.WriteLine($"\n{errorMessage}");
                }
                catch (FormatException e)
                {
                    blockLocation += $"-C{Convert.ToString(casePart)}-S{Convert.ToString(sequencePart)}";
                    errorLine = $"({DateTime.Now}) Format Error ({blockLocation}) - {e.Message}";
                    File.AppendAllText(fileErrorLog, $"{errorLine}\n");
                    Console.WriteLine($"\n{e.Message}");
                }
                catch (OverflowException e)
                {
                    blockLocation += $"-C{Convert.ToString(casePart)}-S{Convert.ToString(sequencePart)}";
                    errorLine = $"({DateTime.Now}) Overflow Error ({blockLocation}) - {e.Message}";
                    File.AppendAllText(fileErrorLog, $"{errorLine}\n");
                    Console.WriteLine($"\n{e.Message}");
                }
                catch (IndexOutOfRangeException e)
                {
                    blockLocation += $"-C{Convert.ToString(casePart)}-S{Convert.ToString(sequencePart)}";
                    errorLine = $"({DateTime.Now}) Index Out Of Range Error ({blockLocation}) - {e.Message}";
                    File.AppendAllText(fileErrorLog, $"{errorLine}\n");
                    Console.WriteLine($"\n{e.Message}");
                }
                catch (FileNotFoundException e)
                {
                    blockLocation += $"-C{Convert.ToString(casePart)}-S{Convert.ToString(sequencePart)}";
                    errorLine = $"({DateTime.Now}) File Not Found Error ({blockLocation}) - {e.Message}";
                    File.AppendAllText(fileErrorLog, $"{errorLine}\n");
                    Console.WriteLine($"\n{e.Message}");
                }
                catch (IOException e)
                {
                    blockLocation += $"-C{Convert.ToString(casePart)}-S{Convert.ToString(sequencePart)}";
                    errorLine = $"({DateTime.Now}) Input/Output Error ({blockLocation}) - {e.Message}";
                    File.AppendAllText(fileErrorLog, $"{errorLine}\n");
                    Console.WriteLine($"\n{e.Message}");
                }
                catch (UnauthorizedAccessException e)
                {
                    blockLocation += $"-C{Convert.ToString(casePart)}-S{Convert.ToString(sequencePart)}";
                    errorLine = $"({DateTime.Now}) Unauthorized Access Error ({blockLocation}) - {e.Message}";
                    File.AppendAllText(fileErrorLog, $"{errorLine}\n");
                    Console.WriteLine($"\n{e.Message}");
                }
                catch (Exception e)
                {
                    blockLocation += $"-C{Convert.ToString(casePart)}-S{Convert.ToString(sequencePart)}";
                    errorLine = $"({DateTime.Now}) Unknown Error ({blockLocation}) - {e.Message}";
                    File.AppendAllText(fileErrorLog, $"{errorLine}\n");
                    Console.WriteLine($"\n{e.Message}");
                }

                Console.WriteLine("\nPress anything to continue.");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}