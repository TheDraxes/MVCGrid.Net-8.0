using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace MVCGrid.Models
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
                return new RenderingEngine("BootstrapRenderingEngine", "MVCGrid.Rendering.BootstrapRenderingEngine, MVCGrid");
            }
        }
        public static RenderingEngine ExportDefault
        {
            get
            {
                return new RenderingEngine("Export", "MVCGrid.Rendering.CsvRenderingEngine, MVCGrid");
            }
        }
    }

    public class RenderingEngineCollection : Dictionary<string, RenderingEngine>
    {
        public void Add(RenderingEngine renderingEngine)
        {
            base.Add(renderingEngine.Name, renderingEngine);
        }
    }
}
