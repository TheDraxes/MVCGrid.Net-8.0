using MVCGrid.Models;
using System.IO;
using System.Web;

namespace MVCGrid.Interfaces
{
    public interface IMVCGridRenderingEngine
    {
        bool AllowsPaging { get; }
        void PrepareResponse(HttpResponse response);
        void Render(RenderingModel model, GridContext gridContext, TextWriter outputStream);
        void RenderContainer(ContainerRenderingModel model, TextWriter outputStream);
    }
}
