using Riok.Mapperly.Abstractions;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.Domain.DTOs;
using Sprout.Exam.Domain.DTOs.Employee.Commands;
using Sprout.Exam.Domain.Models;
using System;
using System.Collections.Generic;

namespace Sprout.Exam.Common.Mapping
{
    [Mapper]
    public partial class EmployeeMapping
    {

        [MapProperty(nameof(CreateEmployeeRequestDto.TypeId), nameof(Employee.EmployeeTypeId))]
        public partial Employee MapFromCreate(CreateEmployeeRequestDto dto);

        [MapProperty(nameof(Employee.EmployeeTypeId), nameof(EmployeeDto.TypeId))]
        public partial EmployeeDto MapFromEntity(Employee entity);
        public partial List<EmployeeDto> MapFromEntities(List<Employee> entity);

        public int EmployeeTypeToId(EmployeeType employeeType) => (int)employeeType;
        public EmployeeType TypeIdToEnum(int employeeTypeId) => (EmployeeType)employeeTypeId;
        public string DateTimeToString(DateTime datetime) => datetime.ToString("yyyy-MM-dd");
    }
}
