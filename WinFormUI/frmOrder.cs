using Autofac;
using Business.Abstract;
using Business.DependencyResolvers.Autofac;
using Entities.Concrete;
using Entities.Concrete.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormUI
{
    public partial class frmOrder : Form
    {
        public frmOrder()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<AutofacBusinessModule>();
            var contanier = builder.Build();
            orderService = contanier.Resolve<IOrderService>();
            InitializeComponent();
        }
        IOrderService orderService;
        SerializeDto _employee;
        private void btnGetbyId_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "")
            {
                return;
            }
            var data = orderService.GetById(int.Parse(txtId.Text));
            List<Order> categories = new List<Order>();
            categories.Add(data.Result);
            dataGridView1.DataSource = categories.Select(re => new { re.OrderID, re.EmployeeID, re.CustomerID, re.ShipCountry, re.ShipCity, re.OrderDate }).ToList();
            MessageBox.Show(data.Message);
        }
        private void Only_Word_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                && !char.IsSeparator(e.KeyChar);
        }

        private void Only_Number_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void ClearAllTxtBox()
        {
            foreach (var c in this.Controls)
            {
                if (c is TextBox)
                {
                    ((TextBox)c).Text = String.Empty;
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cmbCustomers.SelectedItem==null)
            {
                return;
            }
           
            Order order = new Order()
            {
                CustomerID = cmbCustomers.SelectedItem.ToString(),
                EmployeeID = _employee.Id,
                ShipCity = txtCity.Text,
                ShipCountry = txtCountry.Text,
                OrderDate = dtpShipDate.Value
            };

            var result = orderService.Add(order);
            MessageBox.Show(result.Message);
            Listele();
        }

        private void JsonDeserialization()
        {
            JsonSerializer js = new JsonSerializer();
            StreamReader sr = new StreamReader($@"Serialization\user.json");
            JsonReader jsonReader = new JsonTextReader(sr);
            _employee = js.Deserialize<SerializeDto>(jsonReader);
            jsonReader.Close();
            sr.Close();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string result = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            var result2 = orderService.Delete(int.Parse(result));
            MessageBox.Show(result2.Message);
            Listele();
        }
        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {

            if (cmbCustomers.SelectedItem == null)
            {
                return;
            }

            int index = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());

            Order order = new Order()
            {
                OrderID = index,
                CustomerID = cmbCustomers.SelectedItem.ToString(),
                EmployeeID = _employee.Id,
                ShipCity = txtCity.Text,
                ShipCountry = txtCountry.Text,
                OrderDate = dtpShipDate.Value
            };
            var result = orderService.Update(order);
            MessageBox.Show(result.Message);
            Listele();
        }

        private string Listele()
        {
            var data = orderService.GetAll();
            dataGridView1.DataSource = data.Result.Select(re => new {re.OrderID,re.EmployeeID,re.CustomerID, re.ShipCountry, re.ShipCity,re.OrderDate }).ToList();
            return data.Message;
        }

        private void btnGetAll_Click(object sender, EventArgs e)
        {
            Listele();
        }

        private void frmOrder_Load(object sender, EventArgs e)
        {
            JsonDeserialization();
            var result = orderService.GetAll().Result.Select(re => new { re.CustomerID }).Distinct();

            foreach (var item in result)
            {
                cmbCustomers.Items.Add(item.CustomerID);
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            btnDelete.Enabled = true;
            btnUpdate.Enabled = true;
        }
    }
}
