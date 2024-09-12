namespace Animation2Tilemap.WinForms.Forms;

public partial class ConsoleForm : Form
{
    public ConsoleForm()
    {
        InitializeComponent();
    }

    public RichTextBox OutputBox => outputBox;

    private void outputBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            Close();
            e.Handled = true;
        }
    }
}