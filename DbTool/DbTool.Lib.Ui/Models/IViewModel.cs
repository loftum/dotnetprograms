namespace DbTool.Lib.Ui.Models
{
    public delegate void ModelChangeEventHandler(object sender, ModelChangeEventArgs e);

    public interface IViewModel
    {
        event ModelChangeEventHandler ModelChange;
    }
}