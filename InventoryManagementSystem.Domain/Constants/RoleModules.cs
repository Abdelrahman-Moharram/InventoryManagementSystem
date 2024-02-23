using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain.Constants
{
    public class RoleModules
    {
        private readonly string[] _CrudsArr;

        private readonly Dictionary<string, string[]> _roleCruds;

        public RoleModules()
        {
            _CrudsArr = Enum
                .GetNames(typeof(Cruds))
                .Cast<string>()
                .ToArray();


            _roleCruds = new Dictionary<string, string[]>()
            {
                // Admins
                {$"{Roles.Admin}.{Modules.Product}",[Cruds.Read.ToString(), Cruds.Update.ToString(), Cruds.Create.ToString()] },
                {$"{Roles.Admin}.{Modules.Customer}",[Cruds.Read.ToString(), Cruds.Update.ToString(), Cruds.Create.ToString()] },
                {$"{Roles.Admin}.{Modules.Supplier}",[Cruds.Read.ToString(), Cruds.Update.ToString(), Cruds.Create.ToString()] },
                {$"{Roles.Admin}.{Modules.ProductItem}",[Cruds.Read.ToString(), Cruds.Update.ToString()] },
                {$"{Roles.Admin}.{Modules.Category}",[Cruds.Read.ToString(), Cruds.Update.ToString(), Cruds.Create.ToString()] },
                {$"{Roles.Admin}.{Modules.Order}",[Cruds.Read.ToString(), Cruds.Update.ToString(), Cruds.Create.ToString()] },
                {$"{Roles.Admin}.{Modules.Inventory}",[Cruds.Read.ToString(), Cruds.Update.ToString(), Cruds.Create.ToString()] },
                {$"{Roles.Admin}.{Modules.ProductsInventory}",[Cruds.Read.ToString(), Cruds.Update.ToString()] },
                {$"{Roles.Admin}.{Modules.Brand}",[Cruds.Read.ToString(), Cruds.Update.ToString(), Cruds.Create.ToString()] },


                // Customer
                {$"{Roles.Customer}.{Modules.Product}",[Cruds.Read.ToString()] },
                {$"{Roles.Customer}.{Modules.Customer}",[Cruds.Read.ToString()] },
                {$"{Roles.Customer}.{Modules.Supplier}",[Cruds.Read.ToString()] },
                {$"{Roles.Customer}.{Modules.ProductItem}",[Cruds.Read.ToString()] },
                {$"{Roles.Customer}.{Modules.Category}",[Cruds.Read.ToString()] },
                {$"{Roles.Customer}.{Modules.Order}",_CrudsArr },
                {$"{Roles.Customer}.{Modules.Inventory}",[] },
                {$"{Roles.Customer}.{Modules.ProductsInventory}",[] },
                {$"{Roles.Customer}.{Modules.Brand}",[Cruds.Read.ToString()] },

                // Supplier
                {$"{Roles.Supplier}.{Modules.Product}",[Cruds.Read.ToString()]},
                {$"{Roles.Supplier}.{Modules.Order}",_CrudsArr},
                {$"{Roles.Supplier}.{Modules.Customer}",[Cruds.Read.ToString()] },
                {$"{Roles.Supplier}.{Modules.Supplier}",[Cruds.Read.ToString()] },
                {$"{Roles.Supplier}.{Modules.ProductsInventory}",[Cruds.Read.ToString()] },
                {$"{Roles.Supplier}.{Modules.Inventory}",[Cruds.Read.ToString()] },
                {$"{Roles.Supplier}.{Modules.ProductItem}",_CrudsArr },
                {$"{Roles.Supplier}.{Modules.Category}",[Cruds.Read.ToString()] },
                {$"{Roles.Supplier}.{Modules.Brand}",[Cruds.Read.ToString()] },

            };
        }

        private static RoleModules _lock = new();
        private static RoleModules _instance = new();
        public static RoleModules instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                        _instance = new();
                    return _instance;
                }
            }
        }

        public string[] cruds(string roleName, string Module)
        {
            if (roleName == Roles.SuperAdmin.ToString())
                return _CrudsArr;
            return _roleCruds[$"{roleName}.{Module}"];
        }
    }
}
