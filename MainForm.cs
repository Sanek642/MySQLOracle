namespace MySQLOracle
{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            AddForm fadd= new AddForm(this);
            fadd.Show(); //передаем ссылку на главную форму
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            NewTable.UpdateLB(listBox1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string str = textBox1.Text;
            NewTable.UpdateLB(listBox1, str);
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if(listBox1.SelectedItems != null)
            {
                richTextBox1.Clear();
                richTextBox2.Clear();

                using (BookSqlContext db = new BookSqlContext())
                {

                    // получаем значение выделенной строки
                    var idRow = db.NewTables.Where(x => x.Name == listBox1.Text).FirstOrDefault();
                    NewTable.GetPLSQL(richTextBox1, idRow.StrPlsql);
                    NewTable.GetPLPlus(richTextBox2, idRow.StrPlplus);
                }
              
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count > 0)
            {
                this.Enabled = false;
                EditForm fedit = new EditForm(this);
                fedit.Show();
            }
        }
    }
}