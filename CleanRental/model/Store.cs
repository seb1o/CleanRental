using System;
using System.Collections.Generic;

namespace CleanRental.model;

public partial class Store
{
    public int StoreId { get; set; }

    public int ManagerStaffId { get; set; }

    public int AddressId { get; set; }

    public DateTime LastUpdate { get; set; }

    public virtual Address Address { get; set; } = null!;

    public virtual Staff ManagerStaff { get; set; } = null!;
}
