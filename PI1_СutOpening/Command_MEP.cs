using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using PI1_CORE;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PI1_СutOpening
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]

    // Start command class.
    public class Command_MEP : IExternalCommand
    {
        #region public methods

        /// <summary>
        /// Overload this method to implement and external command within Revit.
        /// </summary>
        /// <param name="commandData">An ExternalCommandData object which contains reference to Application and View
        /// needed by external command.</param>
        /// <param name="message">Error message can be returned by external command. This will be displayed only if the command status
        /// was "Failed".  There is a limit of 1023 characters for this message; strings longer than this will be truncated.</param>
        /// <param name="elements">Element set indicating problem elements to display in the failure dialog.  This will be used
        /// only if the command status was "Failed".</param>
        /// <returns>
        /// The result indicates if the execution fails, succeeds, or was canceled by user. If it does not
        /// succeed, Revit will undo any changes made by the external command.
        /// </returns>
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            // Options for operations. 
            SolidCurveIntersectionOptions sciOptions = new SolidCurveIntersectionOptions();
            StructuralType nonStr = StructuralType.NonStructural;
            XYZ zVector = new XYZ(0, 0, 1);

            // Initializing window for getting user information.
            var userInfo = new CommonData();
            using (var window = new Window_MEP(commandData))
            {
                window.ShowDialog();

                if (window.DialogResult == DialogResult.Cancel)
                {
                    return Result.Cancelled;
                }

                userInfo = window.GetInformation();
            }

            // Getting linked file.
            Document linkedDoc = (doc.GetElement(userInfo.LinkedFileId) as RevitLinkInstance).GetLinkDocument();

            // Getting BuiltInCategories for further operations.
            BuiltInCategory wallCategory = BuiltInCategory.OST_Walls;
            BuiltInCategory floorCategory = BuiltInCategory.OST_Floors;
            BuiltInCategory pipeCategory = BuiltInCategory.OST_PipeCurves;
            BuiltInCategory ductCategory = BuiltInCategory.OST_DuctCurves;
            BuiltInCategory condCategory = BuiltInCategory.OST_Conduit;
            BuiltInCategory cablCategory = BuiltInCategory.OST_CableTray;

            // Getting wall and floor elements from linked file.
            List<Element> wallsAndFloors = new MultiCategorySelection()
                .Selection(linkedDoc, wallCategory, floorCategory);

            // Main operation for creating of openings.
            using (Transaction t = new Transaction(doc, "Создание заглушек"))
            {
                t.Start();

                // Activate family symbols.
                FamilySymbol familyForWall = doc.GetElement(userInfo.FamilyForWallId) as FamilySymbol;
                FamilySymbol familyForFloor = doc.GetElement(userInfo.FamilyForFloorId) as FamilySymbol;
                familyForWall.Activate();
                familyForFloor.Activate();

                foreach (Element wallOrFloor in wallsAndFloors)
                {
                    // Getting solid of wall or floor
                    GeometryElement geometryElement = wallOrFloor.get_Geometry(new Options());
                    Solid solid = null;
                    foreach (GeometryObject geometryObject in geometryElement)
                    {
                        solid = geometryObject as Solid;
                        if (solid != null)
                        {
                            break;
                        }
                    }

                    // Getting elements that intersect wall or floor.
                    List<Element> selectionOfIntersectedElements = new IntersectionWithSolidSelection()
                        .Selection(doc, wallOrFloor, pipeCategory, ductCategory, condCategory, cablCategory);

                    foreach (Element communication in selectionOfIntersectedElements)
                    {
                        // Getting communication sizes.
                        var sizes = GetCommunicationSizes(communication);
                        double commWidth = sizes.commWidth;
                        double commHieght = sizes.commHieght;

                        // Check size of the communication and pass the element of iteration, if true.
                        if (commWidth < UnitUtils.ConvertToInternalUnits(100, DisplayUnitType.DUT_MILLIMETERS) ||
                            commHieght < UnitUtils.ConvertToInternalUnits(100, DisplayUnitType.DUT_MILLIMETERS))
                        {
                            continue;
                        }

                        // Getting center for insert.
                        LocationCurve commLocation = communication.Location as LocationCurve;
                        Curve commCurve = commLocation.Curve;
                        Curve curveSegment = null;
                        try
                        {
                            curveSegment = solid.IntersectWithCurve(commCurve, sciOptions).GetCurveSegment(0);
                        }
                        catch
                        {
                            continue;
                        }
                        XYZ center = ((curveSegment.GetEndPoint(0) + curveSegment.GetEndPoint(1)) / 2);

                        // Getting family symbol and direction in case of category of the element.
                        FamilySymbol familySymbol = null;
                        XYZ direction = null;
                        double widthOfElement = 0;
                        switch (wallOrFloor.Category.Id.IntegerValue)
                        {
                            case (int)BuiltInCategory.OST_Walls:
                                Wall wall = wallOrFloor as Wall;
                                familySymbol = familyForWall;
                                LocationCurve wallOrFloorLocCurve = wallOrFloor.Location as LocationCurve;
                                Curve wallOrFloorCurve = wallOrFloorLocCurve.Curve;
                                direction = (wallOrFloorCurve.GetEndPoint(0) - wallOrFloorCurve.GetEndPoint(1));
                                widthOfElement = wall.Width;
                                break;

                            case (int)BuiltInCategory.OST_Floors:
                                Floor floor = wallOrFloor as Floor;
                                familySymbol = familyForFloor;
                                direction = zVector;
                                widthOfElement = floor.get_Parameter(BuiltInParameter.FLOOR_ATTR_THICKNESS_PARAM).AsDouble();
                                break;
                        }

                        // Getting base level of the element.
                        Element level = null /*doc.GetElement(wallOrFloor.LevelId)*/;

                        // Creating opening.
                        FamilyInstance cutOpening = doc.Create.NewFamilyInstance(center, familySymbol, direction, level, nonStr);

                        // Set parameters to created opening.
                        cutOpening.get_Parameter(new Guid("bc4e92d8-db66-4e93-8923-3af6e2dc8599")).Set(commHieght + 2 * userInfo.Offset); // ADSK_Отверстие_Высота
                        cutOpening.get_Parameter(new Guid("096bc30e-3c95-4637-84d5-9f6bf45d8676")).Set(commWidth + 2 * userInfo.Offset); // ADSK_Отверстие_Ширина
                        cutOpening.get_Parameter(new Guid("40b92c1c-1fb2-4492-b339-9754be36c31a")).Set(widthOfElement); // ADSK_Отверстие_Глубина
                    }
                }

                t.Commit();
            }

            return Result.Succeeded;
        }

        /// <summary>
        /// Gets the path of the current command.
        /// </summary>
        /// <returns></returns>
        public static string GetPath()
        {
            return typeof(Command_MEP).Namespace + "." + nameof(Command_MEP);
        }

        #endregion

        #region private methods

        private (double commWidth, double commHieght) GetCommunicationSizes(Element communication)
        {
            ElementId categoryId = communication.Category.Id;
            BuiltInCategory biCommCategory = (BuiltInCategory)categoryId.IntegerValue;

            double commDiam = 0;
            double commWidth = 0;
            double commHieght = 0;

            switch (biCommCategory)
            {
                case BuiltInCategory.OST_PipeCurves:
                    commDiam = communication.get_Parameter(BuiltInParameter.RBS_PIPE_OUTER_DIAMETER).AsDouble();
                    commWidth = commDiam;
                    commHieght = commDiam;
                    break;

                case BuiltInCategory.OST_DuctCurves:
                    string ductForm = communication.get_Parameter(BuiltInParameter.ELEM_FAMILY_PARAM).AsValueString();
                    if (ductForm == "Воздуховод круглого сечения")
                    {
                        commDiam = communication.get_Parameter(BuiltInParameter.RBS_CURVE_DIAMETER_PARAM).AsDouble();
                        commWidth = commDiam;
                        commHieght = commDiam;
                    }
                    else
                    {
                        commWidth = communication.get_Parameter(BuiltInParameter.RBS_CURVE_WIDTH_PARAM).AsDouble();
                        commHieght = communication.get_Parameter(BuiltInParameter.RBS_CURVE_HEIGHT_PARAM).AsDouble();
                    }
                    break;

                case BuiltInCategory.OST_Conduit:
                    commDiam = communication.get_Parameter(BuiltInParameter.RBS_CONDUITRUN_DIAMETER_PARAM).AsDouble();
                    commWidth = commDiam;
                    commHieght = commDiam;
                    break;

                case BuiltInCategory.OST_CableTray:
                    commWidth = communication.get_Parameter(BuiltInParameter.RBS_CABLETRAY_WIDTH_PARAM).AsDouble();
                    commHieght = communication.get_Parameter(BuiltInParameter.RBS_CABLETRAY_HEIGHT_PARAM).AsDouble();
                    break;
            }

            return (commWidth, commHieght);
        }

        #endregion
    }
}
