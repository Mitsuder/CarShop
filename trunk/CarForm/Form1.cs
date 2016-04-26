using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CarForm.ServiceReference1;
using ServiceCar;

namespace CarForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Table1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            Table1.Rows.Clear();
            Service1Client client = new Service1Client();
            Car[] a = client.load();
            for(int i=0; i<a.Length; i++)
            {
                Table1.Rows.Add();
                Table1.Rows[i].Cells[0].Value = a[i].manufacturer;
                Table1.Rows[i].Cells[1].Value = a[i].model;
                Table1.Rows[i].Cells[2].Value = a[i].dat.ToLongDateString();
                Table1.Rows[i].Cells[3].Value = a[i].volume.ToString();
                Table1.Rows[i].Cells[4].Value = a[i].power.ToString();
                Table1.Rows[i].Cells[5].Value = a[i].trancemission.ToString();
            }
            MessageBox.Show("Информация из БД загружена");
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            List<Car> a = new List<Car>();
            Car b = new Car();
            Service1Client client = new Service1Client();
            bool success = true;
            for(int i=0; i<Table1.RowCount-1; i++)
            {
                try
                {
                    b.manufacturer = (string)Table1.Rows[i].Cells[0].Value;
                    b.model = (string)Table1.Rows[i].Cells[1].Value;
                    b.dat = DateTime.Parse((string)Table1.Rows[i].Cells[2].Value);
                    string str = (string)Table1.Rows[i].Cells[3].Value;
                    str = str.Replace('.', ',');
                    b.volume = double.Parse(str);
                    b.power = Int32.Parse((string)Table1.Rows[i].Cells[4].Value);
                    b.trancemission = (string)Table1.Rows[i].Cells[5].Value;
                    a.Add(b);
                    b = new Car();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message+" в строке "+ (i+1)); success = false; }
            }
            if (success)
                if (client.save(a.ToArray()))
                    MessageBox.Show("сохранение успешно");
                else MessageBox.Show("что то пошло не так");
            else MessageBox.Show("сохранение работает только если нет ошибок в таблице");
        }
    }
}
