using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.Control;

namespace IllTechLibrary.Util
{
    public class ControlUtil
    {
        public static ToolStrip GetToolStrip(Form frm)
        {
            IEnumerable<ToolStrip> objects = frm.Controls.OfType<ToolStrip>();

            return objects.First(p => !p.GetType().Equals(typeof(StatusStrip)));
        }

        public static IEnumerable<Control> GetAll(Form frm)
        {
            List<Control> ctls = new List<Control>();

            foreach(Control ctl in frm.Controls)
            {
                // Add root control
                ctls.Add(ctl);

                // Recursivly add children
                ctls.AddRange(GetAll(ctl));
            }

            return ctls;
        }

        public static IEnumerable<Control> GetAll(Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>();

            return controls.SelectMany(ctrl => GetAll(ctrl, type))
                                      .Concat(controls)
                                      .Where(c => c.GetType() == type);
        }

        public static IEnumerable<Control> GetAll(Control control)
        {
            var controls = control.Controls.Cast<Control>();

            return controls.SelectMany(ctrl => GetAll(ctrl))
                                      .Concat(controls);
        }

        public static IEnumerable<T> GetControlsOfType<T>(Control root)
    where T : Control
        {
            var t = root as T;
            if (t != null)
                yield return t;

            if (root.Controls != null)
                foreach (Control c in root.Controls)
                    foreach (var i in GetControlsOfType<T>(c))
                        yield return i;
        }
    }
}
