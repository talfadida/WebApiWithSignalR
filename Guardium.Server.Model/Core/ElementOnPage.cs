using System;

namespace Guardium.Server.Model
{
    public class ElementOnPage: ElementContent
    {
       
        public User CreatedBy { get; set; }

        public DateTime CreatedWhen { get; set; }

    }

 
}
