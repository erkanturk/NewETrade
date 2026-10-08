using System;
using System.Collections.Generic;
using System.Text;

namespace ETICARET.Entities.Dtos;

public record CategoryWithCount(int Id, string Name, int ProductCount, string? ImageUrl);


