namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        DateTime Begin;
        DateTime End;
        Random random = new();
        public Form1()
        {
            InitializeComponent();  
        }

        private void button1_Click(object sender, EventArgs e)
        {
               Thread value = new Thread(() =>
            {
                End = DateTime.Now;
            })
            {
                IsBackground = true
            };

            value.Start();
            value.Join();

            double Difference = (End - Begin).TotalSeconds;
            label2.Text = $"{Difference}";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            Thread Change= new Thread(() =>
            {
                panel1.BackColor = Color.Gray;
            });
            Thread.Sleep(random.Next(2000,7000));
            Change.Start();
            Change.Join(); 
            
            Thread value = new Thread(() =>
            {
                Begin = DateTime.Now;
            })
            {
                IsBackground = true
            };

            value.Start();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

    
    }
}