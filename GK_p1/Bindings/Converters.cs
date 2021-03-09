using GK_p1.Models;
using System.Windows.Forms;

namespace GK_p1
{
    public partial class MainWindow
    {
        private void EdgeToBool(object sender, ConvertEventArgs cevent)
        {
            if (cevent.DesiredType != typeof(bool))
            {
                return;
            }

            cevent.Value = ((Edge)cevent.Value) != null;
        }

        private void NewModeToEnabled(object sender, ConvertEventArgs cevent)
        {
            if (cevent.DesiredType != typeof(bool))
            {
                return;
            }

            cevent.Value = !(bool)cevent.Value;
        }

        private void EdgeToRestrictEnable(object sender, ConvertEventArgs cevent)
        {
            if (cevent.DesiredType != typeof(bool))
            {
                return;
            }

            if (cevent.Value == null)
            {
                cevent.Value = false;
                return;
            }

            cevent.Value = !((Edge)cevent.Value).IsRestricted;
        }

        private void EdgeToCanBeVertical(object sender, ConvertEventArgs cevent)
        {
            if (cevent.DesiredType != typeof(bool))
            {
                return;
            }

            if (cevent.Value == null)
            {
                cevent.Value = false;
                return;
            }

            Edge edge = cevent.Value as Edge;
            cevent.Value = !(edge.IsRestricted || edge.NextNeighbor.Restrictions.Contains(VerticalRestricion.Name) || edge.PrevNeighbor.Restrictions.Contains(VerticalRestricion.Name));
        }

        private void EdgeToCanBeHorizontal(object sender, ConvertEventArgs cevent)
        {
            if (cevent.DesiredType != typeof(bool))
            {
                return;
            }

            if (cevent.Value == null)
            {
                cevent.Value = false;
                return;
            }

            Edge edge = cevent.Value as Edge;
            cevent.Value = !(edge.IsRestricted || edge.NextNeighbor.Restrictions.Contains(HorizontalRestriction.Name) || edge.PrevNeighbor.Restrictions.Contains(HorizontalRestriction.Name));
        }

        private void EdgeToCanBeFixedInLength(object sender, ConvertEventArgs cevent)
        {
            if (cevent.DesiredType != typeof(bool))
            {
                return;
            }

            if (cevent.Value == null)
            {
                cevent.Value = false;
                return;
            }

            Edge edge = cevent.Value as Edge;
            cevent.Value = !edge.IsRestricted;
        }

        private void EdgeToIsRestricted(object sender, ConvertEventArgs cevent)
        {
            if (cevent.DesiredType != typeof(bool))
            {
                return;
            }

            if (cevent.Value == null)
            {
                cevent.Value = false;
                return;
            }

            cevent.Value = ((Edge)cevent.Value).IsRestricted;
        }
    }
}
