using Autofac;
using Business.Abstract;
using Business.DependencyResolvers.Autofac;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormUI
{
    public partial class frmCustomer : Form
    {
        public frmCustomer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<AutofacBusinessModule>();
            var contanier = builder.Build();
            customerService = contanier.Resolve<ICustomerService>();
            InitializeComponent();
        }

        ICustomerService customerService;
        private void btnGetById_Click(object sender, EventArgs e)
        {
            if (txtId.Text=="")
            {
                return;
            }
            var data = customerService.GetById(txtId.Text);
            List<Customer> categories = new List<Customer>();
            categories.Add(data.Result);
            dataGridView1.DataSource = categories.Select(re => new { re.CustomerID, re.CompanyName, re.ContactName, re.City, re.Country, re.Address }).ToList();
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
            Customer customer = new Customer()
            {
                CustomerID = txtCustomerId.Text,
                City = txtId.Text,
                CompanyName=txtCompany.Text,
                ContactName=txtContact.Text,
                Country=txtCountry.Text,             
            };

            var result = customerService.Add(customer);
            MessageBox.Show(result.Message);
            Listele();
        }

        private string Listele()
        {
            var data = customerService.GetAll();
            dataGridView1.DataSource = data.Result.Select(re => new { re.CustomerID, re.CompanyName, re.ContactName, re.City, re.Country, re.Address }).ToList();
            return data.Message;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string result = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            var result2 = customerService.Delete(result);
            MessageBox.Show(result2.Message);
            Listele();
        }
        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string index = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            Customer customer = new Customer()
            {
                CustomerID = index,
                City = txtId.Text,
                CompanyName = txtCompany.Text,
                ContactName = txtContact.Text,
                Country = txtCountry.Text
            };
            var result = customerService.Update(customer);
            MessageBox.Show(result.Message);
            Listele();
        }

        private void btnGetAll_Click(object sender, EventArgs e)
        {
            Listele();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            btnDelete.Enabled = true;
            btnUpdate.Enabled = true;
        }
    }
}
