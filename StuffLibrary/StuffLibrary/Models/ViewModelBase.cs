namespace StuffLibrary.Models
{
    public class ViewModelBase
    {
         public string GenerateTitle(string title)
         {
             return string.Format("StuffLibrary - {0}", title);
         }
    }
}