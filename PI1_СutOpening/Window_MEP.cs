using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using PI1_CORE;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PI1_СutOpening
{
    public partial class Window_MEP : System.Windows.Forms.Form
    {
        #region private members

        private ExternalCommandData _commandData;

        private Document doc;

        private ElementId familyForWallId { get; set; }

        private ElementId familyForFloorId { get; set; }

        private double txtOffset { get; set; }

        private ElementId linkedFileId { get; set; }


        #endregion

        #region constructor

        public Window_MEP(ExternalCommandData commandData)
        {
            InitializeComponent();
            _commandData = commandData;
            doc = commandData.Application.ActiveUIDocument.Document;
        }

        #endregion

        #region events

        private void Window_MEP_Load(object sender, EventArgs e)
        {
            PopulateFamilyForWall();
            PopulateFamilyForFloor();
            PopulateLinkedFile();
        }

        private void cmbFamilyForWall_SelectedIndexChanged(object sender, EventArgs e)
        {
            familyForWallId = ((KeyValuePair<string, ElementId>)cmbFamilyForWall.SelectedItem).Value;
        }

        private void cmbFamilyForFloor_SelectedIndexChanged(object sender, EventArgs e)
        {
            familyForFloorId = ((KeyValuePair<string, ElementId>)cmbFamilyForFloor.SelectedItem).Value;
        }

        private void txtbOffset_TextChanged(object sender, EventArgs e)
        {
            txtOffset = UnitUtils.ConvertToInternalUnits(Convert.ToDouble(txtbOffset.Text), DisplayUnitType.DUT_MILLIMETERS);
        }

        private void cmbLinkedFile_SelectedIndexChanged(object sender, EventArgs e)
        {
            linkedFileId = ((KeyValuePair<string, ElementId>)cmbLinkedFile.SelectedItem).Value;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Close();
        }

        #endregion

        #region prevate methods

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

        private void PopulateLinkedFile()
        {
            var linkedFile = new FilteredElementCollector(doc)
                .OfClass(typeof(RevitLinkInstance));

            PopulateCommand.PopulateKeyValueList(linkedFile, cmbLinkedFile);
        }
        #endregion

        #region public methods

        public CommonData GetInformation()
        {
            var information = new CommonData()
            {
                FamilyForWallId = familyForWallId,
                FamilyForFloorId = familyForFloorId,
                Offset = txtOffset,
                LinkedFileId = linkedFileId
            };

            return information;
        }

        #endregion
    }
}
