using System.Drawing;
using System.Windows.Forms;

namespace PropProAssistant
{
    public static class DebugComponent
    {
        public static void InitializeDebugControls(Form_PropTransfer form)
        {
            AddDebugLabel(form, "DEBUG MODE", form.ClientSize.Width - 50, form.ClientSize.Height - 20);
            AddDebugLabel(form, "DEBUG MENU", form.ClientSize.Width - 50, form.ClientSize.Height - 130);
            AddResetButton(form);
        }

        private static void AddDebugLabel(Form_PropTransfer form, string text, int x, int y)
        {
            var label = new Label();
            label.Text = text;
            label.ForeColor = Color.Red;
            label.Font = new Font(form.Font.FontFamily, 12, FontStyle.Bold);
            label.AutoSize = true;
            label.Location = new Point(x - label.Width, y);
            label.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            form.Controls.Add(label);
        }

        private static void AddResetButton(Form_PropTransfer form)
        {
            var button = new Button();
            button.Text = "Reset";
            button.Location = new Point(form.ClientSize.Width - button.Width - 50, form.ClientSize.Height - button.Height - 80);
            button.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button.Click += form.Btn_ResetButton_Click;
            form.Controls.Add(button);
        }
    }
}
