using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Stroymaterials.AppData
{
    public partial class Spare
    {
        public string CorrectImage
        {
            get
            {
                if (String.IsNullOrEmpty(spare_photo) || String.IsNullOrWhiteSpace(spare_photo))
                {
                    return "../Resources/picture.jpg";
                }
                else
                {
                    return "../Resources/" + spare_photo;
                }
            }
        }
        public Brush CountZero
        {
            get
            {
                if (spare_count <= 0)
                {
                    return Brushes.Gray;
                }
                else
                {
                    return Brushes.Transparent;
                }
            }
        }
    }
}
