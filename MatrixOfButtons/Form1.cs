using System;
using System.Drawing;
using System.Windows.Forms;

namespace MatrixOfButtons
{
    public partial class Form1 : Form
    {
        private const int MaxRows = 4;
        private const int MaxCols = 4;

        private Button[,] matrixButtons;
        private Panel matrixPanel;
        private Label matrixLabel;

        public Form1()
        {
            InitializeComponent();

            matrixPanel = new Panel();
            matrixPanel.Location = new Point(12, 12);
            matrixPanel.Width = MaxCols * 50;
            matrixPanel.Height = MaxRows * 50;
            matrixPanel.BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(matrixPanel);

            matrixLabel = new Label();
            matrixLabel.Location = new Point(12, matrixPanel.Bottom + 12);
            matrixLabel.AutoSize = true;
            Controls.Add(matrixLabel);

            matrixButtons = new Button[MaxRows, MaxCols];

            // Create matrix buttons
            for (int row = 0; row < MaxRows; row++)
            {
                for (int col = 0; col < MaxCols; col++)
                {
                    Button button = new Button();
                    button.Width = 50;
                    button.Height = 50;
                    button.Location = new Point(col * button.Width, row * button.Height);
                    button.Text = $"{row + 1}x{col + 1}";
                    button.Tag = new Point(row, col);
                    button.Click += MatrixButtonClick;
                    matrixPanel.Controls.Add(button);

                    matrixButtons[row, col] = button;
                }
            }
        }

        private void MatrixButtonClick(object sender, EventArgs e)
        {
            // Reset all buttons to default color
            foreach (Button button in matrixButtons)
            {
                button.BackColor = SystemColors.Control;
            }

            // Highlight selected button
            Button selectedButton = (Button)sender;
            selectedButton.BackColor = Color.Red;

            // Get selected dimensions
            int[] dimensions = selectedButton.Text.Split('x').Select(int.Parse).ToArray();
            int rows = dimensions[0];
            int cols = dimensions[1];

            // Create matrix based on selected dimensions
            CreateMatrix(rows, cols);
        }

        private void CreateMatrix(int rows, int cols)
        {
            // Create matrix
            int[,] matrix = new int[rows, cols];
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    matrix[row, col] = row * cols + col + 1;
                }
            }

            // Display matrix
            matrixLabel.Text = "";
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    matrixLabel.Text += $"{matrix[row, col]}\t";
                }
                matrixLabel.Text += Environment.NewLine;
            }
        }
    }
}
