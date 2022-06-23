using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using PI1_UI;
using System.Linq;

namespace PI1_СutOpening
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]

    // Running interface class
    public class Main : IExternalApplication
    {

        /// <summary>
        /// Implement this method to execute some tasks when Autodesk Revit shuts down.
        /// </summary>
        /// <param name="application">A handle to the application being shut down.</param>
        /// <returns>
        /// Indicates if the external application completes its work successfully.
        /// </returns>
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        /// <summary>
        /// Implement this method to create tab, ribbon and button or add elements if tab and ribbon was created when Autodesk Revit starts.
        /// </summary>
        /// <param name="application">A handle to the application being started.</param>
        /// <returns>
        /// Indicates if the external application completes its work successfully.
        /// </returns>
        public Result OnStartup(UIControlledApplication application)
        {
            string tabName = "PI1";
            string ribbonPanelName_AR = RibbonName.Name(RibbonNameType.AR);
            string ribbonPanelName_MEP = RibbonName.Name(RibbonNameType.MEP);

            RibbonPanel ribbonPanel_AR = null;
            RibbonPanel ribbonPanel_MEP = null;

            try
            {
                application.CreateRibbonTab(tabName);
            }
            catch { }

            try
            {
                ribbonPanel_AR = application.CreateRibbonPanel(tabName, ribbonPanelName_AR);
            }
            catch
            {
                ribbonPanel_AR = application.GetRibbonPanels(tabName)
                    .FirstOrDefault(panel => panel.Name.Equals(ribbonPanelName_AR));
            }

            try
            {
                ribbonPanel_MEP = application.CreateRibbonPanel(tabName, ribbonPanelName_MEP);
            }
            catch
            {
                ribbonPanel_MEP = application.GetRibbonPanels(tabName)
                    .FirstOrDefault(panel => panel.Name.Equals(ribbonPanelName_MEP));
            }

            var btnAssociatingParametersToGlobalData_AR = new RevitPushButtonData
            {
                Label = "Создание\nотверстий",
                Panel = ribbonPanel_AR,
                ToolTip = "Создаёт отверстия по предварительно принятым заглушкам (условным отверстиям).",
                CommandNamespacePath = Command_AR.GetPath(),
                ImageName = "icon_PI1_СutOpening_AR_16x16.png",
                LargeImageName = "icon_PI1_СutOpening_AR_32x32.png"
            };

            var btnAssociatingParametersToGlobalData_MEP = new RevitPushButtonData
            {
                Label = "Создание\nзаглушек",
                Panel = ribbonPanel_MEP,
                ToolTip = "Создаёт заглушки (условные отверстия) в местах пересечения инженерных сетей и стен.",
                CommandNamespacePath = Command_MEP.GetPath(),
                ImageName = "icon_PI1_СutOpening_MEP_16x16.png",
                LargeImageName = "icon_PI1_СutOpening_MEP_32x32.png"
            };

            var btnAssociatingParametersToGlobal_AR = RevitPushButton.Create(btnAssociatingParametersToGlobalData_AR);
            var btnAssociatingParametersToGlobal_MEP = RevitPushButton.Create(btnAssociatingParametersToGlobalData_MEP);

            return Result.Succeeded;
        }
    }
}
