using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Rule.Web.WebUserControl
{
    public partial class UCModalDialog2 : System.Web.UI.UserControl
    {
        public string ModalDialogName { get; set; }

        [TemplateContainer(typeof(ControlContainer))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public ITemplate ControlsContainer { get; set; }

        public override Control FindControl(string id)
        {
            return Container.FindControl(id);
        }

        public class ControlContainer : Control, INamingContainer
        {

        }

        private ControlContainer _container;
        public ControlContainer Container
        {
            get
            {
                if (_container == null)
                    _container = new ControlContainer();
                return _container;
            }
            set
            {
                _container = value;
            }
        }

        void Page_Init()
        {
            if (ControlsContainer != null)
            {
                ControlsContainer.InstantiateIn(Container);
                plov.Controls.Add(Container);
            }
        }

        private void assignControlObjects()
        {
            foreach (Control ctl in Container.Controls)
            {

            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        //public string Width
        //{
        //    set
        //    {
        //        dv.Style.Remove(HtmlTextWriterStyle.Width);
        //        dv.Style.Add(HtmlTextWriterStyle.Width, value);
        //    }
        //}

        //public string Height
        //{
        //    set
        //    {
        //        dv.Style.Remove(HtmlTextWriterStyle.Height);
        //        dv.Style.Add(HtmlTextWriterStyle.Height, value);
        //    }
        //}
    }
}