using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace reminder
{
    public partial class Form1 : Form
    {
        private readonly Timer timer;
        private string[] phrases;
        private int phraseIndex = 0;

        public Form1()
        {
            InitializeComponent();

            // Load settings from config file
            var settings = new XmlDocument();
            settings.Load("config.xml");
            var root = settings.DocumentElement;

            // Get timer interval and phrases file path from settings
            var interval = int.Parse(root.SelectSingleNode("Interval").InnerText);
            var phrasesFilePath = root.SelectSingleNode("PhrasesFilePath").InnerText;

            // Load phrases from file
            //phrases = File.ReadAllLines(phrasesFilePath);

            // Load and shuffle phrases from file
            var random = new Random();
            phrases = File.ReadAllLines(phrasesFilePath);
            phrases = phrases.OrderBy(x => random.Next()).ToArray();

            // Set up timer
            timer = new Timer
            {
                Interval = interval * 60 * 1000 // Interval is in minutes
            };
            timer.Tick += Timer1_Tick;
            timer.Start();
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            // Show reminder
            //MessageBox.Show(phrases[phraseIndex]);
            MessageBox.Show(phrases[phraseIndex], "Reminder", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Show();

            // Move to next phrase
            //phraseIndex = (phraseIndex + 1) % phrases.Length;
            phraseIndex++;

            // If we've gone through all phrases, reshuffle them
            if (phraseIndex >= phrases.Length)
            {
                var random = new Random();
                phrases = phrases.OrderBy(x => random.Next()).ToArray();
                phraseIndex = 0;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
