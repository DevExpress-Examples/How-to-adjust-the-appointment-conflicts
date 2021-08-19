using System;
using System.ComponentModel;
using System.Drawing;

namespace DXApplication1 {
    public class InitHelper {
        public static Random RandomInstance = new Random();

        public static void InitResources(BindingList<CustomResource> resources) {
            resources.Add(CreateCustomResource(1, "Peter Dolan", Color.PowderBlue));
            resources.Add(CreateCustomResource(2, "Ryan Fisher", Color.PaleVioletRed));
            resources.Add(CreateCustomResource(3, "Andrew Miller", Color.PeachPuff));
        }

        public static CustomResource CreateCustomResource(int res_id, string caption, Color ResColor) {
            CustomResource cr = new CustomResource();
            cr.ResID = res_id;
            cr.Name = caption;
            return cr;
        }
    }
}
