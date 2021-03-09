using System.Windows.Forms;

namespace GK_p1
{
    public partial class MainWindow
    {
        private void AddBindings()
        {
            Binding edgeToCanAddNewButton = new Binding(nameof(this.newVertexButton.Enabled), this, nameof(this.SelectedEdge), true, DataSourceUpdateMode.OnPropertyChanged)
            {
                DataSourceUpdateMode = DataSourceUpdateMode.Never,
            };
            edgeToCanAddNewButton.Format += new ConvertEventHandler(this.EdgeToBool);
            this.newVertexButton.DataBindings.Add(edgeToCanAddNewButton);

            var newModeReverse = new Binding(nameof(this.newPolygonButton.Enabled), this, nameof(this.NewMode), true, DataSourceUpdateMode.OnPropertyChanged)
            {
                DataSourceUpdateMode = DataSourceUpdateMode.Never,
            };
            newModeReverse.Format += new ConvertEventHandler(this.NewModeToEnabled);
            this.newPolygonButton.DataBindings.Add(newModeReverse);

            var edgeToCanBeVertical = new Binding(nameof(this.makeVerticalButton.Enabled), this, nameof(this.SelectedEdge), true, DataSourceUpdateMode.OnPropertyChanged)
            {
                DataSourceUpdateMode = DataSourceUpdateMode.Never,
            };
            edgeToCanBeVertical.Format += new ConvertEventHandler(this.EdgeToCanBeVertical);
            this.makeVerticalButton.DataBindings.Add(edgeToCanBeVertical);

            var edgeToCanBeHorizontal = new Binding(nameof(this.makeVerticalButton.Enabled), this, nameof(this.SelectedEdge), true, DataSourceUpdateMode.OnPropertyChanged)
            {
                DataSourceUpdateMode = DataSourceUpdateMode.Never,
            };
            edgeToCanBeHorizontal.Format += new ConvertEventHandler(this.EdgeToCanBeHorizontal);
            this.makeHorizontalButton.DataBindings.Add(edgeToCanBeHorizontal);

            var edgeToCanBeFixedInLength = new Binding(nameof(this.fixLengthButton.Enabled), this, nameof(this.SelectedEdge), true, DataSourceUpdateMode.OnPropertyChanged)
            {
                DataSourceUpdateMode = DataSourceUpdateMode.Never,
            };
            edgeToCanBeFixedInLength.Format += new ConvertEventHandler(this.EdgeToCanBeFixedInLength);
            this.fixLengthButton.DataBindings.Add(edgeToCanBeFixedInLength);

            var edgeToCanHaveRestricionRemoved = new Binding(nameof(this.removeRestrictionButton.Enabled), this, nameof(this.SelectedEdge), true, DataSourceUpdateMode.OnPropertyChanged)
            {
                DataSourceUpdateMode = DataSourceUpdateMode.Never,
            };
            edgeToCanHaveRestricionRemoved.Format += new ConvertEventHandler(this.EdgeToIsRestricted);
            this.removeRestrictionButton.DataBindings.Add(edgeToCanHaveRestricionRemoved);
        }
    }
}
