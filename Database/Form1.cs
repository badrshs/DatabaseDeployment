using DatabaseAutoDeployment.Entity;
using DatabaseAutoDeployment.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Database
{
    public sealed partial class Form1 : Form
    {
        private readonly ILogger _logger;
        private readonly IUnitOfWork _unitOfWork;
        private List<Migration> _migrations = new List<Migration>();
        private bool _connectionState = false;

        public Form1(IUnitOfWork unitOfWork)
        {
            InitializeComponent();
            _unitOfWork = unitOfWork;

            AllowDrop = true;
            DragEnter += Form1_DragEnter;
            DragDrop += Form1_DragDrop;
        }

        void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        void Form1_DragDrop(object sender, DragEventArgs e)
        {
            if (!_connectionState)
                return;

            filePathListBox.SelectionMode = SelectionMode.MultiSimple;
            filePathListBox.Items.Clear();

            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            Array.Sort(files);

            foreach (string file in files)
            {
                var migration = new Migration
                {
                    OriginalPath = file,
                    ScriptName = Path.GetFileName(file)
                };

                if (_migrations.All(e => e.ScriptName != migration.ScriptName))
                {
                    filePathListBox.Items.Add(migration);
                }
            };
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ConnectionStringTextBox.Text = _unitOfWork.GetConnectionString();
        }

        private void checkThConnectionBtn_Click(object sender, EventArgs e)
        {
            filePathListBox.Items.Clear();
            _unitOfWork.SetConnectionString(ConnectionStringTextBox.Text);
            _unitOfWork.EnsureCreated();
            _connectionState = _unitOfWork.CanConnectAsync().Result;

            if (_connectionState)
            {
                _migrations = _unitOfWork.Get<Migration>();

                MessageBox.Show($@"Connection string is correct, last script was {_migrations.LastOrDefault()?.ScriptName ?? "nothing"}", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);

                RunBtn.Enabled = true;
            }
            else
            {
                MessageBox.Show("Your connection string not valid ", "Result", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RunBtn_Click(object sender, EventArgs e)
        {
            foreach (Migration item in filePathListBox.Items)
            {
                _unitOfWork.ExecuteSqlRaw(File.ReadAllText(item.OriginalPath));
                _unitOfWork.Add(item);
            }

            _unitOfWork.Save();
            MessageBox.Show($@"{filePathListBox.Items.Count} scripts added successfully", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}