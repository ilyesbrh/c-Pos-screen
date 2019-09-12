using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO.Ports; // Do not forget.

namespace PicRS232
{
    public partial class Form1_Principal : Form
    {
        // Use a string as the receive buffer.
        string Recibidos;
        public Form1_Principal()
        {
            InitializeComponent();
            // Open port while running the application.
            if (!serialPort1.IsOpen)
            {
                try
                {
                    //serialPort1.Encoding = Encoding.UTF32;
                    //serialPort1.Open();
                    
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            // Run the Reception function by triggering the Event 'DataReived'.
            serialPort1.DataReceived += new
            System.IO.Ports.SerialDataReceivedEventHandler(Recepcion);
        }
        
        // Process the data received in the full frame buffer and extract.
        private void Actualizar(object s, EventArgs e)
        {
            // Assign the value of the plot to the RichTextBox.
            //richTextBox_visualizar_mensaje.Text = Recibidos;
        }
        private void button_total_Click(object sender, EventArgs e)
        {
            serialPort1.Write(new Byte[] { 24, 115, 0 }, 0, 3);
        }

        private void button_1_Click(object sender, EventArgs e)
        {
            serialPort1.Write(new char[] { '←', 's', '2' }, 0, 3);
        }

        private void button_0_Click(object sender, EventArgs e)
        {
            serialPort1.Write(new char[] { '←', 's', '1' }, 0, 3);
        }

        private void button_3_Click(object sender, EventArgs e)
        {
            //ReadFromSerial();
        }
        
        // Show local time.
        private void timer1_Tick(object sender, EventArgs e)
        {
            statusStrip1.Items[0].Text = DateTime.Now.ToLongTimeString();
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            serialPort1.Write(new char[] {'↑'}, 0, 1);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string str = Convert.ToString((char)27) + 'Q' + 'A' + textBox1.Text;
            
            serialPort1.Write(str);

        }
        private void ReadFromSerial()
        {
            try
            {
                SerialPort Serial1 = new System.IO.Ports.SerialPort("COM2", 2400, Parity.None, 8, StopBits.One);
                Serial1.DtrEnable = true;
                Serial1.RtsEnable = true;
                Serial1.ReadTimeout = 3000;

                var MessageBufferRequest = new byte[8] { 27 , 115 , 4 ,0,0,0,0,0 };
                var MessageBufferReply = new byte[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
                int BufferLength = 8;

                if (!Serial1.IsOpen)
                {
                    Serial1.Open();
                }

                try
                {
                    Serial1.Write(MessageBufferRequest, 0, BufferLength);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                System.Threading.Thread.Sleep(100);

                Serial1.Read(MessageBufferReply, 0, 7);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SerialPort sp = new SerialPort();

            sp.PortName = "COM2";
            sp.BaudRate = 2400;
            sp.Parity = Parity.None;
            sp.DataBits = 8;
            sp.StopBits = StopBits.One;
            //sp.Encoding = Encoding.UTF8;
            string str = Convert.ToString((char)27) + 'Q' + 'A' + textBox2.Text+'♪';
            sp.Open();
            sp.Write(textBox2.Text);

            sp.Close();
            sp.Dispose();
            sp = null;
        }

        // We always show the message below.
        /*
        private void richTextBox_visualizar_mensaje_TextChanged(object sender, EventArgs e)
        {
            richTextBox_visualizar_mensaje.SelectionStart = richTextBox_visualizar_mensaje.TextLength;
            richTextBox_visualizar_mensaje.ScrollToCaret();
        }
        */
    }
}
