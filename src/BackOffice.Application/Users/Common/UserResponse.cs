﻿namespace BackOffice.Application.Users.Common;

public record UserResponse(
    Guid Id,
    string FullName,
    string Email,
    bool Active);
