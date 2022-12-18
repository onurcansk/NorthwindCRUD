using Autofac;
using Business.Abstract;
using Business.Concrete;
using Business.DependencyResolvers.Autofac;
using Entities.Concrete;
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
    public partial class frmCategory : Form
    {
        public frmCategory()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<AutofacBusinessModule>();
            var contanier = builder.Build();
            _categoryService = contanier.Resolve<ICategoryService>();

            InitializeComponent();
        }
        ICategoryService _categoryService;
        private void btnGetAll_Click(object sender, EventArgs e)
        {
            string message =Listele();
            MessageBox.Show(message);
        }

        private string Listele()
        {
            var data = _categoryService.GetAll();
            dataGridView1.DataSource = data.Result.Select(re => new { re.CategoryID, re.CategoryName, re.Description, re.Picture }).ToList();
            return data.Message;
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

        private void btnGetbyId_Click(object sender, EventArgs e)
        {
            if (txtIdforGet.Text=="")
            {
                return;
            }
            var data = _categoryService.GetById(int.Parse(txtIdforGet.Text));
            List<Category> categories = new List<Category>();
            categories.Add(data.Result);
            dataGridView1.DataSource = categories;
            MessageBox.Show(data.Message);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Category category = new Category()
            {
                CategoryName = txtCategoryName.Text,
                Description = txtDescription.Text,
                Picture = ImageToByteArray(pcbPicture.Image)

            };
            try
            {
                var result = _categoryService.Add(category);
                MessageBox.Show(result.Message);
                Listele();
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

        private void button1_Click(object sender, EventArgs e)
        {
            Image File;
            OpenFileDialog f = new OpenFileDialog();
            f.Filter = "Image files (*.jpg, *.png) | *.jpg; *.png";

            if (f.ShowDialog() == DialogResult.OK)
            {
                File = Image.FromFile(f.FileName);
                pcbPicture.Image = File;
            }
        }

        public byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            if (imageIn != null)
            {
                using (var ms = new MemoryStream())
                {
                    imageIn.Save(ms, imageIn.RawFormat);
                    return ms.ToArray();
                }
            }
            return null;

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string result = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            try
            {
                
                var result2 = _categoryService.Delete(int.Parse(result));
                MessageBox.Show(result2.Message);
                Listele();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }


        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int index = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            Category category = new Category()
            {
                CategoryID = index,
                CategoryName = txtCategoryName.Text,
                Description = txtDescription.Text,
                Picture = ImageToByteArray(pcbPicture.Image)
            };
            try
            {
                var result = _categoryService.Update(category);
                MessageBox.Show(result.Message);
                Listele();
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

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            btnDelete.Enabled = true;
            btnUpdate.Enabled = true;
        }
    }
}
