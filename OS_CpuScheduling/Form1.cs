using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CpuSchedulerUI
{
    public partial class Form1 : Form
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            // Attach click handler if control exists in the designer
            if (LblAdd != null)
            {
                LblAdd.Click -= LblAdd_Click;
                LblAdd.Click += LblAdd_Click;
            }
        }

        private void LblAdd_Click(object? sender, EventArgs e)
        {
            if (TxtBoxAdd == null || InputTabel == null)
            {
                MessageBox.Show("Required controls (TxtboxAdd or InputTabel) not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var text = TxtBoxAdd.Text?.Trim() ?? "";
            if (!int.TryParse(text, out int count) || count <= 0)
            {
                MessageBox.Show("Enter a positive integer in the add textbox.", "Input error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int startIndex = InputTabel.Controls.Count;
            for (int i = 0; i < count; i++)
            {
                AddProcessRow(startIndex + i);
            }
        }

        // Creates and appends one horizontal row (ProcessName, Arrival, Burst)
        private void AddProcessRow(int index)
        {
            var row = new Panel
            {
                Height = 34,
                Width = Math.Max(400, InputTabel.ClientSize.Width - SystemInformation.VerticalScrollBarWidth - InputTabel.Padding.Horizontal),
                Margin = new Padding(3),
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top
            };

            var txtName = new TextBox
            {
                Name = $"txtName_{index}",
                Text = $"P{index + 1}",
                Location = new Point(0, 4),
                Width = 320
            };

            var txtArrival = new TextBox
            {
                Name = $"txtArrival_{index}",
                Text = "0",
                Location = new Point(txtName.Right + 8, 4),
                Width = 120
            };

            var txtBurst = new TextBox
            {
                Name = $"txtBurst_{index}",
                Text = "1",
                Location = new Point(txtArrival.Right + 8, 4),
                Width = 120
            };

            // Optional: simple input validation on leaving the arrival/burst boxes
            txtArrival.Leave += (_, _) =>
            {
                if (!int.TryParse(txtArrival.Text.Trim(), out _) ) txtArrival.Text = "0";
            };
            txtBurst.Leave += (_, _) =>
            {
                if (!int.TryParse(txtBurst.Text.Trim(), out int v) || v <= 0) txtBurst.Text = "1";
            };

            row.Controls.Add(txtName);
            row.Controls.Add(txtArrival);
            row.Controls.Add(txtBurst);

            InputTabel.Controls.Add(row);
        }

        // Example helper to read the user inputs into a typed list (call when you need to run scheduling)
        private (bool ok, System.Collections.Generic.List<(string name,int arrival,int burst)> list) ReadProcessInputs()
        {
            var results = new System.Collections.Generic.List<(string, int, int)>();

            for (int i = 0; i < InputTabel.Controls.Count; i++)
            {
                if (InputTabel.Controls[i] is not Panel row) continue;

                var txts = row.Controls.OfType<TextBox>().ToArray();
                if (txts.Length < 3) return (false, results);

                string name = txts[0].Text.Trim();
                if (string.IsNullOrEmpty(name)) name = $"P{i+1}";

                if (!int.TryParse(txts[1].Text.Trim(), out int arrival) || arrival < 0)
                {
                    MessageBox.Show($"Arrival must be a non-negative integer at row {i + 1}.", "Validation error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return (false, results);
                }

                if (!int.TryParse(txts[2].Text.Trim(), out int burst) || burst <= 0)
                {
                    MessageBox.Show($"Burst must be a positive integer at row {i + 1}.", "Validation error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return (false, results);
                }

                results.Add((name, arrival, burst));
            }

            return (true, results);
        }
    }
}