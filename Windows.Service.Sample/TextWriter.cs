using System.IO;

namespace Windows.Service.Sample
{
    public class TextWriter
    {

        public void Write(string message)
        {
            using (StreamWriter streamWriter = File.AppendText("C:\\SampleServiceLog.txt"))
            {
                streamWriter.WriteLine(message);
            }
        }
    }
}