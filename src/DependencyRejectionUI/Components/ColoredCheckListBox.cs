using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DependercyRejectionUI
{
    public class ColoredCheckListBox: CheckedListBox
    {
        protected override void OnDrawItem(DrawItemEventArgs e)
        {

            base.OnDrawItem(e);
            ////base.OnCausesValidationChanged(e);

            //e.DrawBackground();

            //var projectType = (ProjectType)this.Items[e.Index];

            //var brush = new SolidBrush(projectType.Color);
            
            //e.Graphics.DrawString(projectType.TypeName, this.Font, brush, e.Bounds.X+20, e.Bounds.Y);

            
        }
    }
}
