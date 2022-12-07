using AutoMapper;
using ReportService.DTOs;
using ReportService.Entities.Report;

namespace ContactService.Profiles
{
    public class ReportProfile : Profile
    {
        #region Ctor

        public ReportProfile()
        {
            CreateMap<Report, ReportDTO>()
                .ReverseMap();
            CreateMap<Report, ReportDetailsDTO>()
                .ReverseMap();
        }

        #endregion Ctor
    }
}