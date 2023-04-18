using System.Numerics;

namespace RSA
{
    public partial class MainForm : Form
    {
        RSA rsa = new RSA();
        public MainForm()
        {
            InitializeComponent();
            radioButtonEncrypt.Checked = true;
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBoxOutput.Text);
        }

        private void radioButtonEncrypt_CheckedChanged(object sender, EventArgs e)
        {
            labelInput.Text = "Enter the decrypted message:";
            labelOutput.Text = "Encrypted message:";
            buttonDecrypt.Enabled = false;
            buttonEncrypt.Enabled = true;
            textBoxInput.Text = string.Empty;
            textBoxOutput.Text = string.Empty;

            textBoxN.Text = string.Empty;
            textBoxD.Text = string.Empty;
            textBoxE.Text = string.Empty;
        }

        private void radioButtonDecrypt_CheckedChanged(object sender, EventArgs e)
        {
            labelInput.Text = "Enter the encrypted message:";
            labelOutput.Text = "Decrypted message:";
            buttonDecrypt.Enabled = true;
            buttonEncrypt.Enabled = false;
            textBoxInput.Text = string.Empty;
            textBoxOutput.Text = string.Empty;

            textBoxN.Text = string.Empty;
            textBoxD.Text = string.Empty;
            textBoxE.Text = string.Empty;
        }

        private async void buttonEncrypt_Click(object sender, EventArgs e)
        {
            rsa.Initialize(textBoxP.Text, textBoxQ.Text);
            string[] enryptedMessage = rsa.Encrypt(textBoxInput.Text);
            textBoxOutput.Text = string.Join(" ", enryptedMessage);

            textBoxN.Text = rsa.N.ToString();
            textBoxD.Text = rsa.D.ToString();
            textBoxE.Text = rsa.E.ToString();
        }

        private async void buttonDecrypt_Click(object sender, EventArgs e)
        {
            string[] enryptedMessage = textBoxInput.Text.Split(' ');
            textBoxOutput.Text = rsa.Decrypt(enryptedMessage);
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            textBoxP.Text = PrimeNumberGenerator.Generate(1000, 10000).ToString();
            textBoxQ.Text = PrimeNumberGenerator.Generate(1000, 10000).ToString();
        }
    }
}