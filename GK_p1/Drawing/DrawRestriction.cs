using GK_p1.Models;
using GK_p1.Properties;
using System.Windows.Forms;

namespace GK_p1
{
    public partial class MainWindow
    {
        private void DrawIcon(PaintEventArgs e, int x, int y, string restrictionName)
        {
            if (restrictionName == VerticalRestricion.Name)
            {
                e.Graphics.DrawIcon(Resources.vertical, x, y - (Resources.vertical.Height / 2));
            }
            else if (restrictionName == HorizontalRestriction.Name)
            {
                e.Graphics.DrawIcon(Resources.horizontal, x - (Resources.horizontal.Width / 2), y);
            }
            else if (restrictionName == SameLengthRestriction.Name)
            {
                e.Graphics.DrawIcon(Resources._fixed, x - (Resources._fixed.Width / 2), y - (Resources._fixed.Height / 2));
            }
        }
    }
}
