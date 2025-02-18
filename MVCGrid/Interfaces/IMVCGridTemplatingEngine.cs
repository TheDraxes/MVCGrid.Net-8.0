using MVCGrid.Models;

namespace MVCGrid.Interfaces
{
    public interface IMVCGridTemplatingEngine
    {
        string Process(string template, TemplateModel model);
    }
}
