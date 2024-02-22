namespace InventoryManagementSystem.Domain.Constants
{
    public static class Permissions
    {
        
        public static List<string> GeneratePermissionsList(string module, string[] cruds)
        {
            List<string> permissions = new ();
            
            foreach (var op in cruds)
            {
                permissions.Add ($"{OtherConstants.Permissions}.{op}.{module}");
            }
            return permissions;
        }
        public static List<string> AllCrudsList(string ModuleName)
        {
            List<string> Cruds = new ();
            foreach (string crud in Enum.GetNames (typeof(Cruds)))
            {
                Cruds.Add ($"{OtherConstants.Permissions}.{crud}.{ModuleName}");
            }
            return Cruds;
        }

        public static List<string> AllPermissionsList()
        {
            List<string> Permissions = new List<string>();
            foreach(string module in Enum.GetNames( typeof( Modules )))
            {
                Permissions.AddRange(AllCrudsList(module));
            }
            return Permissions;
        }

    }
}
