using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace Sales
{
    public partial class FormMain : Form
    {
        private readonly string _connectionString;
        private readonly string _providerName;
        private readonly DbProviderFactory _providerFactory;

        public FormMain()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["Sales.Properties.Settings.SalesConnectionString"].ConnectionString;
            _providerName = ConfigurationManager.ConnectionStrings["Sales.Properties.Settings.SalesConnectionString"].ProviderName;
            _providerFactory = DbProviderFactories.GetFactory(_providerName);

            InitializeComponent();
            AddData();
        }

        private void AddData()
        {
            comboBoxTable.Items.Add("Sellers");
            comboBoxTable.Items.Add("Buyers");
            comboBoxTable.Items.Add("Sales");
        }

        private void comboBoxTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (var connection = _providerFactory.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    connection.ConnectionString = _connectionString;
                    connection.Open();

                    DbDataAdapter Adapter = _providerFactory.CreateDataAdapter();
                    command.CommandText = "Select * from " + comboBoxTable.Text;
                    Adapter.SelectCommand = command;

                    DataSet dataSet = new DataSet();
                    dataGridViewData.DataSource = null;
                    Adapter.Fill(dataSet);
                    dataGridViewData.DataSource = dataSet.Tables[0];
                }
            }

        }
    }
}
