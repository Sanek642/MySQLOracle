using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySQLOracle
{
    public partial class EditForm : Form
    {
        MainForm form;
        private NewTable tmp;
        public EditForm()
        {
            InitializeComponent();
        }
        public EditForm(MainForm f)
        {
            this.TopMost = true;
            form = f;
            InitializeComponent();
        }

        private void EditForm_Load(object sender, EventArgs e)
        {
            
                using (BookSqlContext db = new BookSqlContext())
                {
                    richTextBox1.Clear();
                    richTextBox2.Clear();
                    textBox1.Clear();

                    // получаем значение выделенной строки
                    tmp = db.NewTables.Where(x => x.Name == form.listBox1.Text).FirstOrDefault();
                    NewTable.GetPLSQL(richTextBox1, tmp.StrPlsql);
                    NewTable.GetPLPlus(richTextBox2, tmp.StrPlplus);
                    textBox1.Text = tmp.Name;
                }
            
        }

        private void EditForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            form.Enabled = true;
            form.TopMost = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (BookSqlContext db = new BookSqlContext())
            {
                tmp.Name = textBox1.Text.Count() == 0 ? null : textBox1.Text;
                tmp.StrPlsql = richTextBox1.Text.Count() == 0 ? null : richTextBox1.Text;
                tmp.StrPlplus = richTextBox2.Text.Count() == 0 ? null : richTextBox2.Text;

                db.Update(tmp);
                NewTable.TruCathcSave(db, form, this, "Поля должны быть заполнены! Описание должно быть уникальным!");
            }
        }
    }
}
