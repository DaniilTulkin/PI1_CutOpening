using Autodesk.Revit.DB;

namespace PI1_СutOpening
{
    /// <summary>
    /// Helper for getting user information.
    /// </summary>
    public class CommonData
    {
        #region public members

        /// <summary>
        /// Gets or sets the window family Id for wall.
        /// </summary>
        /// <value>
        /// The family for wall identifier.
        /// </value>
        public ElementId FamilyForWallId { get; set; }

        /// <summary>
        /// Gets or sets the window family Id for floor.
        /// </summary>
        /// <value>
        /// The family for floor identifier.
        /// </value>
        public ElementId FamilyForFloorId { get; set; }

        /// <summary>
        /// Gets or sets the offset.
        /// </summary>
        /// <value>
        /// The offset.
        /// </value>
        public double Offset { get; set; }

        /// <summary>
        /// Gets or sets the linked file Id.
        /// </summary>
        /// <value>
        /// The linked file identifier.
        /// </value>
        public ElementId LinkedFileId { get; set; }

        #endregion
    }
}
