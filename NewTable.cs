using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

namespace MySQLOracle
{
    public partial class NewTable
    {
        private string strPlsql;
        private string strPlplus;
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string StrPlsql { get { return strPlsql; } set { strPlsql = FilterSet(value); } }
        public string StrPlplus { get { return strPlplus; } set { strPlplus = FilterSet(value); } }

        private static string FilterSet(string strin)
        {
            try
            {
                // Удаляем лишние пробелы и символы перевода на новую строку
                string strnew = Regex.Replace(strin, @"\s+", " ");
                return strnew;
            }
            catch
            {
                return strin;
            }
        }


        private static void MessegeOk(string error)
        {
            MessageBox.Show(
            error, "Ошибка!",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information,
            MessageBoxDefaultButton.Button1,
            MessageBoxOptions.DefaultDesktopOnly);

        }

        public static void TruCathcSave(BookSqlContext db, MainForm mainForm, Form curForm, string error)
        {
            try
            {
                db.SaveChanges();
                NewTable.UpdateLB(mainForm.listBox1);
                curForm.Close();
            }
            catch
            {

                MessegeOk(error);
                curForm.TopMost = true;

            }

        }

        public static void UpdateLB(ListBox lb)
        {
            //очищаем listbox перед заполнением
            lb.Items.Clear();
            using (BookSqlContext db = new BookSqlContext())
            {

                // получаем объекты из бд и передаем в листбокс
                var data = db.NewTables.ToList();
                foreach (var i in data)
                {
                    lb.Items.Add(i.Name);
                }
            }
        }

        public static void UpdateLB(ListBox lb, string str)
        {
            //очищаем listbox перед заполнением
            lb.Items.Clear();
            using (BookSqlContext db = new BookSqlContext())
            {

                // получаем объекты из бд и передаем в датагрид
                var data = db.NewTables.Where(x => EF.Functions.Like(x.Name,$"%{str}%")).ToList();
                foreach (var i in data)
                {
                    lb.Items.Add(i.Name);
                }
            }
        }

        public static void GetPLSQL(RichTextBox r, string plsql)
        {
            List<string> lang = new List<string>() {"ADD", "ALTER","ALL","AND", "ANY", "AS", "ASC", "BACKUP", "BETWEEN", "CASE", "CHECK", "COLUMN",
                 "CREATE", "CREATE","DATABASE", "DEFAULT", "DELETE", "DESC", "DISTINCT", "DROP", "EXEC", "EXISTS", "FOREIGN","KEY","FULL","OUTER",
                "JOIN", "GROUP", "BY", "HAVING", "IN", "INDEX", "INNER", "INSERT", "INTO", "IS", "NOT", "NULL", "LEFT", "LIKE", "LIMIT", "OR",
                "ORDER","OVER", "PRIMARY", "PROCEDURE","RIGHT","PARTITION", "ROWNUM", "TOP", "SET", "TABLE", "UNION", "UNIQUE", "UPDATE", "VALUES", "VIEW"};
            
            List<string> nlang = new List<string>() { "SELECT", "CONSTRAINT", "WHERE", "FROM" };

            string[] str = plsql.Split(" ");
            
            int countSelect = 0;
            foreach (var s in str)
            {

                if (lang.Exists(x => x == s.ToUpper()))
                {
                    r.SelectionColor = Color.Blue;
                    r.AppendText(s + " ");
                    r.SelectionColor = Color.Black;
                }
                else if (nlang.Exists(x => x == s.ToUpper()))
                {
                    if (s.ToUpper() == "SELECT")
                    {
                        countSelect++;
                    }

                    string tab = new string('\t', (countSelect - 1) < 0 ? 0 : (countSelect - 1));

                    r.AppendText("\n");
                    r.SelectionColor = Color.Blue;
                    r.AppendText(tab + s + " ");
                    r.SelectionColor = Color.Black;
                }
                else
                {
                    r.AppendText(s + " ");
                }

            }

        }

        public static void GetPLPlus(RichTextBox r, string plsql)
        {
            List<string> lang = new List<string>() {"ADD", "ALTER","ALL","AND", "ANY", "AS", "ASC", "BACKUP", "BETWEEN", "CASE", "CHECK", "COLUMN",
                 "CREATE", "CREATE","DATABASE", "DEFAULT", "DELETE", "DESC", "DISTINCT", "DROP", "EXEC", "EXISTS", "FOREIGN","KEY","FULL","OUTER",
                "JOIN", "GROUP", "BY", "HAVING", "IN", "INDEX", "INNER", "INSERT", "INTO", "IS", "NOT", "NULL", "LEFT", "LIKE", "LIMIT", "OR",
                "ORDER","OVER", "PRIMARY", "PROCEDURE","RIGHT","PARTITION", "ROWNUM", "TOP", "SET", "TABLE", "UNION", "UNIQUE", "UPDATE", "VALUES", "VIEW"};

            List<string> nlang = new List<string>() { "SELECT", "CONSTRAINT", "WHERE", "FROM" };

            string[] str = plsql.Split(" ");

            int countSelect = 0;
            foreach (var s in str)
            {

                if (lang.Exists(x => x == s.Trim(',').ToUpper()))
                {
                    r.SelectionColor = Color.Blue;
                    r.AppendText(s + " ");
                    r.SelectionColor = Color.Black;
                }
                else if (nlang.Exists(x => x == s.ToUpper()))
                {
                    if (s.ToUpper() == "SELECT")
                    {
                        countSelect++;
                    }

                    string tab = new string('\t', (countSelect - 1) < 0 ? 0 : (countSelect - 1));

                    r.AppendText("\n");
                    r.SelectionColor = Color.Blue;
                    r.AppendText(tab + s + " ");
                    r.SelectionColor = Color.Black;
                }
                else
                {
                    r.AppendText(s + " ");
                }

            }

        }
    }
}
