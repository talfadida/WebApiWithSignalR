using System;
using System.Linq;
using System.Collections.Generic;

namespace Guardium.Server.Model
{

    public class Page
    {
        public string PageUUID { get; set; }
        public User UserCreator { get; set; }
        
         
        public List<ElementOnPage> ListOfElements { get; set; } = new List<ElementOnPage>();


        public Page(string uuid, User creator)
        {
            this.PageUUID = uuid;
            this.UserCreator = creator;            
        }


        public IEnumerable<ElementContent> GetStringElementsForUi()
        {
            return ListOfElements.Cast<ElementContent>(); //Select(e => e.ElementContent);
        }

        public bool TryAddElement(User user, ElementContent elementContent, out string status)
        {
            status = "";
            try
            {
                ListOfElements.Add(new ElementOnPage()
                {
                    CreatedBy = user,
                    CreatedWhen = DateTime.Now,
                    ElementIdentifier = elementContent.ElementIdentifier,
                    Content = elementContent.Content
                     
                });
                return true;
            }
            catch (Exception ex)
            {
                status = ex.Message;
                return false;
            }
        }

        public bool TryDeleteElement(string elementIdentifier, out object status)
        {
            status = "";
            try
            {

                if (ListOfElements.Remove(ListOfElements.Find(e => e.ElementIdentifier == elementIdentifier)))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                status = ex.Message;
                return false;
            }
        }
    }
}
