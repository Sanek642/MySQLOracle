using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace MySQLOracle
{
    public partial class AddForm : Form
    {
        MainForm form;
        
        public AddForm()
        {
            InitializeComponent();
        }

        public AddForm(MainForm f)
        {
            this.TopMost = true;
            form = f;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (BookSqlContext db = new BookSqlContext())
            {
                string? name = textBox1.Text.Count() == 0 ? null : textBox1.Text;
                string? plsql = richTextBox1.Text.Count() == 0 ? null : richTextBox1.Text;
                string? plplus = richTextBox2.Text.Count() == 0 ? null : richTextBox2.Text;



                NewTable code = new NewTable
                {
                    Name = name,
                    StrPlsql = plsql, 
                    StrPlplus = plplus
                };

                db.NewTables.Add(code);
                NewTable.TruCathcSave(db, form, this, "Поля должны быть заполнены! Описание должно быть уникальным!");

            }
        }

        private void AddForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            form.Enabled = true;
            form.TopMost = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            richTextBox2.Clear();
            richTextBox2.Text = richTextBox1.Text;

        }
    }
}
