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
    public partial class frmProduct : Form
    {
        public frmProduct()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<AutofacBusinessModule>();
            var contanier = builder.Build();
            productService = contanier.Resolve<IProductService>();
            categoryService = contanier.Resolve<ICategoryService>();
            InitializeComponent();
        }
        ICategoryService categoryService;
        IProductService productService;
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cmbCategory.SelectedItem==null)
            {
                return;
            }
            Product product = new Product()
            {
                ProductName=txtProductName.Text,
                CategoryID=cmbCategory.SelectedIndex,
                UnitPrice=Convert.ToDecimal(txtPrice.Text),
                UnitsInStock=Convert.ToInt16(txtStock.Text)
            };

            var result = productService.Add(product);
            MessageBox.Show(result.Message);
            Listele();
        }
        private string Listele()
        {
            var data = productService.GetAll();
            dataGridView1.DataSource = data.Result.Select(re => new { re.ProductID, re.ProductName, re.CategoryID, re.UnitsInStock, re.UnitPrice }).ToList();
            return data.Message;
        }
        private void btnGetById_Click(object sender, EventArgs e)
        {
            if (txtId.Text=="")
            {
                return;
            }
            var data = productService.GetById(int.Parse(txtId.Text));
            List<Product> categories = new List<Product>();
            categories.Add(data.Result);
            dataGridView1.DataSource = categories.Select(re => new { re.ProductID, re.ProductName, re.Category, re.UnitsInStock, re.UnitPrice }).ToList();
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (cmbCategory.SelectedItem == null)
            {
                return;
            }
            int index = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            Product product = new Product()
            {
                ProductID=index,
                ProductName = txtProductName.Text,
                CategoryID = cmbCategory.SelectedIndex,
                UnitPrice = Convert.ToDecimal(txtPrice.Text),
                UnitsInStock = Convert.ToInt16(txtStock.Text)
            };
            var result = productService.Update(product);
            MessageBox.Show(result.Message);
            Listele();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int result = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            var result2 = productService.Delete(result);
            MessageBox.Show(result2.Message);
            Listele();
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            btnDelete.Enabled = true;
            btnUpdate.Enabled = true;
        }

        private void btnGetAll_Click(object sender, EventArgs e)
        {
            Listele();
        }

        private void frmProduct_Load(object sender, EventArgs e)
        {
            var result = categoryService.GetAll().Result.Select(cat => new { cat.CategoryName }).ToList();
            foreach (var item in result)
            {
                cmbCategory.Items.Add(item.CategoryName);
            }
        }
    }
}
