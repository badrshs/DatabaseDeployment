using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(Form1_DragEnter);
            this.DragDrop += new DragEventHandler(Form1_DragDrop);

            string contents = File.ReadLines($"{AppDomain.CurrentDomain.BaseDirectory}/importedFiles.txt").FirstOrDefault();
            ConnectionStringTextBox.Text = contents;
        }

        void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        void Form1_DragDrop(object sender, DragEventArgs e)
        {
            filePathListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;

            filePathListBox.Items.Clear();
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            string contents = File.ReadAllText($"{AppDomain.CurrentDomain.BaseDirectory}/importedFiles.txt");

            Array.Sort<string>(files);
            int i = 0;
            foreach (string file in files)
            {
                if (!contents.Contains(file.ToString()))
                {
                    filePathListBox.Items.Add(file);
                }
            };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (filePathListBox.Items.Count == 0)
                    return;
                string sqlConnectionString = ConnectionStringTextBox.Text;

                SqlConnection conn = new SqlConnection(sqlConnectionString);
                Server server = new Server(new ServerConnection(conn));
                string contents = File.ReadAllText($"{AppDomain.CurrentDomain.BaseDirectory}/importedFiles.txt");
                if (contents.Length == 0)
                {
                    File.AppendAllLines($"{AppDomain.CurrentDomain.BaseDirectory}/importedFiles.txt", new[] { sqlConnectionString });
                }
                else
                {
                    File.AppendAllLines($"{AppDomain.CurrentDomain.BaseDirectory}/importedFiles.txt", new[] { "" });
                }

                foreach (var file in filePathListBox.Items)
                {
                    if (!contents.Contains(file.ToString()))
                    {
                        server.ConnectionContext.ExecuteNonQuery(File.ReadAllText(file.ToString()));
                        File.AppendAllLines($"{AppDomain.CurrentDomain.BaseDirectory}/importedFiles.txt", new[] { file.ToString() });
                    }
                }
                MessageBox.Show(filePathListBox.Items.Count + " scripts Deployed Sucessfully", "Successfully Deployed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.InnerException.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
