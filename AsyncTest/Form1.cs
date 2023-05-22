namespace AsyncTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            button2.BackColor = Color.Aquamarine;
            button1.Text = "Sync task";
            button2.Text = "UI task";
            button3.Text = "Async task";
            DoSomeAction().GetAwaiter().GetResult();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.BackColor = Color.Red;
            Thread.Sleep(5000);
            MessageBox.Show("Some action done");
            button1.BackColor = Color.Green;
        }


        private void button2_Click(object sender, EventArgs e) =>
            button2.BackColor = button2.BackColor == Color.Aquamarine ? Color.DarkRed : Color.Aquamarine;

        private async void button3_Click(object sender, EventArgs e)
        {
            button3.BackColor = Color.Red;
            await DoSomeAction();
            button3.BackColor = Color.Green;
            MessageBox.Show("Async action done");

        }


        // public void SomeSyncHandler()
        // {
        //     await DoSomeAction();
        // }

        private async Task DoSomeAction()
        {
            await Task.Delay(5000);
        }
    }
}