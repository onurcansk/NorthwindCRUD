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
    public partial class frmEmployee : Form
    {
        public frmEmployee()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<AutofacBusinessModule>();
            var contanier = builder.Build();
            employeeService = contanier.Resolve<IEmployeeService>();
            InitializeComponent();
        }
        IEmployeeService employeeService;
        private void button1_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "")
            {
                return;
            }
            var data = employeeService.GetById(int.Parse(txtID.Text));
            List<Employee> employees = new List<Employee>();
            employees.Add(data.Result);
            dataGridView1.DataSource = employees.Select(re => new { re.EmployeeID, re.FirstName, re.LastName, re.BirthDate, re.Photo, re.City }).ToList();
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
        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var employee =
               new Employee
               {
                   FirstName = txtName.Text,
                   LastName = txtLastName.Text,
                   City = txtCity.Text,
                   Country = txtCountry.Text,
                   BirthDate = dtpBirth.Value
               };

            try
            {
                var result = employeeService.Add(employee);
                MessageBox.Show(result.Message);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                ClearAllTxtBox();
            }

            
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string result = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            try
            {
                var result2 = employeeService.Delete(int.Parse(result));
                MessageBox.Show(result2.Message);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int index = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            Employee employee = new Employee()
            {
                FirstName = txtName.Text,
                LastName = txtLastName.Text,
                City = txtCity.Text,
                Country = txtCountry.Text,
                BirthDate = dtpBirth.Value
            };
            try
            {
                var result =employeeService.Update(employee);
                MessageBox.Show(result.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                ClearAllTxtBox();
            }
           
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

        private void button5_Click(object sender, EventArgs e)
        {
            var result = employeeService.GetAll();
            dataGridView1.DataSource = result.Result.Select(re=> new {re.EmployeeID, re.FirstName, re.LastName,re.BirthDate,re.Photo,re.City}).ToList();
            MessageBox.Show(result.Message);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            button3.Enabled = true;
            button4.Enabled = true;
        }
    }
}
