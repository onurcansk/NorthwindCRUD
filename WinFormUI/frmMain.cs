using Autofac;
using Base.Utilities.Results.Concrete;
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
    public partial class frmMain : Form
    {
        public frmMain()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<AutofacBusinessModule>();
            var contanier = builder.Build();
            _employeeService = contanier.Resolve<IEmployeeService>();

            InitializeComponent();
        }
        IEmployeeService _employeeService;
        private void button1_Click(object sender, EventArgs e)
        {
            UserDto userDto = new UserDto()
            {
                FirtName = textBox1.Text.ToLower(),
                LastName = textBox2.Text.ToLower()
            };

            var result = _employeeService.Login(userDto);
            if (result.Result!=null)
            {
                SerializeDto serializeDto = new SerializeDto()
                {
                    FirstName = result.Result.FirstName,
                    LastName = result.Result.LastName,
                    Auth = result.Result.Auth,
                    Id = result.Result.EmployeeID
                };
                Serialize(serializeDto);

                frmNavigator frmNavigator = new frmNavigator();
                this.Hide();
                frmNavigator.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("İsim veya soyisim hatalı");
            }
            


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
        private void Serialize(SerializeDto result)
        {
            if (result != null)
            {
                JsonSerializer js = new JsonSerializer();
                if (!Directory.Exists($"Serialization"))
                {
                    Directory.CreateDirectory($"Serialization");
                }
                StreamWriter sw = new StreamWriter($"Serialization\\user.json");
                JsonWriter jkw = new JsonTextWriter(sw);
                js.Serialize(jkw, result);
                sw.Close();
                jkw.Close();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
