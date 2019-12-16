using DD.Lab.GenericUI.Core;
using DD.Lab.GenericUI.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Attribute = DD.Lab.GenericUI.Core.Models.Attribute;

namespace UIClientV2.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //var modelManager = new GenericManager();

            //var projectEntity = new Entity("Project", "Project");
            //projectEntity.Attributes.Add(new Attribute(Attribute.AttributeType.Guid, "Id", "Id", "Id", true));
            //projectEntity.Attributes.Add(new Attribute(Attribute.AttributeType.String, "Name", "Name", "Name", true));
            //projectEntity.Attributes.Add(new Attribute(Attribute.AttributeType.String, "Path", "Path", "Path", true));

            //var domainEntity = new Entity("Domain", "Domain");
            //domainEntity.Attributes.Add(new Attribute(Attribute.AttributeType.Guid, "Id", "Id", "Id"));
            //domainEntity.Attributes.Add(new Attribute(Attribute.AttributeType.String, "Name", "Name", "Name"));
            //domainEntity.Attributes.Add(new Attribute(Attribute.AttributeType.EntityReference, "ProjectId", "Project", "Project") { ReferencedEntity = "Project" });

            //var relationship = new Relationship("Project", "Domain", "ProjectId");

            //modelManager.Model.Entities.Add(projectEntity);
            //modelManager.Model.Entities.Add(domainEntity);
            //modelManager.Model.Relationships.Add(relationship);

            //modelManager.SaveCurrentModel("example.json");
        }
    }
}
