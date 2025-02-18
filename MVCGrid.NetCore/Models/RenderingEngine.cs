using System.Collections.Generic;

namespace MVCGrid.NetCore.Models
{
    public class RenderingEngine
    {
        public RenderingEngine(string Name, string Type)
        {
            this.Name = Name;
            this.Type = Type;
        }
        public string Name { get; set; }
        public string Type { get; set; }

        public static RenderingEngine BootstrapRenderingEngine
        {
            get
            {
                return new RenderingEngine("BootstrapRenderingEngine", "MVCGrid.Rendering.BootstrapRenderingEngine, MVCGrid.NetCore");
            }
        }
        public static RenderingEngine ExportDefault
        {
            get
            {
                return new RenderingEngine("Export", "MVCGrid.Rendering.CsvRenderingEngine, MVCGrid.NetCore");
            }
        }
    }

    public class RenderingEngineCollection : Dictionary<string, RenderingEngine>
    {
        public RenderingEngineCollection() : base()
        {

        }
        public void Add(RenderingEngine renderingEngine)
        {
            base.Add(renderingEngine.Name, renderingEngine);
        }
    }
}
