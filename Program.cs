using System;
using System.IO;

namespace OPERSYSMP2
{
    class MainClass
    {
        /*
         * Displays contents of directory path sent to method       
         * string currentPath = directory path to be accessed (ex. /Users/Patrick/Desktop) 
        */
        public static void ShowDirectoryContents(string currentPath)
        {

            DirectoryInfo di = new DirectoryInfo(currentPath);

            DirectoryInfo[] diArr = di.GetDirectories();
            FileInfo[] fi = di.GetFiles("*.*");

            Console.WriteLine("Folders:");
            foreach (DirectoryInfo dri in diArr){
                if (string.Equals(dri.Name, ".DocumentRevisions-V100") == false && string.Equals(dri.Name, ".Trashes") == false)
                    Console.WriteLine(dri.Name);
            }
            Console.WriteLine();
            Console.WriteLine("Files:");
            foreach (FileInfo file in fi){
                Console.WriteLine("{0}: {1}: {2}", file.Name, file.LastAccessTime, file.Length);
            }
        }

        /*
         * Displays all commands implemented       
        */
        public static void Help()
        {
            Console.WriteLine();
            Console.WriteLine("Commands:");
            Console.WriteLine();
            Console.WriteLine("access __  : access indicated path");
            Console.WriteLine("back       : move to previous path");
            Console.WriteLine("copy       : copy file in current path to indicated path");
            Console.WriteLine("delete     : delete indicated file (must be in current path)");
            Console.WriteLine("changename : change file name (must be in current path)");
        }

        /*
         *        
        */
        public static void AccessDirectory(string directory){

            Console.WriteLine("patOS File Explorer: ");
            Console.WriteLine();
            Console.WriteLine("Current Directory: " + directory);
            Console.WriteLine();

            //string[] filePaths = Directory.GetFiles(directory);
            ShowDirectoryContents(directory);
        }

        /*
         * Moves to last directory  
         * string directory = directory path to be accessed (ex. /Users/Patrick/Desktop) 
        */
        public static String Back(string directory)
        {
            Console.Clear();
            int strlength = 0;
            for (int i = directory.Length - 1; directory[i] != '/'; i--){
                strlength++;
            }

            return directory.Substring(0, directory.Length - strlength);
        }

        /*
         * Copies file to input directory
         * string sourceDir = input directory on which the file is to be pasted on
        */
        public static void CopyFile(String sourceDir)
        { 
            String fileName = "";
            String source = "";
            String destDir = "";

            Console.Write("Input file name (must be in current directory): ");
            fileName = Console.ReadLine();

            source = Path.Combine(sourceDir, fileName);

            Console.Write("Input destination directory (case sensitive): ");
            destDir = Console.ReadLine();

            destDir = Path.Combine(destDir, fileName);

            File.Copy(source, destDir, false);
            Console.WriteLine("File " + fileName + " has been copied in " + destDir);
        }

        /*
         * Changes file name       
        */
        public static void changeFileName(String sourceDir) 
        {
            String fileName = "";
            String newFileName = "";

            Console.Write("Enter current file name: ");
            fileName = Console.ReadLine();
            Console.Write("Enter new file name: ");
            newFileName = Console.ReadLine();

            File.Move(Path.Combine(sourceDir, fileName), Path.Combine(sourceDir, newFileName));
            Console.WriteLine("File " + fileName + " has name changed to " + newFileName);
        }

        public static void deleteFile(String sourceDir)
        {
            string fileName = "";

            Console.Write("Enter file name: ");
            fileName = Console.ReadLine();
            File.Delete(Path.Combine(sourceDir, fileName));
            Console.WriteLine("File name " + fileName + " has been deleted!");
        }

        public static void Main(string[] args)
        {
            String directory = "/";
            String input = "";

            while (string.Equals(input, "exit") == false)
            {
                AccessDirectory(directory);

                Console.WriteLine("To show list of commands, input help");
                input = Console.ReadLine();

                if (input.Contains("access"))
                {
                    if (directory[directory.Length - 1] == '/')
                        for (int i = 7; i < input.Length; i++)
                            directory += input[i];
                    else{
                        directory += "/";
                        for (int i = 7; i < input.Length; i++)
                            directory += input[i];
                    }
                }
                else if (string.Equals(input, "back"))
                {
                    directory = Back(directory);
                }

                else if (input.Contains("copy"))
                {
                    CopyFile(directory);
                }
                else if (string.Equals(input, "changename"))
                {
                    changeFileName(directory);
                }
                else if (string.Equals(input, "delete"))
                {
                    deleteFile(directory); 
                }
                else if (string.Equals(input, "help"))
                {
                    Help();
                }
                else 
                {
                    Console.WriteLine("Invalid input. Please try again."); 
                }
            }
        }
    }
}
