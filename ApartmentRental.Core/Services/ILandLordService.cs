﻿using ApartmentRental.Core.DTO;

namespace ApartmentRental.Core.Services;

public interface ILandLordService
{
    Task AddLandLordAsync (LandLordCreationRequestDto dto);
}