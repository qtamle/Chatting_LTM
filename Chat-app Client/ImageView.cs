using Communicator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Chat_app_Client
{
    public partial class ImageView : Form
    {
        private BufferFile bufferFile;

        public ImageView(BufferFile bufferFile)
        {
            this.bufferFile = bufferFile;
            InitializeComponent();
            this.Text = bufferFile.receiver + " - Picture from " + bufferFile.sender;
        }

        private void ImageView_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Image.FromStream(new MemoryStream(bufferFile.buffer));
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.InitialDirectory = @"C:\";
            saveDialog.Title = "Lưu tập tin";
            saveDialog.CheckFileExists = false;
            saveDialog.CheckPathExists = true;
            saveDialog.DefaultExt = bufferFile.extension;
            saveDialog.Filter = "All files (*" + bufferFile.extension + ")|*" + bufferFile.extension + "";
            saveDialog.FilterIndex = 2;
            saveDialog.RestoreDirectory = true;
            saveDialog.FileName = String.Format("{0:yyyy-MM-dd HH-mm-ss}__{1}", DateTime.Now, bufferFile.sender) + bufferFile.extension;

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                FileInfo fi = new FileInfo(saveDialog.FileName);

                try
                {
                    using (FileStream fStream = File.Create(fi.FullName))
                    {
                        fStream.Write(bufferFile.buffer, 0, bufferFile.buffer.Length);
                        fStream.Flush();
                        fStream.Close();
                    }
                }
                catch (Exception Ex)
                {
                    Console.WriteLine(Ex.ToString());
                }
            }
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
