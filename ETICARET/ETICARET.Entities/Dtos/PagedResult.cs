using System;
using System.Collections.Generic;
using System.Text;

namespace ETICARET.Entities.Dtos;

public record PagedResult<T>(List<T> Item, int TotalCount);

