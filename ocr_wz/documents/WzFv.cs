using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Data;

namespace ocr_wz.documents
{
    class WzFv
    {
        public WzFv(string fileNameTXT, string fileLogName)
        {
            string pdfName = fileNameTXT.Replace(".txt", ".pdf");
            conf Config = new conf();
            FileStream fs = new FileStream(fileNameTXT,
                                           FileMode.Open, FileAccess.ReadWrite);
            DataTable docNames = new DataTable();
            docNames.Columns.Add("WZ", typeof(string));

            try
            {

            }
            catch
            {

            }
        }
    }
}
