using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using PI1_CORE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PI1_СutOpening
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]

    // Start command class.
    public class Command_AR : IExternalCommand
    {
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
            StructuralType nonStr = StructuralType.NonStructural;

            // Initializing window for getting user information.
            var userInfo = new CommonData();
            using (var window = new Window_AR(commandData))
            {
                window.ShowDialog();

                if (window.DialogResult == DialogResult.Cancel)
                {
                    return Result.Cancelled;
                }

                userInfo = window.GetInformation();
            }

            // Getting BuiltInCategories for further operations.
            BuiltInCategory wallCategory = BuiltInCategory.OST_Walls;
            BuiltInCategory floorCategory = BuiltInCategory.OST_Floors;
            BuiltInCategory windowCategory = BuiltInCategory.OST_Windows;

            // Getting wall and floor elements from the file.
            List<Element> wallsAndFloors = new MultiCategorySelection()
                .Selection(doc, wallCategory, floorCategory);

            // Main operation for creating of openings.
            using (Transaction t = new Transaction(doc, "Создание отверстий"))
            {
                t.Start();

                // Activate family symbols.
                FamilySymbol familyForWall = doc.GetElement(userInfo.FamilyForWallId) as FamilySymbol;
                FamilySymbol familyForFloor = doc.GetElement(userInfo.FamilyForFloorId) as FamilySymbol;
                familyForWall.Activate();
                familyForFloor.Activate();

                foreach (Element wallOrFloor in wallsAndFloors)
                {
                    // Getting integer value of category of the element;
                    int wallOrFloorIntCategory = wallOrFloor.Category.Id.IntegerValue;

                    // Getting family symbol in case of category of the element.
                    FamilySymbol familySymbol = null;
                    switch (wallOrFloorIntCategory)
                    {
                        case (int)BuiltInCategory.OST_Walls:
                            familySymbol = familyForWall;
                            break;
                        case (int)BuiltInCategory.OST_Floors:
                            familySymbol = familyForFloor;
                            break;
                    }

                    // Getting base level of the element.
                    Level level = doc.GetElement(wallOrFloor.LevelId) as Level;

                    // Getting elements that intersect wall or floor.
                    var selectionOfIntersectedElements = new IntersectionWithSolidSelection()
                        .Selection(doc, wallOrFloor, windowCategory)
                        .Where(el => el.Name.Contains("Заглушка"));

                    foreach (Element window in selectionOfIntersectedElements)
                    {
                        // Getting center for insert.
                        LocationPoint locPoint = window.Location as LocationPoint;
                        XYZ center = locPoint.Point;

                        // Check the acceptence and create an opening in case of it.
                        int acceptance = window.LookupParameter("Принято").AsInteger();
                        if (acceptance == 1)
                        {
                            // Getting parameters from current opening.
                            double commHieght = window.get_Parameter(new Guid("bc4e92d8-db66-4e93-8923-3af6e2dc8599")).AsDouble(); // ADSK_Отверстие_Высота
                            double commWidth = window.get_Parameter(new Guid("096bc30e-3c95-4637-84d5-9f6bf45d8676")).AsDouble(); // ADSK_Отверстие_Ширина

                            // Creating opening.
                            FamilyInstance cutOpening = doc.Create.NewFamilyInstance(center, familySymbol, wallOrFloor, level, nonStr);

                            // Setting parameters were got from window.
                            cutOpening.get_Parameter(new Guid("bc4e92d8-db66-4e93-8923-3af6e2dc8599")).Set(commHieght); // ADSK_Отверстие_Высота
                            cutOpening.get_Parameter(new Guid("096bc30e-3c95-4637-84d5-9f6bf45d8676")).Set(commWidth); // ADSK_Отверстие_Ширина
                        }
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
            return typeof(Command_AR).Namespace + "." + nameof(Command_AR);
        }
    }
}
