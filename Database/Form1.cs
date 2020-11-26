using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DatabaseAutoDeployment.Entity;
using DatabaseAutoDeployment.Repository;
using Microsoft.Extensions.Logging;

namespace Database
{
    public partial class Form1 : Form
    {
        private readonly ILogger _logger;
        private readonly IUnitOfWork _unitOfWork;

        public Form1(ILogger<Form1> logger,IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            var x = _unitOfWork.Get<Migration>();

        }
    }
}
