using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using PI1_CORE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PI1_СutOpening
{
    public partial class Window_AR : System.Windows.Forms.Form
    {
        #region private methods

        private ExternalCommandData commandData;

        private Document doc;
        
        private ElementId familyForWallId { get; set; }

        private ElementId familyForFloorId { get; set; }

        #endregion

        #region constructor

        public Window_AR(ExternalCommandData commandData)
        {
            InitializeComponent();
            this.commandData = commandData;
            this.doc = commandData.Application.ActiveUIDocument.Document;
        }

        #endregion

        #region events

        private void Window_AR_Load(object sender, EventArgs e)
        {
            PopulateFamilyForWall();
            PopulateFamilyForFloor();
        }

        private void cmbFamilyForWall_SelectedIndexChanged(object sender, EventArgs e)
        {
            familyForWallId = ((KeyValuePair<string, ElementId>)cmbFamilyForWall.SelectedItem).Value;
        }

        private void cmbFamilyForFloor_SelectedIndexChanged(object sender, EventArgs e)
        {
            familyForFloorId = ((KeyValuePair<string, ElementId>)cmbFamilyForFloor.SelectedItem).Value;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Close();
        }

        #endregion

        #region private methods

        private void PopulateFamilyForWall()
        {
            var windows = new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_Windows)
                .WhereElementIsElementType();

            PopulateCommand.PopulateKeyValueList(windows, cmbFamilyForWall);
        }

        private void PopulateFamilyForFloor()
        {
            var windows = new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_Windows)
                .WhereElementIsElementType();

            PopulateCommand.PopulateKeyValueList(windows, cmbFamilyForFloor);
        }
                
        #endregion

        #region public methods

        public CommonData GetInformation()
        {
            var information = new CommonData()
            {
                FamilyForWallId = familyForWallId,
                FamilyForFloorId = familyForFloorId
            };

            return information;
        }

        #endregion
    }
}
