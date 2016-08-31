using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO; 

namespace Mini_project___hangman
{
    public partial class Form1 : Form
    {
        //add all properties of images together
        private Bitmap [] hangImages = { Mini_project___hangman.Properties.Resources.Untitled_1, Mini_project___hangman.Properties.Resources.Untitled_2, Mini_project___hangman.Properties.Resources.Untitled_3,
                                        Mini_project___hangman.Properties.Resources.Untitled_4, Mini_project___hangman.Properties.Resources.Untitled_5, Mini_project___hangman.Properties.Resources.Untitled_6,
                                        Mini_project___hangman.Properties.Resources.Untitled_7, Mini_project___hangman.Properties.Resources.Untitled_8};
            //assing private int to wring guesses
            private int wrongGuesses = 0;
            //private int rightGuesses = 0;
            private string current = "";
            private string copyCurrent = "";
            private string[] words;
            //private string lblword;
        


        /*  
         private static readOnly string[] Words = new string[] { "computer", "programmer", "software", "debugger", "compiler",
         "developer", "algorithm", "array", "method", "variable"}; */

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            SetupWordChoice();
            labelResult.Text = "";
            //Enable Alphabet buttons with the press of New Button
            foreach (Control control in Controls)
            {
                Button letter = control as Button;
                if (letter != null)
                {
                    letter.Enabled = true;
                }

            }
        }

       
        private void loadwords()
    {
        char[] delimiterChars = {','};
        string[] readText = System.IO.File.ReadAllLines(@"C:\\Users\student\Aurelija Zubaviciute\GitHub\Mini project - hangman\Mini project - hangman\bin\Debug\words.txt");
        words = new string[readText.Length];
        int index = 0;
        foreach (string s in readText)
        {
            string[] line = s.Split(delimiterChars);
            words[index++] = line[0]; 
        }
          
    } 

    private void SetupWordChoice()
        {
            //making a guess
            wrongGuesses = 0;
            hangImage1.Image = hangImages[wrongGuesses];
            int guessIndex = (new Random()).Next(words.Length); //generates random number for the words 
            current = words[guessIndex];

            label2.Text = current;

            //make a copy of a guess
            copyCurrent = "";
            for(int index=0; index < current.Length; index++)
            {
                copyCurrent += "_";
            }
             displayCopy();

        }
        //display to the label above function
        private void displayCopy()
        {
            lblword.Text += " ";
            
            for (int index = 0; index < copyCurrent.Length; index++)
            {
                lblword.Text += copyCurrent.Substring(index,1);
                lblword.Text += " ";
                
            }
        } 

        private void updateCopy(char guess)
        {

        } 
        //all alphabet buttons are linked to this part.
        private void buttonA_Click(object sender, EventArgs e)
        {
            Button choice = sender as Button;
            choice.Enabled = false;

            if (current.Contains(choice.Text))
            {
                char[] temp = copyCurrent.ToCharArray();
                char[] find = current.ToCharArray();
                char guessChar = choice.Text.ElementAt(0);
                for (int index = 0; index <find.Length; index++)
                {
                    if (find [index] == guessChar)
                    {
                        temp[index] = guessChar;
                    }
                    copyCurrent = new string(temp);
                    displayCopy();
                }
            }


            else
            {
                wrongGuesses++;
            }

            if (wrongGuesses <= 7)
            {
                hangImage1.Image = hangImages[wrongGuesses];
            }
            else 
            {
                labelResult.Text = "You Lost!";
            }
            if (copyCurrent.Equals(current))
            {
                labelResult.Text = "Yay, you won!!!";
            }

            label3.Text = choice.Text;
        }

        //Form application
        private void Form1_Load(object sender, EventArgs e)
        {
            loadwords();
            SetupWordChoice();
        }

        //Exit button
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
