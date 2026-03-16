namespace ToDoList;
public class FileHandling
{
    //Methods for writing to and reading from files.The files will be saved in the current directory.
    public string FileName { get; set; }
    public string Dir { get; set; }
    public FileHandling(string fileName)
    {
        FileName = fileName;
        Dir=Directory.GetCurrentDirectory();
    }  
    public void SaveToFile(List<string> linesToSave)
    {//wrinting strings to a file
        string path = Path.Combine(Dir, FileName);
        FileInfo fileInfo = new FileInfo(path);
        //delete the file if it exists
        if (fileInfo.Exists) 
        {
            fileInfo.Delete();
        }
        //Creates a new file and writes to it
        using (StreamWriter sw = fileInfo.CreateText())
        {
            foreach (string line in linesToSave)
            {
                sw.WriteLine(line);
            }
            sw.Close();
        }
    }
    public List<string> ReadFromFile()
    {//reading files from a file
        string line = string.Empty;
        string path = Path.Combine(Dir, FileName);
        List<string> lines= new List<string>();
        FileInfo fileInfo = new FileInfo(path);
        //Reads from the file if it exists and returns a list of strings. If it doesn't exist an empty L
        if (fileInfo.Exists) 
        {
            using (StreamReader sr = new StreamReader(path))
            {
                while ((line = sr.ReadLine()) is not null) //Reads until end of file.
                {
                    lines.Add(line);
                }
                sr.Close();
            }          
        }        
        return lines;
    }
}
