using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cryptography_HW1
{
    class FileIO
    {
        public String Read()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            String data = "";

            dlg.InitialDirectory = "C:\\";
            dlg.Filter = "txt files (*.txt)| *.txt";

            if(dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    data = File.ReadAllText(dlg.FileName);
                    data = data.ToLower();
                }
                catch(Exception exc)
                {
                    Console.Write(exc);
                }
            }

            return data;
        }

        public bool Write(String text)
        {
            SaveFileDialog dlg = new SaveFileDialog();

            dlg.InitialDirectory = "C:\\";
            dlg.Filter = "txt files (*.txt)| *.txt";

            if(dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    File.WriteAllText(dlg.FileName, text);
                    return true;
                }
                catch(Exception exc)
                {
                    Console.Write(exc);
                }
            }

            return false;
        }

    }
}
