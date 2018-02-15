using System.ComponentModel;

namespace WTS.BL.Models
{
    public enum RoleEnum
    {
        [Description("Supervisor")] Supervisor = 1,
        [Description("Technician")] Technician = 2
    }
}