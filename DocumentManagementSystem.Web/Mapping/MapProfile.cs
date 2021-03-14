using AutoMapper;
using DocumentManagementSystem.Core.Entities;
using DocumentManagementSystem.Web.DTOs;


namespace DocumentManagementSystem.Web.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

            CreateMap<UserType, UserTypeDto>();
            CreateMap<UserTypeDto, UserType>();

            CreateMap<Programs, ProgramDto>();
            CreateMap<ProgramDto, Programs>();

            CreateMap<Course, CourseDto>();
            CreateMap<CourseDto, Course>();

            CreateMap<StudentInfo, StudentInfoDto>();
            CreateMap<StudentInfoDto, StudentInfo>();

            CreateMap<ProjectType, ProjectTypeDto>();
            CreateMap<ProjectTypeDto, ProjectType>();

            CreateMap<Reports, ReportsDto>();
            CreateMap<ReportsDto, Reports>();

            CreateMap<StudentFinalGrade, StudentFinalGradeDto>();
            CreateMap<StudentFinalGradeDto, StudentFinalGrade>();

            CreateMap<Semester, SemesterDto>();
            CreateMap<SemesterDto, Semester>();

            CreateMap<Announcement, AnnouncementDto>();
            CreateMap<AnnouncementDto, Announcement>();
        }
    }
}
