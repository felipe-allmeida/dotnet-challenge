using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRental.API.FunctionalTests.Utils
{
    public class MockFile
    {
        public MockFile(string fileName, byte[] bytes)
        {
            FileName = fileName;
            Bytes = bytes;
        }

        public string FileName { get; private set; }
        public byte[] Bytes { get; private set; }
    }

    public static class FileUtils
    {
        public static MockFile CreateFile(string fileName, string fileContent)
        {
            using var ms = new MemoryStream();

            var w = new StreamWriter(ms);
            w.WriteLine(fileContent);
            w.Flush();
            ms.Position = 0;

            System.Net.Mime.ContentType ct = new System.Net.Mime.ContentType(System.Net.Mime.MediaTypeNames.Text.Plain);
            System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(ms, ct);
            attach.ContentDisposition.FileName = fileName;

            return new MockFile(fileName, ms.ToArray());
        }
    }
}
